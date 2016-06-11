using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace DocScanner
{
	public partial class ScannerConfigWindow : Form
	{
		public ScannerConfigWindow()
		{
			InitializeComponent();
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			DialogResult result = openDialog.ShowDialog(this);
			if(result != DialogResult.OK)
				return;

			string assemblyPath = openDialog.FileName;

			string xmlPath = Path.ChangeExtension(assemblyPath, "xml");
			if(!File.Exists(xmlPath))
			{
				MessageBox.Show("WARNING: Unable to find XML documentation file for " + assemblyPath,
					"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			string pdbPath = Path.ChangeExtension(assemblyPath, "pdb");
			if(!File.Exists(pdbPath))
			{
				MessageBox.Show("WARNING: Unable to find program database file (pdb) for " + assemblyPath,
					"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			AssemblyPathTextBox.Text = assemblyPath;
		}

		private void AddTagToPresentButton_Click(object sender, EventArgs e)
		{
			string item = (string) AvailableTagsList.SelectedItem;
			if(item == null)
				return;

			TagsPresentList.Items.Add(item);
			AvailableTagsList.Items.Remove(item);
		}

		private void RemoveTagFromPresentButton_Click(object sender, EventArgs e)
		{
			string item = (string) TagsPresentList.SelectedItem;
			if(item == null)
				return;

			AvailableTagsList.Items.Add(item);
			TagsPresentList.Items.Remove(item);
		}

		private void AddTagToMissingButton_Click(object sender, EventArgs e)
		{
			string item = (string) AvailableTagsList.SelectedItem;
			if(item == null)
				return;

			TagsMissingList.Items.Add(item);
			AvailableTagsList.Items.Remove(item);
		}

		private void RemoveTagFromMissingButton_Click(object sender, EventArgs e)
		{
			string item = (string) TagsMissingList.SelectedItem;
			if(item == null)
				return;

			AvailableTagsList.Items.Add(item);
			TagsMissingList.Items.Remove(item);
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			MemberFilterFlags filter = MemberFilterFlags.None;
			if(NamespacesCheckBox.Checked)
				filter |= MemberFilterFlags.Namespaces;
			if(TypesCheckBox.Checked)
				filter |= MemberFilterFlags.Types;
			if(MethodsCheckBox.Checked)
				filter |= MemberFilterFlags.Methods;
			if(PropertiesCheckBox.Checked)
				filter |= MemberFilterFlags.Properties;
			if(FieldsCheckBox.Checked)
				filter |= MemberFilterFlags.Fields;
			if(EventsCheckBox.Checked)
				filter |= MemberFilterFlags.Events;

			ScanResultsWindow results = new ScanResultsWindow();
			results.Show();
			results.Process(AssemblyPathTextBox.Text,
				from object present in TagsPresentList.Items select (string) present,
				from object missing in TagsMissingList.Items select (string) missing,
				filter
			);
		}
	}
}
