using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityLancher.Controller;

namespace UnityLancher.View
{
	public partial class MainForm : Form
	{
		private ProjectListControl projectListControl;
		private CreateProjectForm createProjectControl;

		private UnityProjectController controller;

		private Control mainControl;

		HotKeys hotKey = new HotKeys();

		bool allowQuit = false;

		public MainForm()
		{
			InitializeComponent();

			controller = new UnityProjectController();
			controller.mainForm = this;

			projectListControl = new ProjectListControl(controller);
			createProjectControl = new CreateProjectForm(controller);
			createProjectControl.onHide += () =>
			{
				SetMainControl(projectListControl);
			};

			SetMainControl(projectListControl);

			this.ShowInTaskbar = false;

			//notify icon
			this.notifyIcon1.Visible = true;
			notifyIcon1.MouseClick += NotifyIcon1_MouseClick;
			var menu = new ContextMenuStrip();
			notifyIcon1.ContextMenuStrip = menu;
			menu.ItemClicked += NotifyIconMenu_ItemClicked;

			menu.Items.Add("Start on Boot");
			menu.Items.Add("Exit");

			var item = menu.Items[0] as ToolStripMenuItem;
			if (item != null)
			{
				item.CheckOnClick = true;
				item.Checked = WinHelper.IsStartOnBoot();
			}

			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private void SetMainControl(Control ctrl)
		{
			if(mainControl != null)
			{
				mainControl.Visible = false;
			}

			mainControl = ctrl;
			ctrl.Parent = this.panel_Bottom;
			ctrl.Dock = DockStyle.Fill;
			ctrl.Visible = true;
		}

		private void NotifyIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Exit")
			{
				allowQuit = true;
				Application.Exit();
			}
			else if (e.ClickedItem.Text == "Start on Boot")
			{
				var item = e.ClickedItem as ToolStripMenuItem;
				bool startup = WinHelper.IsStartOnBoot();

				Console.WriteLine("item.Checked: " + item.Checked);
				WinHelper.SetStartOnBoot(!startup);
			}
		}

		private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ShowWindow();
				if (mainControl == projectListControl)
				{
					projectListControl.UpdateList();
				}
			}
		}

		public void ShowWindow()
		{
			Visible = true;

			this.Activate();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!allowQuit)
			{
				this.Visible = false;
				e.Cancel = true;
			}
		}

		private void Button_Open_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			//dialog.InitialDirectory = ;
			dialog.IsFolderPicker = true;
			dialog.FolderChanging += Dialog_FolderChanging;
			dialog.EnsureValidNames = false;

			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				controller.OpenProject(dialog.FileName);
			}
		}

		private void Dialog_FolderChanging(object sender, CommonFileDialogFolderChangeEventArgs e)
		{
			CommonOpenFileDialog dialog = sender as CommonOpenFileDialog;
		}

		/// <summary>
		/// Create Project
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_New_Click(object sender, EventArgs e)
		{
			SetMainControl(createProjectControl);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 116)
			{
				if(mainControl == projectListControl)
				{
					projectListControl.UpdateList();
				}
			}
		}
	}
}
