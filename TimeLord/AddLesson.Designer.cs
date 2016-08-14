namespace TimeLord
{
    partial class AddLesson
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
            this.button1 = new System.Windows.Forms.Button();
            this.cb_day = new System.Windows.Forms.ComboBox();
            this.cb_periodStart = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_noOfPeriods = new System.Windows.Forms.ComboBox();
            this.cb_room = new System.Windows.Forms.ComboBox();
            this.cb_subjectCode = new System.Windows.Forms.ComboBox();
            this.cb_teacherCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_class = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_yearGroup = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.num_hwkAmount = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ck_locked = new System.Windows.Forms.CheckBox();
            this.ck_invisible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_hwkAmount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(475, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_day
            // 
            this.cb_day.FormattingEnabled = true;
            this.cb_day.Location = new System.Drawing.Point(120, 19);
            this.cb_day.Name = "cb_day";
            this.cb_day.Size = new System.Drawing.Size(121, 21);
            this.cb_day.TabIndex = 5;
            this.cb_day.SelectedIndexChanged += new System.EventHandler(this.cb_day_SelectedIndexChanged);
            // 
            // cb_periodStart
            // 
            this.cb_periodStart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cb_periodStart.FormattingEnabled = true;
            this.cb_periodStart.Location = new System.Drawing.Point(120, 52);
            this.cb_periodStart.Name = "cb_periodStart";
            this.cb_periodStart.Size = new System.Drawing.Size(121, 21);
            this.cb_periodStart.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Day:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Period Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Number of Periods";
            // 
            // cb_noOfPeriods
            // 
            this.cb_noOfPeriods.FormattingEnabled = true;
            this.cb_noOfPeriods.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cb_noOfPeriods.Location = new System.Drawing.Point(120, 82);
            this.cb_noOfPeriods.Name = "cb_noOfPeriods";
            this.cb_noOfPeriods.Size = new System.Drawing.Size(121, 21);
            this.cb_noOfPeriods.TabIndex = 9;
            this.cb_noOfPeriods.Text = "1";
            // 
            // cb_room
            // 
            this.cb_room.FormattingEnabled = true;
            this.cb_room.Location = new System.Drawing.Point(364, 82);
            this.cb_room.Name = "cb_room";
            this.cb_room.Size = new System.Drawing.Size(121, 21);
            this.cb_room.TabIndex = 13;
            // 
            // cb_subjectCode
            // 
            this.cb_subjectCode.FormattingEnabled = true;
            this.cb_subjectCode.Location = new System.Drawing.Point(364, 54);
            this.cb_subjectCode.Name = "cb_subjectCode";
            this.cb_subjectCode.Size = new System.Drawing.Size(121, 21);
            this.cb_subjectCode.TabIndex = 12;
            // 
            // cb_teacherCode
            // 
            this.cb_teacherCode.FormattingEnabled = true;
            this.cb_teacherCode.Location = new System.Drawing.Point(364, 18);
            this.cb_teacherCode.Name = "cb_teacherCode";
            this.cb_teacherCode.Size = new System.Drawing.Size(121, 21);
            this.cb_teacherCode.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Teacher:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Subject:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Room";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(264, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Form:";
            // 
            // cb_class
            // 
            this.cb_class.FormattingEnabled = true;
            this.cb_class.Location = new System.Drawing.Point(364, 139);
            this.cb_class.Name = "cb_class";
            this.cb_class.Size = new System.Drawing.Size(121, 21);
            this.cb_class.TabIndex = 17;
            this.cb_class.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Year Group:";
            // 
            // cb_yearGroup
            // 
            this.cb_yearGroup.FormattingEnabled = true;
            this.cb_yearGroup.Location = new System.Drawing.Point(364, 109);
            this.cb_yearGroup.Name = "cb_yearGroup";
            this.cb_yearGroup.Size = new System.Drawing.Size(121, 21);
            this.cb_yearGroup.TabIndex = 19;
            this.cb_yearGroup.SelectedIndexChanged += new System.EventHandler(this.cb_yearGroup_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Homework Amount";
            // 
            // num_hwkAmount
            // 
            this.num_hwkAmount.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.num_hwkAmount.Location = new System.Drawing.Point(120, 110);
            this.num_hwkAmount.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_hwkAmount.Name = "num_hwkAmount";
            this.num_hwkAmount.Size = new System.Drawing.Size(60, 20);
            this.num_hwkAmount.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(192, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "mins.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ck_locked);
            this.groupBox1.Controls.Add(this.ck_invisible);
            this.groupBox1.Controls.Add(this.cb_day);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cb_periodStart);
            this.groupBox1.Controls.Add(this.num_hwkAmount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cb_noOfPeriods);
            this.groupBox1.Controls.Add(this.cb_yearGroup);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cb_teacherCode);
            this.groupBox1.Controls.Add(this.cb_class);
            this.groupBox1.Controls.Add(this.cb_subjectCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cb_room);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 187);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // ck_locked
            // 
            this.ck_locked.AutoSize = true;
            this.ck_locked.Location = new System.Drawing.Point(161, 164);
            this.ck_locked.Name = "ck_locked";
            this.ck_locked.Size = new System.Drawing.Size(62, 17);
            this.ck_locked.TabIndex = 27;
            this.ck_locked.Text = "Locked";
            this.ck_locked.UseVisualStyleBackColor = true;
            this.ck_locked.CheckedChanged += new System.EventHandler(this.ck_locked_CheckedChanged);
            // 
            // ck_invisible
            // 
            this.ck_invisible.AutoSize = true;
            this.ck_invisible.Location = new System.Drawing.Point(8, 164);
            this.ck_invisible.Name = "ck_invisible";
            this.ck_invisible.Size = new System.Drawing.Size(64, 17);
            this.ck_invisible.TabIndex = 26;
            this.ck_invisible.Text = "Invisible";
            this.ck_invisible.UseVisualStyleBackColor = true;
            // 
            // AddLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 251);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLesson";
            this.Text = "Add Lesson";
            this.Load += new System.EventHandler(this.AddLesson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_hwkAmount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_noOfPeriods;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown num_hwkAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cb_day;
        public System.Windows.Forms.ComboBox cb_periodStart;
        public System.Windows.Forms.ComboBox cb_room;
        public System.Windows.Forms.ComboBox cb_subjectCode;
        public System.Windows.Forms.ComboBox cb_teacherCode;
        public System.Windows.Forms.ComboBox cb_class;
        public System.Windows.Forms.ComboBox cb_yearGroup;
        public System.Windows.Forms.CheckBox ck_locked;
        public System.Windows.Forms.CheckBox ck_invisible;

    }
}