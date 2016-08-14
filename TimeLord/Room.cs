using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace TimeLord
{
    public class Room
    {
        public string RoomCode;
        public bool Visible;
        public Rectangle roomBounds;
        public bool selectedForPrint;
        public Room(string RoomCode)
        {
            this.RoomCode = RoomCode;
            this.Visible = true;
            this.selectedForPrint = false;
        }
        public override string ToString()
        {
            return this.RoomCode;
        }
    }
}
