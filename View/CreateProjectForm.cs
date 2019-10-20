using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityLancher.Controller;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace UnityLancher
{
	public partial class CreateProjectForm : UserControl
	{
		private UnityProjectController controller;

		public System.Action onHide;

		public CreateProjectForm(UnityProjectController controller)
		{
			InitializeComponent();

			this.controller = controller;
			UpdateView();
		}

		public void UpdateView()
		{
			textBox_ProjectName.Text = "NewProject";
			comboBox_UnityVersion.Items.Clear();

			int index = 0;
			int cnt = 0;
			var prefVersion = Prefs.GetPrefVersion();

			foreach (var kvp in controller.GetUnitys())
			{
				comboBox_UnityVersion.Items.Add(kvp.Key);
				if (kvp.Key == prefVersion)
				{
					index = cnt;
				}
				cnt++;
			}

			if (comboBox_UnityVersion.Items.Count >= 0)
				comboBox_UnityVersion.SelectedIndex = index;

			textBox_ProjectPath.Text = Prefs.GetPrefProjectPath();
		}

		private void Button_Cancel_Click(object sender, EventArgs e)
		{
			if (onHide != null)
			{
				onHide();
			}
		}

		private void Button_Create_Click(object sender, EventArgs e)
		{
			var path = textBox_ProjectPath.Text + "\\" + textBox_ProjectName.Text;
			var version = comboBox_UnityVersion.SelectedItem.ToString();

			Prefs.SetString("ProjectPath", textBox_ProjectPath.Text);
			Prefs.SetString("UnityVersion", version);
			controller.CreateProject(path, version);

			if (onHide != null)
			{
				onHide();
			}
		}

		private void Button_SelectProjectPath_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.InitialDirectory = textBox_ProjectPath.Text;
			dialog.IsFolderPicker = true;
			dialog.EnsureValidNames = false;

			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				textBox_ProjectPath.Text = dialog.FileName;
			}
		}
	}
}
