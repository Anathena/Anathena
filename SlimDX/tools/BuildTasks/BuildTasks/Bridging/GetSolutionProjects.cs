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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace BuildTasks {
	public class GetSolutionProjects : Task {
		[Required]
		public string SolutionFile {
			get;
			set;
		}

		[Output]
		public ITaskItem[] ProjectFiles {
			get;
			set;
		}

		public override bool Execute() {
			if ( !File.Exists( SolutionFile ) ) {
				Log.LogError( string.Format( "Input solution '{0}' does not exist.", SolutionFile ) );
				return false;
			}

			string solutionText = string.Empty; ;
			using ( StreamReader reader = new StreamReader( SolutionFile ) ) {
				solutionText = reader.ReadToEnd();
			}

			List<ITaskItem> outputs = new List<ITaskItem>();
			Regex regex = new Regex( "^Project\\(.*\\s=\\s\"(?<name>.*)\",\\s\"(?<path>.*)\",", RegexOptions.Multiline );
			foreach ( Match match in regex.Matches( solutionText ) ) {
				string name = match.Groups["name"].Value;
				string path = match.Groups["path"].Value;
				if ( name != path && path.EndsWith( "proj" ) ) {
					outputs.Add( new TaskItem( Path.GetFullPath( Path.Combine( Path.GetDirectoryName( SolutionFile ), path ) ) ) );
				}
			}
		
			ProjectFiles = outputs.ToArray();
			return true;
		}
	}
}
