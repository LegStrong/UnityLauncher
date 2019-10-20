using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace UnityLancher
{
    public partial class UnityLancher : Form
    {
        UnityLancherCore core = new UnityLancherCore();
        HotKeys hotKey = new HotKeys();

        bool allowQuit = false;

        public UnityLancher()
        {
            InitializeComponent();

            KeyDown += UnityLancher_KeyDown;
            KeyPreview = true;

            notifyIcon1.MouseClick += NotifyIcon1_MouseClick;

            FormClosing += UnityLancher_FormClosing;

            this.listView1.Columns.Add("Path", 780, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Version", 120, HorizontalAlignment.Left);
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.MouseClick += ListView1_MouseClick;

            RefreshUI();

            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;

            hotKey.Regist(this.Handle, (int)HotKeys.HotkeyModifiers.Alt, Keys.D3, ()=> {
                ShowWindow();
            });
        }

        //重载WndProc函数
        protected override void WndProc(ref Message m)
        {
            hotKey.ProcessHotKey(m);//快捷键消息处理
            base.WndProc(ref m);
        }

        private void UnityLancher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowQuit)
            {
                this.Visible = false;

                e.Cancel = true;
            }
        }

        public void ShowWindow()
        {
            Visible = true;
            RefreshUI();
            this.Activate();
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //WindowState = FormWindowState.Normal;
            if(e.Button == MouseButtons.Right)
            {
                var menu = new ContextMenuStrip();
                notifyIcon1.ContextMenuStrip = menu;
                menu.ItemClicked += NotifyIconMenu_ItemClicked;

                menu.Items.Add("Start on Boot");
                menu.Items.Add("Exit");

                var item = menu.Items[0] as ToolStripMenuItem;
                if(item != null)
                {
                    item.CheckOnClick = true;
                    item.Checked = WinHelper.IsStartOnBoot();
                }
            }
            else
            {
                ShowWindow();
            }
        }

        private void NotifyIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "Exit")
            {
                allowQuit = true;
                Application.Exit();
            }
            else if(e.ClickedItem.Text == "Start on Boot")
            {
                var item = e.ClickedItem as ToolStripMenuItem;
                bool startup = WinHelper.IsStartOnBoot();
                item.Checked = !startup;
                WinHelper.SetStartOnBoot(!startup);
            }
        }

        private void UnityLancher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                core.Reset();
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            listView1.Items.Clear();

            this.listView1.BeginUpdate();

            foreach (var kvp in core.recentProject)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = kvp.Key;
                lvi.SubItems.Add(kvp.Value);
                this.listView1.Items.Add(lvi);
            }

            this.listView1.EndUpdate();

            var menu = new ContextMenuStrip();
            listView1.ContextMenuStrip = menu;
            menu.ItemClicked += Menu_ItemClicked;

            foreach (var kvp in core.versionDict)
            {
                menu.Items.Add("Open With Unity " + kvp.Key);
            }

            //menu.Items.Add("")
        }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var v = e.ClickedItem.Text.Remove(0, "Open With Unity ".Length);
            var items = listView1.SelectedItems;
            if (items.Count != 0)
            {
                var version = v;
                var path = items[0].Text;

                var p = WinHelper.GetProcessUsedFile((path + "\\Temp\\UnityLockfile"));
                if(p != null)
                {
                    p.Kill();
                }
                core.OpenProject(path, version);
            }
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var items = listView1.SelectedItems;
            if (items.Count != 0)
            {
                var version = items[0].SubItems[1].Text;
                var path = items[0].Text;
                core.OpenProject(path, version);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
