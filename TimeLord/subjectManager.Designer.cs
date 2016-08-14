namespace TimeLord
{
    partial class subjectManager
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
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_removeSubject = new System.Windows.Forms.Button();
            this.btn_addSubject = new System.Windows.Forms.Button();
            this.list_subjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(523, 284);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(78, 40);
            this.btn_ok.TabIndex = 18;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_name);
            this.groupBox1.Controls.Add(this.tb_code);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(397, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 100);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(48, 65);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(146, 20);
            this.tb_name.TabIndex = 3;
            this.tb_name.TextChanged += new System.EventHandler(this.tb_name_TextChanged);
            // 
            // tb_code
            // 
            this.tb_code.Location = new System.Drawing.Point(48, 33);
            this.tb_code.Name = "tb_code";
            this.tb_code.Size = new System.Drawing.Size(146, 20);
            this.tb_code.TabIndex = 2;
            this.tb_code.TextChanged += new System.EventHandler(this.tb_code_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code:";
            // 
            // btn_removeSubject
            // 
            this.btn_removeSubject.Location = new System.Drawing.Point(39, 5);
            this.btn_removeSubject.Name = "btn_removeSubject";
            this.btn_removeSubject.Size = new System.Drawing.Size(21, 23);
            this.btn_removeSubject.TabIndex = 16;
            this.btn_removeSubject.Text = "-";
            this.btn_removeSubject.UseVisualStyleBackColor = true;
            this.btn_removeSubject.Click += new System.EventHandler(this.btn_removeSubject_Click);
            // 
            // btn_addSubject
            // 
            this.btn_addSubject.Location = new System.Drawing.Point(12, 5);
            this.btn_addSubject.Name = "btn_addSubject";
            this.btn_addSubject.Size = new System.Drawing.Size(21, 23);
            this.btn_addSubject.TabIndex = 15;
            this.btn_addSubject.Text = "+";
            this.btn_addSubject.UseVisualStyleBackColor = true;
            this.btn_addSubject.Click += new System.EventHandler(this.btn_addSubject_Click);
            // 
            // list_subjects
            // 
            this.list_subjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.list_subjects.FullRowSelect = true;
            this.list_subjects.GridLines = true;
            this.list_subjects.HideSelection = false;
            this.list_subjects.Location = new System.Drawing.Point(12, 34);
            this.list_subjects.MultiSelect = false;
            this.list_subjects.Name = "list_subjects";
            this.list_subjects.Size = new System.Drawing.Size(379, 290);
            this.list_subjects.TabIndex = 14;
            this.list_subjects.UseCompatibleStateImageBehavior = false;
            this.list_subjects.View = System.Windows.Forms.View.Details;
            this.list_subjects.SelectedIndexChanged += new System.EventHandler(this.list_subjects_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Code";
            this.columnHeader1.Width = 54;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 300;
            // 
            // subjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 336);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_removeSubject);
            this.Controls.Add(this.btn_addSubject);
            this.Controls.Add(this.list_subjects);
            this.Name = "subjectManager";
            this.Text = "subjectManager";
            this.Load += new System.EventHandler(this.subjectManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_removeSubject;
        private System.Windows.Forms.Button btn_addSubject;
        private System.Windows.Forms.ListView list_subjects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}