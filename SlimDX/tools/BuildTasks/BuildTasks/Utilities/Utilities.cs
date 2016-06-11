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

using System.Text;
using System.Reflection;
using System.IO;

namespace BuildTasks {
	public static class Utilities {
		public static string GenerateLicenseHeader() {
			StringBuilder builder = new StringBuilder();
			
			builder.AppendLine("/*");
			builder.AppendLine("* Copyright (c) 2007-2008 SlimDX Group");
			builder.AppendLine("* ");
			builder.AppendLine("* Permission is hereby granted, free of charge, to any person obtaining a copy");
			builder.AppendLine("* of this software and associated documentation files (the \"Software\"), to deal");
			builder.AppendLine("* in the Software without restriction, including without limitation the rights");
			builder.AppendLine("* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell");
			builder.AppendLine("* copies of the Software, and to permit persons to whom the Software is");
			builder.AppendLine("* furnished to do so, subject to the following conditions:");
			builder.AppendLine("* ");
			builder.AppendLine("* The above copyright notice and this permission notice shall be included in");
			builder.AppendLine("* all copies or substantial portions of the Software.");
			builder.AppendLine("* ");
			builder.AppendLine("* THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR");
			builder.AppendLine("* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,");
			builder.AppendLine("* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE");
			builder.AppendLine("* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER");
			builder.AppendLine("* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,");
			builder.AppendLine("* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN");
			builder.AppendLine("* THE SOFTWARE.");
			builder.AppendLine("*/");
			
			return builder.ToString();
		}
		
		public static string GetResourceText( string name ) {
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream stream = assembly.GetManifestResourceStream( name );
			if( stream == null ) {
				throw new FileNotFoundException("Embedded resource file not found.",name);
			}
			
			using(StreamReader reader = new StreamReader(stream)) {
				return reader.ReadToEnd();
			}
		}
	}
}
