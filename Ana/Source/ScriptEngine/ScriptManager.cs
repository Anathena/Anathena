namespace Ana.Source.ScriptEngine
{
    using Content.Templates;
    using CSScriptLibrary;
    using Output;
    using Project.ProjectItems;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils;

    /// <summary>
    /// Class for managing a single script.
    /// </summary>
    internal class ScriptManager
    {
        /// <summary>
        /// The identifier to look for when inserting a the using statements from a script into the main script template.
        /// </summary>
        public const String ScriptUsingsInsertionIdentifier = "{{USINGS}}";

        /// <summary>
        /// The identifier to look for when inserting a classless script into the main script template.
        /// </summary>
        public const String ScriptCodeInsertionIdentifier = "{{CODE}}";

        /// <summary>
        /// Time to wait for the update loop to finish on deactivation.
        /// </summary>
        private const Int32 AbortTime = 500;

        /// <summary>
        /// Update time in milliseconds.
        /// </summary>
        private const Int32 UpdateTime = 1000 / 15;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptManager" /> class.
        /// </summary>
        public ScriptManager()
        {
        }

        /// <summary>
        /// Gets or sets a cancelation request for the update loop.
        /// </summary>
        private CancellationTokenSource CancelRequest { get; set; }

        /// <summary>
        /// Gets or sets the task for the update loops.
        /// </summary>
        private Task Task { get; set; }

        /// <summary>
        /// Gets or sets the compiled assembly object of a script.
        /// </summary>
        private dynamic ScriptObject { get; set; }

        /// <summary>
        /// Compiles a script. Will compress the file and convert to base64. This will compile using CodeDOM becuase this
        /// generates a file that we can read to create the assembly. At runtime we can compile using Mono instead.
        /// </summary>
        /// <param name="script">The input script in plaintext.</param>
        /// <returns>The compiled script. Returns null on failure.</returns>
        public String CompileScript(String script)
        {
            String result = null;

            try
            {
                script = this.PrecompileScript(script);
                String compiledScriptFile = CSScript.CompileCode(script);
                Byte[] compressedScript = Compression.Compress(File.ReadAllBytes(compiledScriptFile));
                result = Convert.ToBase64String(compressedScript);

                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Script compiled");
            }
            catch (Exception ex)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Error compiling script: " + ex.ToString());
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Runs the activation function in the script.
        /// </summary>
        /// <param name="scriptItem">The script to run.</param>
        /// <returns>Returns true if the function successfully ran, otherwise false.</returns>
        public Boolean RunActivationFunction(ScriptItem scriptItem)
        {
            try
            {
                Assembly assembly;

                if (scriptItem.IsCompiled)
                {
                    // Assembly already compiled, just load it
                    assembly = Assembly.Load(Compression.Decompress(Convert.FromBase64String(scriptItem.Script)));
                }
                else
                {
                    // Raw script, compile it. Use Mono at runtime instead of CodeDOM.
                    String script = this.PrecompileScript(scriptItem.Script);
                    assembly = CSScript.MonoEvaluator.CompileCode(script);
                }

                this.ScriptObject = assembly.CreateObject("*");
                this.ScriptObject.OnActivate();

                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Script activated: " + scriptItem.Description?.ToString());
            }
            catch (SecurityException ex)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Invalid operation in sandbox environment: " + ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Script activation error: " + ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Continously runs the update function in the script.
        /// </summary>
        /// <param name="scriptItem">The script to run.</param>
        public void RunUpdateFunction(ScriptItem scriptItem)
        {
            this.CancelRequest = new CancellationTokenSource();

            try
            {
                this.Task = Task.Run(
                async () =>
                {
                    TimeSpan elapsedTime;
                    DateTime previousTime = DateTime.Now;

                    while (true)
                    {
                        DateTime currentTime = DateTime.Now;
                        elapsedTime = currentTime - previousTime;

                        try
                        {
                            // Call the update function, giving the elapsed milliseconds since the previous call
                            ScriptObject.OnUpdate((Single)elapsedTime.TotalMilliseconds);
                        }
                        catch (Exception ex)
                        {
                            String exception = ex.ToString();

                            if (exception.ToString().Contains("does not contain a definition for 'OnUpdate'"))
                            {
                                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Optional update function not executed");
                            }
                            else
                            {
                                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Error running update function: " + ex.ToString());
                            }

                            return;
                        }

                        previousTime = currentTime;

                        // Await with cancellation
                        await Task.Delay(ScriptManager.UpdateTime, this.CancelRequest.Token);
                    }
                },
                this.CancelRequest.Token);

                return;
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Error executing update loop.");
            }
        }

        /// <summary>
        /// Runs the deactivation function in the script.
        /// </summary>
        /// <param name="scriptItem">The script to run.</param>
        public void RunDeactivationFunction(ScriptItem scriptItem)
        {
            // Abort the update loop
            try
            {
                this.ScriptObject.OnDeactivate();

                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Script deactivated: " + scriptItem.Description?.ToString());

                try
                {
                    this.CancelRequest?.Cancel();
                    this.Task?.Wait(ScriptManager.AbortTime);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Error when deactivating script: " + ex.ToString());
            }

            return;
        }

        /// <summary>
        /// Takes the classless script written by the user and embeds it in the main script template.
        /// This gives the script access to the engine classes that it will require.
        /// </summary>
        /// <param name="script">The classless script.</param>
        /// <returns>The complete script.</returns>
        private String PrecompileScript(String script)
        {
            StringBuilder usings = new StringBuilder(4096);
            String classlessScript = String.Empty;
            script = script ?? String.Empty;

            using (StringReader sr = new StringReader(script))
            {
                // Collect all using statements from the script
                String line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    // Ignore comments and whitespace
                    if (line.StartsWith("//") || line.Trim() == String.Empty)
                    {
                        continue;
                    }

                    if (!line.TrimStart().StartsWith("using "))
                    {
                        break;
                    }

                    // Collect using statement
                    usings.AppendLine(line);
                }

                // The remaining portion of the script will be kept as the actual script
                if (line != null)
                {
                    classlessScript = line + sr.ReadToEnd();
                }
            }

            // Fill in the script template with the collected information
            script = new ScriptTemplate().TransformText().Replace(ScriptManager.ScriptUsingsInsertionIdentifier, usings.ToString());
            script = script.Replace(ScriptManager.ScriptCodeInsertionIdentifier, classlessScript);

            return script;
        }
    }
    //// End class
}
//// End namespace