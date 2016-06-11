/*
* Copyright (c) 2007-2008 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Xml.XPath;

namespace BuildTasks {
	public class GenerateUnitTests : Task {
		string assemblyFileName;
		string outputPath;

		[Required]
		public string AssemblyFile {
			get {
				return assemblyFileName;
			}
			set {
				assemblyFileName = value;
			}
		}

		[Required]
		public string OutputPath {
			get {
				return outputPath;
			}
			set {
				outputPath = value;
			}
		}

		public override bool Execute() {
			Dictionary<string, string> templates = LoadTemplates();

			if (!File.Exists(assemblyFileName)) {
				Log.LogError(string.Format("Assembly '{0}' does not exist.", assemblyFileName));
				return false;
			}

			Assembly assembly = Assembly.LoadFile(assemblyFileName);
			if (assembly == null) {
				Log.LogError(string.Format("Assembly '{0}' could not be loaded.", assemblyFileName));
				return false;
			}

			foreach (Type type in assembly.GetExportedTypes()) {
				if (type.IsValueType) {
					string cleanName = CleanTypeName(type.FullName);
					string fileName = string.Format("{0}GeneratedTests.cs", cleanName);
					string filePath = Path.Combine(outputPath, fileName);

					using (StreamWriter writer = new StreamWriter(filePath)) {
						WriteTestHeader(writer);
						writer.WriteLine("\t[TestFixture]");
						writer.WriteLine(string.Format("\tpublic class {0}GeneratedTests {{", cleanName));
						
						foreach (KeyValuePair<string, string> pair in templates) {
							WriteTest(writer,type,new TestTemplate(pair.Key, pair.Value));
						}

						writer.WriteLine("\t}");
						WriteTestFooter(writer);
					}
				}
			}

			return true;
		}
		
		static void WriteTestHeader(TextWriter writer) {
			writer.WriteLine(Utilities.GenerateLicenseHeader());

      writer.WriteLine("using System;");
      writer.WriteLine("using NUnit.Framework;");
      writer.WriteLine();
      writer.WriteLine("namespace SlimDX.Tests {");
		}

		static void WriteTestFooter(TextWriter writer) {
      writer.WriteLine("}");
		}
		
		static void WriteTest(TextWriter writer,Type type,TestTemplate test) {
			writer.WriteLine("\t\t[Test]");
			writer.WriteLine(string.Format("\t\tpublic void {0}() {{",test.Name));
			
			writer.WriteLine(test.Apply(type));
			
			writer.WriteLine("\t\t}");
			writer.WriteLine();
		}
		
		static string CleanTypeName(string fullTypeName) {
			return string.Join(string.Empty, fullTypeName.Split('.'));
		}

		Dictionary<string, string> LoadTemplates() {
			Dictionary<string, string> results = new Dictionary<string, string>();
			Assembly assembly = Assembly.GetExecutingAssembly();

			foreach (string name in assembly.GetManifestResourceNames()) {
				// Assume anything with the .btt (Bulk Test Template) extension
				// is a unit test template.
				if (name.EndsWith(".btt")) {
					string[] parts = name.Split('.');
					results.Add(parts[parts.Length - 2], Utilities.GetResourceText(name));
				}
			}

			return results;
		}
	}

	class TestTemplate {
		string name;
		string code;
		
		public string Name {
			get {
				return name;
			}
		}
		
		public TestTemplate(string name, string xml) {
			this.name = name;

			using (StringReader reader = new StringReader(xml)) {
				XPathDocument xpath = new XPathDocument(reader);
				XPathNavigator root = xpath.CreateNavigator();
				XPathNavigator codeNav = root.SelectSingleNode("/BulkTestTemplate/Test/text()");

				code = codeNav.Value;
			}
		}
		
		public string Apply(Type type) {
			string baked = code;
			baked = baked.Replace("{FullType}",type.FullName);
			
			return baked;
		}
	}
}
