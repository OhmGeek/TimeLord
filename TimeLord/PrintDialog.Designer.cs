namespace TimeLord
{
    partial class PrintDialog
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
            this.lb_typeOfPrint = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ck_printInvisible = new System.Windows.Forms.CheckBox();
            this.lb_selectedItems = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.print_document = new System.Drawing.Printing.PrintDocument();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.print_previewWindow = new System.Windows.Forms.PrintPreviewControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.lbl_pageDisplayed = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_typeOfPrint
            // 
            this.lb_typeOfPrint.FormattingEnabled = true;
            this.lb_typeOfPrint.Items.AddRange(new object[] {
            "Year Timetables",
            "Staff Timetables",
            "Room Timetables",
            "Subject Timetables",
            "Homework Timetables",
            "Whole School Timetables"});
            this.lb_typeOfPrint.Location = new System.Drawing.Point(6, 19);
            this.lb_typeOfPrint.Name = "lb_typeOfPrint";
            this.lb_typeOfPrint.Size = new System.Drawing.Size(131, 121);
            this.lb_typeOfPrint.TabIndex = 0;
            this.lb_typeOfPrint.SelectedIndexChanged += new System.EventHandler(this.lb_typeOfPrint_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.ck_printInvisible);
            this.groupBox1.Controls.Add(this.lb_selectedItems);
            this.groupBox1.Controls.Add(this.lb_typeOfPrint);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 423);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Document Properties:";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(198, 231);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Print";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Print Preview";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ck_printInvisible
            // 
            this.ck_printInvisible.AutoSize = true;
            this.ck_printInvisible.Location = new System.Drawing.Point(6, 146);
            this.ck_printInvisible.Name = "ck_printInvisible";
            this.ck_printInvisible.Size = new System.Drawing.Size(130, 17);
            this.ck_printInvisible.TabIndex = 2;
            this.ck_printInvisible.Text = "Print Invisible Lessons";
            this.ck_printInvisible.UseVisualStyleBackColor = true;
            // 
            // lb_selectedItems
            // 
            this.lb_selectedItems.CheckOnClick = true;
            this.lb_selectedItems.FormattingEnabled = true;
            this.lb_selectedItems.Location = new System.Drawing.Point(143, 16);
            this.lb_selectedItems.Name = "lb_selectedItems";
            this.lb_selectedItems.Size = new System.Drawing.Size(149, 124);
            this.lb_selectedItems.TabIndex = 1;
            this.lb_selectedItems.ThreeDCheckBoxes = true;
            this.lb_selectedItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lb_selectedItems_ItemCheck);
            this.lb_selectedItems.SelectedIndexChanged += new System.EventHandler(this.lb_selectedItems_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "<-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(228, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "->";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // print_document
            // 
            this.print_document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.print_document_PrintPage);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.94601F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.05399F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.print_previewWindow, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 475);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // print_previewWindow
            // 
            this.print_previewWindow.AutoZoom = false;
            this.print_previewWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.print_previewWindow.Location = new System.Drawing.Point(305, 3);
            this.print_previewWindow.Name = "print_previewWindow";
            this.print_previewWindow.Size = new System.Drawing.Size(470, 423);
            this.print_previewWindow.TabIndex = 2;
            this.print_previewWindow.Zoom = 1D;
            this.print_previewWindow.Click += new System.EventHandler(this.print_previewWindow_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_pageDisplayed);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(305, 432);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 40);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // lbl_pageDisplayed
            // 
            this.lbl_pageDisplayed.AutoSize = true;
            this.lbl_pageDisplayed.Location = new System.Drawing.Point(368, 18);
            this.lbl_pageDisplayed.Name = "lbl_pageDisplayed";
            this.lbl_pageDisplayed.Size = new System.Drawing.Size(58, 13);
            this.lbl_pageDisplayed.TabIndex = 6;
            this.lbl_pageDisplayed.Text = "Page 0 / 0";
            // 
            // PrintDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 475);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PrintDialog";
            this.Text = "PrintDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintDialog_FormClosing);
            this.Load += new System.EventHandler(this.PrintDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_typeOfPrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox lb_selectedItems;
        private System.Windows.Forms.CheckBox ck_printInvisible;
        private System.Drawing.Printing.PrintDocument print_document;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PrintPreviewControl print_previewWindow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lbl_pageDisplayed;
    }
}