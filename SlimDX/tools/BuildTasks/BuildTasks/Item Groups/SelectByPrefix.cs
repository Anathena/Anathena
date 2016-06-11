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
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;

namespace BuildTasks {
	public class SelectByPrefix : Task {
		[Required]
		public string Prefix {
			get;
			set;
		}

		[Required]
		public ITaskItem[] InputItems {
			get;
			set;
		}

		[Output]
		public ITaskItem[] OutputItems {
			get;
			set;
		}

		public override bool Execute() {
			List<ITaskItem> outputs = new List<ITaskItem>();
			string prefix = Path.GetFullPath( Prefix );
			
			foreach ( ITaskItem input in InputItems ) {
				string item = Path.GetFullPath( input.ItemSpec );
				if ( item.StartsWith( prefix ) ) {
					TaskItem output = new TaskItem( item );
					output.SetMetadata( "UniquePart", item.Remove( 0, prefix.Length ) );
					outputs.Add( output );
				}
			}

			OutputItems = outputs.ToArray();
			return true;
		}
	}
}
