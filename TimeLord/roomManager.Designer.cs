namespace TimeLord
{
    partial class roomManager
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
            this.list_room = new System.Windows.Forms.ListBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_roomName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_removeRoom = new System.Windows.Forms.Button();
            this.btn_addRoom = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_room
            // 
            this.list_room.FormattingEnabled = true;
            this.list_room.Location = new System.Drawing.Point(210, 30);
            this.list_room.MultiColumn = true;
            this.list_room.Name = "list_room";
            this.list_room.Size = new System.Drawing.Size(251, 186);
            this.list_room.TabIndex = 0;
            this.list_room.SelectedIndexChanged += new System.EventHandler(this.list_room_SelectedIndexChanged);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(12, 167);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(81, 46);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_roomName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 68);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details:";
            // 
            // tb_roomName
            // 
            this.tb_roomName.Location = new System.Drawing.Point(78, 23);
            this.tb_roomName.Name = "tb_roomName";
            this.tb_roomName.Size = new System.Drawing.Size(94, 20);
            this.tb_roomName.TabIndex = 1;
            this.tb_roomName.TextChanged += new System.EventHandler(this.tb_roomName_TextChanged);
            this.tb_roomName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_roomName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name:";
            // 
            // btn_removeRoom
            // 
            this.btn_removeRoom.Location = new System.Drawing.Point(237, 3);
            this.btn_removeRoom.Name = "btn_removeRoom";
            this.btn_removeRoom.Size = new System.Drawing.Size(21, 23);
            this.btn_removeRoom.TabIndex = 8;
            this.btn_removeRoom.Text = "-";
            this.btn_removeRoom.UseVisualStyleBackColor = true;
            this.btn_removeRoom.Click += new System.EventHandler(this.btn_removeRoom_Click);
            // 
            // btn_addRoom
            // 
            this.btn_addRoom.Location = new System.Drawing.Point(210, 3);
            this.btn_addRoom.Name = "btn_addRoom";
            this.btn_addRoom.Size = new System.Drawing.Size(21, 23);
            this.btn_addRoom.TabIndex = 7;
            this.btn_addRoom.Text = "+";
            this.btn_addRoom.UseVisualStyleBackColor = true;
            this.btn_addRoom.Click += new System.EventHandler(this.btn_addRoom_Click);
            // 
            // roomManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 225);
            this.Controls.Add(this.btn_removeRoom);
            this.Controls.Add(this.btn_addRoom);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.list_room);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "roomManager";
            this.Text = "Room Manager";
            this.Load += new System.EventHandler(this.roomManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_room;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_roomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_removeRoom;
        private System.Windows.Forms.Button btn_addRoom;
    }
}