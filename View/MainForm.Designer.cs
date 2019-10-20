namespace UnityLancher.View
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel_Top = new System.Windows.Forms.Panel();
			this.button_New = new System.Windows.Forms.Button();
			this.button_Open = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel_Bottom = new System.Windows.Forms.Panel();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.panel_Top.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel_Top, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel_Bottom, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 541);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// panel_Top
			// 
			this.panel_Top.BackColor = System.Drawing.SystemColors.Control;
			this.panel_Top.Controls.Add(this.button_New);
			this.panel_Top.Controls.Add(this.button_Open);
			this.panel_Top.Controls.Add(this.label1);
			this.panel_Top.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Top.Location = new System.Drawing.Point(3, 3);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.Size = new System.Drawing.Size(938, 74);
			this.panel_Top.TabIndex = 0;
			// 
			// button_New
			// 
			this.button_New.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button_New.Location = new System.Drawing.Point(785, 27);
			this.button_New.Name = "button_New";
			this.button_New.Size = new System.Drawing.Size(90, 32);
			this.button_New.TabIndex = 4;
			this.button_New.Text = "New";
			this.button_New.UseVisualStyleBackColor = true;
			this.button_New.Click += new System.EventHandler(this.Button_New_Click);
			// 
			// button_Open
			// 
			this.button_Open.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button_Open.Location = new System.Drawing.Point(660, 27);
			this.button_Open.Name = "button_Open";
			this.button_Open.Size = new System.Drawing.Size(90, 32);
			this.button_Open.TabIndex = 3;
			this.button_Open.Text = "Open";
			this.button_Open.UseVisualStyleBackColor = true;
			this.button_Open.Click += new System.EventHandler(this.Button_Open_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(21, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Unity Lancher";
			// 
			// panel_Bottom
			// 
			this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Bottom.Location = new System.Drawing.Point(3, 83);
			this.panel_Bottom.Name = "panel_Bottom";
			this.panel_Bottom.Size = new System.Drawing.Size(938, 455);
			this.panel_Bottom.TabIndex = 1;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "UnityLancher";
			this.notifyIcon1.Visible = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 541);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "UnityLancher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel_Top;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_New;
		private System.Windows.Forms.Button button_Open;
		private System.Windows.Forms.Panel panel_Bottom;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
	}
}