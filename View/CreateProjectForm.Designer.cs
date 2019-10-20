namespace UnityLancher
{
	partial class CreateProjectForm
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox_ProjectName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_ProjectPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button_Cancel = new System.Windows.Forms.Button();
			this.button_Create = new System.Windows.Forms.Button();
			this.comboBox_UnityVersion = new System.Windows.Forms.ComboBox();
			this.button_SelectProjectPath = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_ProjectName
			// 
			this.textBox_ProjectName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox_ProjectName.Location = new System.Drawing.Point(135, 104);
			this.textBox_ProjectName.Name = "textBox_ProjectName";
			this.textBox_ProjectName.Size = new System.Drawing.Size(315, 29);
			this.textBox_ProjectName.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(133, 79);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Project Name";
			// 
			// textBox_ProjectPath
			// 
			this.textBox_ProjectPath.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox_ProjectPath.Location = new System.Drawing.Point(135, 206);
			this.textBox_ProjectPath.Name = "textBox_ProjectPath";
			this.textBox_ProjectPath.Size = new System.Drawing.Size(315, 29);
			this.textBox_ProjectPath.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(569, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 15);
			this.label2.TabIndex = 4;
			this.label2.Text = "Unity Version";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(133, 180);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Project Path";
			// 
			// button_Cancel
			// 
			this.button_Cancel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
			this.button_Cancel.Location = new System.Drawing.Point(503, 332);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 34);
			this.button_Cancel.TabIndex = 8;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
			// 
			// button_Create
			// 
			this.button_Create.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button_Create.Location = new System.Drawing.Point(609, 332);
			this.button_Create.Name = "button_Create";
			this.button_Create.Size = new System.Drawing.Size(161, 34);
			this.button_Create.TabIndex = 9;
			this.button_Create.Text = "Create Project";
			this.button_Create.UseVisualStyleBackColor = true;
			this.button_Create.Click += new System.EventHandler(this.Button_Create_Click);
			// 
			// comboBox_UnityVersion
			// 
			this.comboBox_UnityVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_UnityVersion.Font = new System.Drawing.Font("微软雅黑", 12F);
			this.comboBox_UnityVersion.FormattingEnabled = true;
			this.comboBox_UnityVersion.Location = new System.Drawing.Point(572, 104);
			this.comboBox_UnityVersion.Name = "comboBox_UnityVersion";
			this.comboBox_UnityVersion.Size = new System.Drawing.Size(121, 29);
			this.comboBox_UnityVersion.TabIndex = 10;
			// 
			// button_SelectProjectPath
			// 
			this.button_SelectProjectPath.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F);
			this.button_SelectProjectPath.Location = new System.Drawing.Point(405, 207);
			this.button_SelectProjectPath.Name = "button_SelectProjectPath";
			this.button_SelectProjectPath.Size = new System.Drawing.Size(44, 27);
			this.button_SelectProjectPath.TabIndex = 11;
			this.button_SelectProjectPath.Text = "...";
			this.button_SelectProjectPath.UseVisualStyleBackColor = true;
			this.button_SelectProjectPath.Click += new System.EventHandler(this.Button_SelectProjectPath_Click);
			// 
			// CreateProjectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button_SelectProjectPath);
			this.Controls.Add(this.comboBox_UnityVersion);
			this.Controls.Add(this.button_Create);
			this.Controls.Add(this.button_Cancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox_ProjectPath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_ProjectName);
			this.Name = "CreateProjectForm";
			this.Size = new System.Drawing.Size(798, 463);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_ProjectName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_ProjectPath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button_Cancel;
		private System.Windows.Forms.Button button_Create;
		private System.Windows.Forms.ComboBox comboBox_UnityVersion;
		private System.Windows.Forms.Button button_SelectProjectPath;
	}
}
