namespace TimeLord
{
    partial class VisibleYearsAndForms
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
            this.list_years = new System.Windows.Forms.CheckedListBox();
            this.btn_allDay = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.list_forms = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // list_years
            // 
            this.list_years.Cursor = System.Windows.Forms.Cursors.Default;
            this.list_years.FormattingEnabled = true;
            this.list_years.Location = new System.Drawing.Point(12, 42);
            this.list_years.Name = "list_years";
            this.list_years.Size = new System.Drawing.Size(255, 289);
            this.list_years.TabIndex = 5;
            this.list_years.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.list_years_ItemCheck);
            this.list_years.SelectedIndexChanged += new System.EventHandler(this.list_days_SelectedIndexChanged);
            // 
            // btn_allDay
            // 
            this.btn_allDay.Location = new System.Drawing.Point(13, 13);
            this.btn_allDay.Name = "btn_allDay";
            this.btn_allDay.Size = new System.Drawing.Size(75, 23);
            this.btn_allDay.TabIndex = 7;
            this.btn_allDay.Text = "Select All";
            this.btn_allDay.UseVisualStyleBackColor = true;
            this.btn_allDay.Click += new System.EventHandler(this.btn_allDay_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(448, 365);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 53);
            this.btn_OK.TabIndex = 9;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // list_forms
            // 
            this.list_forms.FormattingEnabled = true;
            this.list_forms.Location = new System.Drawing.Point(273, 42);
            this.list_forms.Name = "list_forms";
            this.list_forms.Size = new System.Drawing.Size(250, 289);
            this.list_forms.TabIndex = 10;
            this.list_forms.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.list_forms_ItemCheck);
            // 
            // VisibleYearsAndForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 430);
            this.Controls.Add(this.list_forms);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_allDay);
            this.Controls.Add(this.list_years);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VisibleYearsAndForms";
            this.Text = "Normal View:";
            this.Load += new System.EventHandler(this.VisibleYearsAndForms_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox list_years;
        private System.Windows.Forms.Button btn_allDay;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.CheckedListBox list_forms;
    }
}