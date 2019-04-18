namespace AntiMoyuClient
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.username = new System.Windows.Forms.TextBox();
			this.password = new System.Windows.Forms.TextBox();
			this.label_status = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.checkbox_autostart = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownRest = new System.Windows.Forms.NumericUpDown();
			this.button_rest = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRest)).BeginInit();
			this.SuspendLayout();
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point(141, 12);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(239, 20);
			this.username.TabIndex = 0;
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(141, 38);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(239, 20);
			this.password.TabIndex = 1;
			// 
			// label_status
			// 
			this.label_status.AutoSize = true;
			this.label_status.Location = new System.Drawing.Point(22, 85);
			this.label_status.Name = "label_status";
			this.label_status.Size = new System.Drawing.Size(67, 13);
			this.label_status.TabIndex = 2;
			this.label_status.Text = "状态：空闲";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "用户名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "密码";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(305, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "登录";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
			// 
			// checkbox_autostart
			// 
			this.checkbox_autostart.AutoSize = true;
			this.checkbox_autostart.Location = new System.Drawing.Point(25, 109);
			this.checkbox_autostart.Name = "checkbox_autostart";
			this.checkbox_autostart.Size = new System.Drawing.Size(74, 17);
			this.checkbox_autostart.TabIndex = 6;
			this.checkbox_autostart.Text = "开机启动";
			this.checkbox_autostart.UseVisualStyleBackColor = true;
			this.checkbox_autostart.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 133);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "临时休息(分钟)";
			// 
			// numericUpDownRest
			// 
			this.numericUpDownRest.Location = new System.Drawing.Point(132, 131);
			this.numericUpDownRest.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.numericUpDownRest.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownRest.Name = "numericUpDownRest";
			this.numericUpDownRest.Size = new System.Drawing.Size(147, 20);
			this.numericUpDownRest.TabIndex = 8;
			this.numericUpDownRest.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button_rest
			// 
			this.button_rest.Location = new System.Drawing.Point(305, 128);
			this.button_rest.Name = "button_rest";
			this.button_rest.Size = new System.Drawing.Size(75, 23);
			this.button_rest.TabIndex = 9;
			this.button_rest.Text = "开始休息";
			this.button_rest.UseVisualStyleBackColor = true;
			this.button_rest.Click += new System.EventHandler(this.button_rest_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 157);
			this.Controls.Add(this.button_rest);
			this.Controls.Add(this.numericUpDownRest);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkbox_autostart);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label_status);
			this.Controls.Add(this.password);
			this.Controls.Add(this.username);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "不摸鱼";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRest)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.TextBox password;
		public System.Windows.Forms.Label label_status;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.CheckBox checkbox_autostart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownRest;
		private System.Windows.Forms.Button button_rest;
	}
}

