namespace BPMCodeGen
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.richSetup = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.MobTxt = new System.Windows.Forms.TextBox();
            this.mobBtn = new System.Windows.Forms.Button();
            this.mobComb = new System.Windows.Forms.ComboBox();
            this.richMob = new System.Windows.Forms.RichTextBox();
            this.webB1 = new System.Windows.Forms.WebBrowser();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择一个模板";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "请选择模板";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(39, 59);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(831, 178);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(515, 23);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "保存文件";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // richSetup
            // 
            this.richSetup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richSetup.Location = new System.Drawing.Point(39, 256);
            this.richSetup.Name = "richSetup";
            this.richSetup.Size = new System.Drawing.Size(831, 188);
            this.richSetup.TabIndex = 5;
            this.richSetup.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "生成代码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MobTxt
            // 
            this.MobTxt.Location = new System.Drawing.Point(613, 25);
            this.MobTxt.Name = "MobTxt";
            this.MobTxt.Size = new System.Drawing.Size(100, 21);
            this.MobTxt.TabIndex = 7;
            // 
            // mobBtn
            // 
            this.mobBtn.Location = new System.Drawing.Point(731, 22);
            this.mobBtn.Name = "mobBtn";
            this.mobBtn.Size = new System.Drawing.Size(75, 23);
            this.mobBtn.TabIndex = 8;
            this.mobBtn.Text = "选择子模板";
            this.mobBtn.UseVisualStyleBackColor = true;
            this.mobBtn.Click += new System.EventHandler(this.mobBtn_Click);
            // 
            // mobComb
            // 
            this.mobComb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobComb.FormattingEnabled = true;
            this.mobComb.Location = new System.Drawing.Point(850, 25);
            this.mobComb.Name = "mobComb";
            this.mobComb.Size = new System.Drawing.Size(121, 20);
            this.mobComb.TabIndex = 9;
            this.mobComb.SelectedValueChanged += new System.EventHandler(this.mobComb_SelectedValueChanged);
            // 
            // richMob
            // 
            this.richMob.Location = new System.Drawing.Point(876, 59);
            this.richMob.Name = "richMob";
            this.richMob.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richMob.Size = new System.Drawing.Size(247, 178);
            this.richMob.TabIndex = 10;
            this.richMob.Text = "";
            // 
            // webB1
            // 
            this.webB1.Location = new System.Drawing.Point(39, 470);
            this.webB1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webB1.Name = "webB1";
            this.webB1.Size = new System.Drawing.Size(831, 213);
            this.webB1.TabIndex = 11;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(381, 449);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(41, 12);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "看效果";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(904, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "参数设置：";
            // 
            // txtParam
            // 
            this.txtParam.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtParam.Location = new System.Drawing.Point(891, 277);
            this.txtParam.Multiline = true;
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(218, 156);
            this.txtParam.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 682);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.webB1);
            this.Controls.Add(this.richMob);
            this.Controls.Add(this.mobComb);
            this.Controls.Add(this.mobBtn);
            this.Controls.Add(this.MobTxt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richSetup);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.RichTextBox richSetup;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox MobTxt;
        private System.Windows.Forms.Button mobBtn;
        private System.Windows.Forms.ComboBox mobComb;
        private System.Windows.Forms.RichTextBox richMob;
        private System.Windows.Forms.WebBrowser webB1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtParam;
    }
}

