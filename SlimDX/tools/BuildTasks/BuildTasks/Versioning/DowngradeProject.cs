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

namespace BuildTasks {
	public class DowngradeProject : Task {
		ITaskItem[] inputProjects;
		ITaskItem[] outputProjects;

		[Required]
		public ITaskItem[] InputProjects {
			get {
				return inputProjects;
			}
			set {
				inputProjects = value;
			}
		}

		[Required]
		public ITaskItem[] OutputProjects {
			get {
				return outputProjects;
			}
			set {
				outputProjects = value;
			}
		}

		public override bool Execute() {
			for ( int fileIndex = 0; fileIndex < inputProjects.Length; ++fileIndex ) {
				if ( !File.Exists( inputProjects[fileIndex].ItemSpec ) ) {
					Log.LogError( string.Format( "Input project '{0}' does not exist.", inputProjects[fileIndex].ItemSpec ) );
					return false;
				}

				XPathDocument doc = new XPathDocument( inputProjects[fileIndex].ItemSpec );
				XPathNavigator root = doc.CreateNavigator();

				using ( StreamWriter writer = new StreamWriter( outputProjects[fileIndex].ItemSpec ) ) {
					foreach ( XPathNavigator element in root.SelectDescendants( XPathNodeType.Element, true ) ) {
						WriteElement( writer, 0, element );
						break;
					}
				}
			}

			return true;
		}

		static void WriteElement( StreamWriter writer, int indent, XPathNavigator element ) {
			string elementPrefix = new string( ' ', indent * 2 );
			string attributePrefix = new string( ' ', indent * 2 + 2 );

			writer.Write( "{0}<{1}", elementPrefix, element.Name );

			foreach ( XPathNavigator attribute in element.Select( "@*" ) ) {
				if ( element.Name == "VisualStudioProject" && attribute.Name == "Version" ) {
					writer.Write( "{0}{1}Version=\"8.00\"", Environment.NewLine, attributePrefix );
				}
				else if ( element.Name == "VisualStudioProject" && attribute.Name == "TargetFrameworkVersion" ) {
					// No op.
				}
				else if ( element.Name == "AssemblyReference" && attribute.Name == "MinFrameworkVersion" ) {
					// No op.
				}
				else if ( element.Name == "ProjectReference" && attribute.Name == "Include" ) {
					string newValue = Path.GetFileNameWithoutExtension( attribute.Value );
					newValue += ".2005" + Path.GetExtension( attribute.Value );
					writer.Write( " Include=\"{0}\"", newValue );
				}
				else if ( element.Name == "Import" && attribute.Name == "Project" ) {
					string newValue = attribute.Value.Replace( "$(MSBuildToolsPath)", "$(MSBuildBinPath)" );
					writer.Write( " Project=\"{0}\"", newValue );
				}
				else {
					writer.Write( "{0}{1}{2}=\"{3}\"", Environment.NewLine, attributePrefix, attribute.Name, attribute.Value );
				}
			}

			if ( element.Name == "Project" )
			{
				XPathNodeIterator ancestors = element.SelectAncestors(XPathNodeType.Element, false);
				if ( ancestors.Count == 0 )
					writer.Write( Environment.NewLine + "  xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\"" );
			}

			if ( element.HasChildren ) {
				writer.Write( ">" );

				bool writeNewline = true;
				foreach ( XPathNavigator child in element.SelectChildren( XPathNodeType.Element ) ) {
					if ( writeNewline )
					{
						writer.WriteLine();
						writeNewline = false;
					}

					WriteElement( writer, indent + 1, child );
				}

				foreach ( XPathNavigator child in element.SelectChildren( XPathNodeType.Text ) ) {
					writer.Write( child.Value );
				}

				if ( !writeNewline )
				{
					writer.Write(elementPrefix);
				}

				writer.WriteLine( "</{0}>", element.Name );
			}
			else {
				writer.WriteLine( "/>" );
			}
		}
	}
}
