using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Reflection;


namespace Gecko
{
	/// <summary>
	/// Search /usr/lib directory for the latest xulrunner.
	/// Currently looks for xulrunner-1.9.2 variants in /usr/lib.
	/// Returns null if no xulrunner-1.9.2 directory is found.
	/// xulrunner doesn't come with a .pc file so I can't use pkg-config to find location of xulrunner.
	/// </summary>
	public static class XULRunnerLocator
	{
		private static string FindSpecialXulRunnerDirectory(string applicationPath, string hintPath)
		{
			string rootPath = Path.GetPathRoot(applicationPath);
			string path = applicationPath;

			while (!rootPath.Equals(path, StringComparison.CurrentCultureIgnoreCase))
			{
				var candinate = Path.Combine(path, hintPath);
				if (Directory.Exists(candinate))
					return candinate;
				path = Path.GetDirectoryName(path);				
			}
			return null;
		}

		private static string GetXULRunnerLocationLinux(string hintPath)
		{
			string solutionXulRunnerFolder = FindSpecialXulRunnerDirectory(DirectoryOfTheApplicationExecutable, hintPath);
			if (solutionXulRunnerFolder != null)
				return solutionXulRunnerFolder;

			string version = typeof(XULRunnerLocator).Assembly.GetName().Version.Major.ToString();
			string[] folders = new string[] {
				"/usr/lib/xulrunner-geckofx-" + version,
				"/usr/lib/xulrunner-" + version,
				"/usr/lib/firefox"
			};
			for (int i = 0; i < folders.Length; ++i)
			{
				if (Directory.Exists(folders[i]))
					return folders[i];
			}
			throw new ApplicationException("/usr/lib/firefox doesn't exist");
		}

		private static string GetXULRunnerLocationWindows(string hintPath)
		{
			//NB for shipping apps, we don't have a way to find their xulrunner, so they won't be running this code 
			//unless they depend on the customer installing a certain verion of Firefox and keeping it from auto-updating.
			//So this is more for unit tests and geckofx sample apps.

			string solutionXulRunnerFolder = FindSpecialXulRunnerDirectory(DirectoryOfTheApplicationExecutable, hintPath);
			if (solutionXulRunnerFolder != null) 
				return solutionXulRunnerFolder;

			//look for firefox itself

			string version = typeof(XULRunnerLocator).Assembly.GetName().Version.Major.ToString();

			string[] folderSearch = new string[] { "Mozilla Firefox " + version + ".0", "Mozilla Firefox " + version + ".0a", "Mozilla Firefox " + version, "Mozilla Firefox" };

			var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			return folderSearch.Select(t => Path.Combine(programFiles, t)).FirstOrDefault(Directory.Exists);
		}

		private static string DirectoryOfTheApplicationExecutable
		{
			get
			{
				string path;
				bool unitTesting = Assembly.GetEntryAssembly() == null;
				if (unitTesting)
				{
					path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
					path = Uri.UnescapeDataString(path);
				}
				else
				{
					path = AppDomain.CurrentDomain.BaseDirectory;					
				}
				return Directory.GetParent(path).FullName;
			}
		}

		/// <summary>
		/// Get xulrunner's directory.
		/// </summary>
		/// <param name="hintPath">A hint path to search the xulrunner with. Should be a relative path from any ancestors of the executable.</param>
		/// <returns></returns>
		public static string GetXULRunnerLocation(string hintPath = "PutXulRunnerFolderHere/xulrunner")
		{
			return Xpcom.IsLinux
				? GetXULRunnerLocationLinux(hintPath)
				: GetXULRunnerLocationWindows(hintPath);
		}
	}
}
