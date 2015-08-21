namespace MouseHelper
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_locky = new System.Windows.Forms.CheckBox();
            this.chk_lockx = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_stop = new System.Windows.Forms.TextBox();
            this.txt_start = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericHour = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericMill = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericSecond = new System.Windows.Forms.NumericUpDown();
            this.numericSecond1 = new System.Windows.Forms.NumericUpDown();
            this.numericSecond2 = new System.Windows.Forms.NumericUpDown();
            this.cmb_maction = new System.Windows.Forms.ComboBox();
            this.cmb_mpress = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_my = new System.Windows.Forms.TextBox();
            this.txt_mx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_stop = new System.Windows.Forms.Button();
            this.lbl_version = new System.Windows.Forms.Label();
            this.linkLbl_version = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(297, 178);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(43, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_locky);
            this.groupBox1.Controls.Add(this.chk_lockx);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmb_maction);
            this.groupBox1.Controls.Add(this.cmb_mpress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_my);
            this.groupBox1.Controls.Add(this.txt_mx);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自定义功能区";
            // 
            // chk_locky
            // 
            this.chk_locky.AutoSize = true;
            this.chk_locky.Location = new System.Drawing.Point(109, 50);
            this.chk_locky.Name = "chk_locky";
            this.chk_locky.Size = new System.Drawing.Size(42, 16);
            this.chk_locky.TabIndex = 10;
            this.chk_locky.Text = "锁Y";
            this.chk_locky.UseVisualStyleBackColor = true;
            this.chk_locky.CheckedChanged += new System.EventHandler(this.chk_locky_CheckedChanged);
            // 
            // chk_lockx
            // 
            this.chk_lockx.AutoSize = true;
            this.chk_lockx.Location = new System.Drawing.Point(109, 21);
            this.chk_lockx.Name = "chk_lockx";
            this.chk_lockx.Size = new System.Drawing.Size(42, 16);
            this.chk_lockx.TabIndex = 9;
            this.chk_lockx.Text = "锁X";
            this.chk_lockx.UseVisualStyleBackColor = true;
            this.chk_lockx.CheckedChanged += new System.EventHandler(this.chk_lockx_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_stop);
            this.groupBox3.Controls.Add(this.txt_start);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(10, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 66);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "快捷键";
            // 
            // txt_stop
            // 
            this.txt_stop.Location = new System.Drawing.Point(126, 38);
            this.txt_stop.Name = "txt_stop";
            this.txt_stop.Size = new System.Drawing.Size(66, 21);
            this.txt_stop.TabIndex = 3;
            this.txt_stop.Text = "F4";
            this.txt_stop.TextChanged += new System.EventHandler(this.txt_stop_TextChanged);
            // 
            // txt_start
            // 
            this.txt_start.Location = new System.Drawing.Point(126, 14);
            this.txt_start.Name = "txt_start";
            this.txt_start.Size = new System.Drawing.Size(66, 21);
            this.txt_start.TabIndex = 2;
            this.txt_start.Text = "F3";
            this.txt_start.TextChanged += new System.EventHandler(this.txt_start_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "停止快捷键： Ctrl + ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "启动快捷键： Ctrl + ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericHour);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericMill);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numericSecond);
            this.groupBox2.Controls.Add(this.numericSecond1);
            this.groupBox2.Controls.Add(this.numericSecond2);
            this.groupBox2.Location = new System.Drawing.Point(229, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 66);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "点击间隔";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(186, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "1/100秒";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "小时";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "1/10秒";
            // 
            // numericHour
            // 
            this.numericHour.Location = new System.Drawing.Point(8, 36);
            this.numericHour.Name = "numericHour";
            this.numericHour.Size = new System.Drawing.Size(38, 21);
            this.numericHour.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(94, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "秒";
            // 
            // numericMill
            // 
            this.numericMill.Location = new System.Drawing.Point(52, 36);
            this.numericMill.Name = "numericMill";
            this.numericMill.Size = new System.Drawing.Size(38, 21);
            this.numericMill.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "分钟";
            // 
            // numericSecond
            // 
            this.numericSecond.Location = new System.Drawing.Point(96, 36);
            this.numericSecond.Name = "numericSecond";
            this.numericSecond.Size = new System.Drawing.Size(38, 21);
            this.numericSecond.TabIndex = 10;
            this.numericSecond.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericSecond1
            // 
            this.numericSecond1.Location = new System.Drawing.Point(140, 36);
            this.numericSecond1.Name = "numericSecond1";
            this.numericSecond1.Size = new System.Drawing.Size(42, 21);
            this.numericSecond1.TabIndex = 11;
            // 
            // numericSecond2
            // 
            this.numericSecond2.Location = new System.Drawing.Point(186, 36);
            this.numericSecond2.Name = "numericSecond2";
            this.numericSecond2.Size = new System.Drawing.Size(47, 21);
            this.numericSecond2.TabIndex = 12;
            // 
            // cmb_maction
            // 
            this.cmb_maction.FormattingEnabled = true;
            this.cmb_maction.Items.AddRange(new object[] {
            "单击",
            "双击"});
            this.cmb_maction.Location = new System.Drawing.Point(264, 47);
            this.cmb_maction.Name = "cmb_maction";
            this.cmb_maction.Size = new System.Drawing.Size(53, 20);
            this.cmb_maction.TabIndex = 7;
            // 
            // cmb_mpress
            // 
            this.cmb_mpress.FormattingEnabled = true;
            this.cmb_mpress.Items.AddRange(new object[] {
            "左键",
            "右键",
            "中键"});
            this.cmb_mpress.Location = new System.Drawing.Point(264, 18);
            this.cmb_mpress.Name = "cmb_mpress";
            this.cmb_mpress.Size = new System.Drawing.Size(53, 20);
            this.cmb_mpress.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "操作：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "按键：";
            // 
            // txt_my
            // 
            this.txt_my.Location = new System.Drawing.Point(32, 46);
            this.txt_my.Name = "txt_my";
            this.txt_my.Size = new System.Drawing.Size(70, 21);
            this.txt_my.TabIndex = 3;
            // 
            // txt_mx
            // 
            this.txt_mx.Location = new System.Drawing.Point(32, 17);
            this.txt_mx.Name = "txt_mx";
            this.txt_mx.Size = new System.Drawing.Size(70, 21);
            this.txt_mx.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X：";
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(346, 178);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(43, 23);
            this.btn_stop.TabIndex = 2;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(399, 183);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(17, 12);
            this.lbl_version.TabIndex = 3;
            this.lbl_version.Text = "v1";
            // 
            // linkLbl_version
            // 
            this.linkLbl_version.AutoSize = true;
            this.linkLbl_version.Location = new System.Drawing.Point(8, 188);
            this.linkLbl_version.Name = "linkLbl_version";
            this.linkLbl_version.Size = new System.Drawing.Size(0, 12);
            this.linkLbl_version.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 209);
            this.Controls.Add(this.linkLbl_version);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_start);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "鼠标助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecond2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_my;
        private System.Windows.Forms.TextBox txt_mx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_maction;
        private System.Windows.Forms.ComboBox cmb_mpress;
        private System.Windows.Forms.NumericUpDown numericMill;
        private System.Windows.Forms.NumericUpDown numericHour;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericSecond2;
        private System.Windows.Forms.NumericUpDown numericSecond1;
        private System.Windows.Forms.NumericUpDown numericSecond;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_stop;
        private System.Windows.Forms.TextBox txt_start;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.CheckBox chk_locky;
        private System.Windows.Forms.CheckBox chk_lockx;
        private System.Windows.Forms.LinkLabel linkLbl_version;
    }
}

