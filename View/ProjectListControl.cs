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

namespace UnityLancher.View
{


	public partial class ProjectListControl : UserControl
	{
		private UnityProjectController controller;
		private Font titleFont;
		private Font secondFont;

		public ProjectListControl(UnityProjectController controller)
		{
			InitializeComponent();

			this.controller = controller;
			titleFont = new Font(Font.Name, 16, FontStyle.Bold);
			secondFont = new Font(Font.Name, 12);

			listBox1.MouseDoubleClick += ListView1_MouseDoubleClick;
			listBox1.MouseDown += ListBox1_MouseDown;
			listBox1.MouseHover += ListBox1_MouseHover;

			listBox1.DrawMode = DrawMode.OwnerDrawFixed;
			listBox1.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
			listBox1.ItemHeight = 56;

			var menu = new ContextMenuStrip();
			listBox1.ContextMenuStrip = menu;
			menu.ItemClicked += Menu_ItemClicked;

			foreach (var kvp in controller.GetUnitys())
			{
				menu.Items.Add("Open With Unity " + kvp.Key);
			}

			UpdateList();
		}

		private void ListBox1_MouseHover(object sender, EventArgs e)
		{
			//Point point = listBox1.PointToClient(Cursor.Position);
			//int index = listBox1.IndexFromPoint(point);
			//if (index < 0) return;
			////Do any action with the item
			//listBox1.GetItemRectangle(index).Inflate(1, 2);
		}

		private void ListBox1_MouseDown(object sender, MouseEventArgs e)
		{
			listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
		}

		private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

			if (selected)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
			}

			if(e.Index >= 0)
			{
				Brush myBrush = Brushes.Black;
				var item = listBox1.Items[e.Index] as UnityProject;

				var bound = new Rectangle(new Point(e.Bounds.X + 32, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height / 2));
				var projName = System.IO.Path.GetFileName(item.path);
				e.Graphics.DrawString(projName, titleFont, myBrush, bound, StringFormat.GenericDefault);

				myBrush = Brushes.DimGray;
				var bound2 = new Rectangle(new Point(e.Bounds.X + 32, e.Bounds.Y + e.Bounds.Height / 2), new Size(e.Bounds.Width, e.Bounds.Height / 2));

				e.Graphics.DrawString(item.path + " | " + item.version, secondFont, myBrush, bound2, StringFormat.GenericDefault);
			}
			else
			{
				Console.WriteLine("-1");
			}

			e.DrawFocusRectangle();
		}

		private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var items = listBox1.SelectedItems;
			if (items.Count != 0)
			{
				var item = items[0] as UnityProject;
				controller.OpenProject(item.path, item.version);
			}
		}

		public void UpdateList()
		{
			listBox1.Items.Clear();
			this.listBox1.BeginUpdate();

			foreach (var kvp in controller.GetProjects())
			{
				var o = new UnityProject();
				o.path = kvp.Key;
				o.version = kvp.Value;
				listBox1.Items.Add(o);
			}

			this.listBox1.EndUpdate();
		}

		private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var v = e.ClickedItem.Text.Remove(0, "Open With Unity ".Length);
			var items = listBox1.SelectedItems;
			if (items.Count != 0)
			{
				var version = v;
				var item = items[0] as UnityProject;

				var p = WinHelper.GetProcessUsedFile((item.path + "\\Temp\\UnityLockfile"));
				if (p != null)
				{
					p.Kill();
				}
				controller.OpenProject(item.path, version);
			}
		}

		public class UnityProject
		{
			public string path;
			public string version;
		}
	}
}
