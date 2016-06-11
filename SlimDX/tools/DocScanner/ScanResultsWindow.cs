using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace DocScanner
{
	[Flags]
	public enum MemberFilterFlags
	{
		None = 0,
		Namespaces = 0x01,
		Types = 0x02,
		Methods = 0x04,
		Properties = 0x08,
		Fields = 0x10,
		Events = 0x20
	}

	public partial class ScanResultsWindow : Form
	{
		SymbolEngine m_Symbols;

		public ScanResultsWindow()
		{
			InitializeComponent();
		}

		bool CheckNameAgainstFilter(string name, MemberFilterFlags filter)
		{
			if((filter & MemberFilterFlags.Namespaces) == MemberFilterFlags.None && name.StartsWith("N:"))
			{
				return false;
			}

			if((filter & MemberFilterFlags.Types) == MemberFilterFlags.None && name.StartsWith("T:"))
			{
				return false;
			}

			if((filter & MemberFilterFlags.Methods) == MemberFilterFlags.None && name.StartsWith("M:"))
			{
				return false;
			}

			if((filter & MemberFilterFlags.Properties) == MemberFilterFlags.None && name.StartsWith("P:"))
			{
				return false;
			}

			if((filter & MemberFilterFlags.Fields) == MemberFilterFlags.None && name.StartsWith("F:"))
			{
				return false;
			}

			if((filter & MemberFilterFlags.Events) == MemberFilterFlags.None && name.StartsWith("E:"))
			{
				return false;
			}

			return true;
		}

		public void Process(string assemblyPath, IEnumerable<string> tagsPresent, IEnumerable<string> tagsMissing, MemberFilterFlags filter)
		{
			ResultsList.Items.Clear();
			string xmlPath = Path.ChangeExtension(assemblyPath, "xml");

			XmlDocument docs = new XmlDocument();
			docs.Load(xmlPath);

			XmlNodeList nodes = docs.SelectNodes("/doc/members/member");
			foreach(XmlNode node in nodes)
			{
				string name = node.Attributes["name"].Value;
				if(!CheckNameAgainstFilter(name, filter))
					continue;

				bool addItem = true;

				//check if it contains any tags that are supposed to be missing
				foreach(string tag in tagsMissing)
				{
					XmlNode tagNode = node.SelectSingleNode(tag);
					if(tagNode != null && tagNode.InnerText != string.Empty)
					{
						addItem = false;
						break;
					}
				}

				//If we've already decided not to add this, don't keep checking the node
				if(!addItem)
					continue;

				//check if it's missing any tags that are supposed to be present
				foreach(string tag in tagsPresent)
				{
					XmlNode tagNode = node.SelectSingleNode(tag);
					if(tagNode == null || tagNode.InnerText == string.Empty)
					{
						addItem = false;
						break;
					}
				}

				//it checks out, add it to the list
				if(addItem)
					ResultsList.Items.Add(name);
			}

			m_Symbols = new SymbolEngine(assemblyPath);
		}

		private void CopyMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText((string) ResultsList.SelectedItem, TextDataFormat.Text);
		}

		private void CopyAllMenuItem_Click(object sender, EventArgs e)
		{
			string text = string.Empty;

			foreach(string item in ResultsList.Items)
			{
				text += item + Environment.NewLine;
			}

			Clipboard.SetText(text, TextDataFormat.Text);
		}

		private void ResultsList_MouseUp(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				int index = ResultsList.IndexFromPoint(e.X, e.Y);
				if(index == ListBox.NoMatches)
					return;

				ResultsList.SelectedIndex = index;
				string file = m_Symbols.GetFileForSymbol((string) ResultsList.SelectedItem);
				GoToHeaderMenuItem.Enabled = file != null;
				ItemContextMenu.Show(ResultsList, e.X, e.Y);
			}
		}

		private void OpenHeaderFile()
		{
			string file = m_Symbols.GetFileForSymbol((string) ResultsList.SelectedItem);
			if(file != null)
			{
				//SlimDX doc comments always live in the header but the SymbolEngine
				//almost always returns source files, so we try to find the header instead
				string header = Path.ChangeExtension(file, "h");
				if(File.Exists(header))
					file = header;

				System.Diagnostics.Process.Start(file);
			}
		}

		private void ResultsList_DoubleClick(object sender, EventArgs e)
		{
			OpenHeaderFile();
		}

		private void GoToHeaderMenuItem_Click(object sender, EventArgs e)
		{
			OpenHeaderFile();
		}
	}
}
