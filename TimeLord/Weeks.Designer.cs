namespace TimeLord
{
    partial class Weeks
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.list_days = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_periodDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_periodDisplay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.list_periods = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_dayName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_addDay = new System.Windows.Forms.Button();
            this.btn_removeDay = new System.Windows.Forms.Button();
            this.btn_removePeriod = new System.Windows.Forms.Button();
            this.btn_addPeriod = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(451, 417);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // list_days
            // 
            this.list_days.Cursor = System.Windows.Forms.Cursors.Default;
            this.list_days.FormattingEnabled = true;
            this.list_days.Location = new System.Drawing.Point(12, 32);
            this.list_days.Name = "list_days";
            this.list_days.Size = new System.Drawing.Size(255, 290);
            this.list_days.TabIndex = 1;
            this.list_days.SelectedIndexChanged += new System.EventHandler(this.list_days_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_periodDesc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_periodDisplay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(273, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 83);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period:";
            // 
            // tb_periodDesc
            // 
            this.tb_periodDesc.Location = new System.Drawing.Point(90, 47);
            this.tb_periodDesc.Name = "tb_periodDesc";
            this.tb_periodDesc.Size = new System.Drawing.Size(157, 20);
            this.tb_periodDesc.TabIndex = 3;
            this.tb_periodDesc.TextChanged += new System.EventHandler(this.tb_periodDesc_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // tb_periodDisplay
            // 
            this.tb_periodDisplay.Location = new System.Drawing.Point(90, 17);
            this.tb_periodDisplay.Name = "tb_periodDisplay";
            this.tb_periodDisplay.Size = new System.Drawing.Size(157, 20);
            this.tb_periodDisplay.TabIndex = 1;
            this.tb_periodDisplay.TextChanged += new System.EventHandler(this.tb_periodDisplay_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Period Name:";
            // 
            // list_periods
            // 
            this.list_periods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.list_periods.FullRowSelect = true;
            this.list_periods.GridLines = true;
            this.list_periods.HideSelection = false;
            this.list_periods.Location = new System.Drawing.Point(273, 32);
            this.list_periods.MultiSelect = false;
            this.list_periods.Name = "list_periods";
            this.list_periods.Size = new System.Drawing.Size(253, 290);
            this.list_periods.TabIndex = 4;
            this.list_periods.UseCompatibleStateImageBehavior = false;
            this.list_periods.View = System.Windows.Forms.View.Details;
            this.list_periods.SelectedIndexChanged += new System.EventHandler(this.listView_periods_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 29;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 61;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 128;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_dayName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(14, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 83);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Day:";
            // 
            // tb_dayName
            // 
            this.tb_dayName.Location = new System.Drawing.Point(60, 32);
            this.tb_dayName.Name = "tb_dayName";
            this.tb_dayName.Size = new System.Drawing.Size(187, 20);
            this.tb_dayName.TabIndex = 1;
            this.tb_dayName.TextChanged += new System.EventHandler(this.tb_dayName_TextChanged);
            this.tb_dayName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_dayName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // btn_addDay
            // 
            this.btn_addDay.Location = new System.Drawing.Point(12, 3);
            this.btn_addDay.Name = "btn_addDay";
            this.btn_addDay.Size = new System.Drawing.Size(21, 23);
            this.btn_addDay.TabIndex = 5;
            this.btn_addDay.Text = "+";
            this.btn_addDay.UseVisualStyleBackColor = true;
            this.btn_addDay.Click += new System.EventHandler(this.btn_addDay_Click);
            // 
            // btn_removeDay
            // 
            this.btn_removeDay.Location = new System.Drawing.Point(37, 3);
            this.btn_removeDay.Name = "btn_removeDay";
            this.btn_removeDay.Size = new System.Drawing.Size(21, 23);
            this.btn_removeDay.TabIndex = 6;
            this.btn_removeDay.Text = "-";
            this.btn_removeDay.UseVisualStyleBackColor = true;
            this.btn_removeDay.Click += new System.EventHandler(this.btn_removeDay_Click);
            // 
            // btn_removePeriod
            // 
            this.btn_removePeriod.Location = new System.Drawing.Point(503, 3);
            this.btn_removePeriod.Name = "btn_removePeriod";
            this.btn_removePeriod.Size = new System.Drawing.Size(21, 23);
            this.btn_removePeriod.TabIndex = 8;
            this.btn_removePeriod.Text = "-";
            this.btn_removePeriod.UseVisualStyleBackColor = true;
            this.btn_removePeriod.Click += new System.EventHandler(this.btn_removePeriod_Click);
            // 
            // btn_addPeriod
            // 
            this.btn_addPeriod.Location = new System.Drawing.Point(478, 3);
            this.btn_addPeriod.Name = "btn_addPeriod";
            this.btn_addPeriod.Size = new System.Drawing.Size(21, 23);
            this.btn_addPeriod.TabIndex = 7;
            this.btn_addPeriod.Text = "+";
            this.btn_addPeriod.UseVisualStyleBackColor = true;
            this.btn_addPeriod.Click += new System.EventHandler(this.btn_addPeriod_Click);
            // 
            // Weeks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 452);
            this.Controls.Add(this.btn_removePeriod);
            this.Controls.Add(this.btn_addPeriod);
            this.Controls.Add(this.btn_removeDay);
            this.Controls.Add(this.btn_addDay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.list_periods);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.list_days);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Weeks";
            this.Text = "Week Manager:";
            this.Load += new System.EventHandler(this.Weeks_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ListBox list_days;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_periodDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_periodDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView list_periods;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_dayName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_addDay;
        private System.Windows.Forms.Button btn_removeDay;
        private System.Windows.Forms.Button btn_removePeriod;
        private System.Windows.Forms.Button btn_addPeriod;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}