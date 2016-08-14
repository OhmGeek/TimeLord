using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLord
{
    public partial class roomManager : Form
    {
        public Timetable currentTT = null;

        public roomManager()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RefreshListBoxContents()
        {
            
            list_room.DataSource = null;
           
            list_room.DataSource = currentTT.Rooms;


            if (list_room.Items.Count != 0)
            {
                list_room.SelectedIndex = list_room.Items.Count - 1;
            }


        }



        private void roomManager_Load(object sender, EventArgs e)
        {
            if (currentTT == null)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            RefreshListBoxContents();
        }

        private void btn_addRoom_Click(object sender, EventArgs e)
        {
            if (currentTT.Rooms.Count() == 256)
            {
                MessageBox.Show("You cannot add more than 256 rooms.");
            }
            currentTT.Rooms.Add(new Room("Room"));
            RefreshListBoxContents();
        }

        private void btn_removeRoom_Click(object sender, EventArgs e)
        {
            if (currentTT.Rooms.Count() == 0)
            {
                MessageBox.Show("There are no rooms to delete.");
                return;

            }
            int index = list_room.SelectedIndex;

            if (index == -1) return;
            

            currentTT.Rooms.RemoveAt(index);
            RefreshListBoxContents();
        }

        private void list_room_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (list_room.SelectedItem == null) return;
            if (list_room.Items.Count == 0) return;
            //if (list_room.SelectedIndex == -1) return;
            Room selectedItem = (Room)list_room.SelectedItem;
            tb_roomName.Text = selectedItem.RoomCode;
        }

        private void tb_roomName_TextChanged(object sender, EventArgs e)
        {
            if (list_room.SelectedIndex == -1) return;
            Room selectedRoom = (Room)list_room.SelectedItem;
            selectedRoom.RoomCode = tb_roomName.Text;
            
        }

        private void tb_roomName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshListBoxContents();

            }
        }
    }
}
