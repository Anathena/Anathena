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
using System.IO;
using System.Xml.XPath;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Text.RegularExpressions;

namespace BuildTasks {
	public class DowngradeSolution : Task {
		ITaskItem[] inputSolutions;
		ITaskItem[] outputSolutions;

		static readonly string sourceFormatVersion = "Microsoft Visual Studio Solution File, Format Version 10.00";
		static readonly string sourceToolVersion = "# Visual Studio 2008";
		static readonly string targetFormatVersion = "Microsoft Visual Studio Solution File, Format Version 9.00";
		static readonly string targetToolVersion = "# Visual Studio 2005";

		[Required]
		public ITaskItem[] InputSolutions {
			get {
				return inputSolutions;
			}
			set {
				inputSolutions = value;
			}
		}

		[Required]
		public ITaskItem[] OutputSolutions {
			get {
				return outputSolutions;
			}
			set {
				outputSolutions = value;
			}
		}

		public override bool Execute() {
			for ( int fileIndex = 0; fileIndex < inputSolutions.Length; ++fileIndex ) {
				if ( !File.Exists( inputSolutions[fileIndex].ItemSpec ) ) {
					Log.LogError( string.Format( "Input solution file '{0}' does not exist.", inputSolutions[fileIndex].ItemSpec ) );
					return false;
				}

				string[] lines = File.ReadAllLines( inputSolutions[fileIndex].ItemSpec );
				for ( int lineIndex = 0; lineIndex < lines.Length; ++lineIndex ) {
					if ( lines[lineIndex].StartsWith( sourceFormatVersion ) ) {
						lines[lineIndex] = targetFormatVersion;
					}
					else if ( lines[lineIndex].StartsWith( sourceToolVersion ) ) {
						lines[lineIndex] = targetToolVersion;
					}
					else if ( lines[lineIndex].StartsWith( "Project" ) ) {
						lines[lineIndex] = lines[lineIndex].Replace(".vcproj", ".2005.vcproj");
						lines[lineIndex] = lines[lineIndex].Replace(".csproj", ".2005.csproj");
					}
				}

				File.WriteAllLines( outputSolutions[fileIndex].ItemSpec, lines );
			}

			return true;
		}
	}
}
