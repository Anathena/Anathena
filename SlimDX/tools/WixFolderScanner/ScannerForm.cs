using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WixFolderScanner
{
	public partial class ScannerForm : Form
	{
		private string m_startFolder;

		public ScannerForm()
		{
			InitializeComponent();
		}

		void Append(string text)
		{
			OutputBox.AppendText(text);
		}

		string GetId(string baseName)
		{
			return baseName.Replace('\\', '_');
		}

		string ScanFolder(string folder, string[] ignoreExtensions)
		{
			string dirName = folder;
			if(dirName != m_startFolder)
				dirName = folder.Substring(m_startFolder.Length + 1);
			
			int slashPos = dirName.LastIndexOf('\\');
			string shortName = slashPos > 0 ? dirName.Substring(slashPos + 1) : dirName;

			if(shortName == "bin" || shortName == "obj")
				return string.Empty;

			string id = "Samples_" + GetId(dirName);
			string attribs = string.Format("Id=\"{0}\" Name=\"{1}\"", id, shortName);
			string xml = XmlTools.OpenTag("Directory", attribs);
			
			//Create a component and put all the files inside it
			Guid guid = Guid.NewGuid();
			attribs = string.Format("Id=\"{0}\" DiskId=\"{1}\" Guid=\"{2}\"", id, 1, guid.ToString());
			xml += XmlTools.OpenTag("Component", attribs);

			string[] files = Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly);
			foreach(string file in files)
			{
				bool ignore = false;
				foreach(string ext in ignoreExtensions)
				{
					if(file.EndsWith(ext))
					{
						ignore = true;
						break;
					}
				}
				if(ignore)
					continue;

				string fileName = file.Substring(folder.Length + 1);
				string fileId = GetId(id + fileName);
				attribs = string.Format("Id=\"{0}\" Name=\"{1}\" Source=\"{2}\"", fileId, fileName, file);

				xml += XmlTools.SelfClosingTag("File", attribs);
			}

			xml += XmlTools.CloseTag();

			//Process subdirectories
			string[] dirs = Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly);
			foreach(string dir in dirs)
			{
				var fileAttribs = File.GetAttributes(dir);
				if((fileAttribs & FileAttributes.Hidden) == FileAttributes.Hidden)
					continue;

				xml += ScanFolder(dir, ignoreExtensions);
			}

			xml += XmlTools.CloseTag();
			return xml;
		}

		private void ScanButton_Click(object sender, EventArgs e)
		{
			if(!Directory.Exists(FolderBox.Text))
				return;

			string[] extensions = ExtensionsBox.Text.Split(' ', ',');
			m_startFolder = FolderBox.Text;
			string text = ScanFolder(m_startFolder, extensions);
			Append(text);
		}
	}
}
