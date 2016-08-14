namespace TimeLord
{
    partial class staffManager
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
            this.btn_removePeriod = new System.Windows.Forms.Button();
            this.btn_addPeriod = new System.Windows.Forms.Button();
            this.list_staff = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_removePeriod
            // 
            this.btn_removePeriod.Location = new System.Drawing.Point(39, 3);
            this.btn_removePeriod.Name = "btn_removePeriod";
            this.btn_removePeriod.Size = new System.Drawing.Size(21, 23);
            this.btn_removePeriod.TabIndex = 11;
            this.btn_removePeriod.Text = "-";
            this.btn_removePeriod.UseVisualStyleBackColor = true;
            this.btn_removePeriod.Click += new System.EventHandler(this.btn_removePeriod_Click);
            // 
            // btn_addPeriod
            // 
            this.btn_addPeriod.Location = new System.Drawing.Point(12, 3);
            this.btn_addPeriod.Name = "btn_addPeriod";
            this.btn_addPeriod.Size = new System.Drawing.Size(21, 23);
            this.btn_addPeriod.TabIndex = 10;
            this.btn_addPeriod.Text = "+";
            this.btn_addPeriod.UseVisualStyleBackColor = true;
            this.btn_addPeriod.Click += new System.EventHandler(this.btn_addPeriod_Click);
            // 
            // list_staff
            // 
            this.list_staff.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.list_staff.FullRowSelect = true;
            this.list_staff.GridLines = true;
            this.list_staff.HideSelection = false;
            this.list_staff.Location = new System.Drawing.Point(12, 32);
            this.list_staff.MultiSelect = false;
            this.list_staff.Name = "list_staff";
            this.list_staff.Size = new System.Drawing.Size(379, 290);
            this.list_staff.TabIndex = 9;
            this.list_staff.UseCompatibleStateImageBehavior = false;
            this.list_staff.View = System.Windows.Forms.View.Details;
            this.list_staff.SelectedIndexChanged += new System.EventHandler(this.list_staff_SelectedIndexChanged);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_name);
            this.groupBox1.Controls.Add(this.tb_code);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(397, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 100);
            this.groupBox1.TabIndex = 12;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 40);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // staffManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 340);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_removePeriod);
            this.Controls.Add(this.btn_addPeriod);
            this.Controls.Add(this.list_staff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "staffManager";
            this.Text = "Staff Manager";
            this.Load += new System.EventHandler(this.staffManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_removePeriod;
        private System.Windows.Forms.Button btn_addPeriod;
        private System.Windows.Forms.ListView list_staff;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}