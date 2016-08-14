using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLord
{
   public class Lesson
    {
        public string TeacherAbbreviation;
        public string SubjectAbbreviation;
        public byte homeworkAmount;
        public bool locked;
        public bool invisible;
        public string RoomCode;
        public byte DayIndex;
        public byte PeriodIndex;
        public bool selected;
        public byte YearIndex;
        public byte FormIndex;
       /// <summary>
       /// Constructor.
       /// </summary>
       /// <param name="Teacher">TeacherCode</param>
       /// <param name="Subject">Subject Code</param>
       /// <param name="RoomCode">Room Name</param>
       /// <param name="DayIndex">The index of the day in the timetable.</param>
       /// <param name="PeriodIndex">The index of the period in the timetable.</param>
       /// <param name="homeworkAmount">The amount of homework set for the lesson.</param>
       /// <param name="invisible">Whether the lesson is 'invisible'</param>
       /// <param name="locked">Whether the lesson is locked</param>
        public Lesson(string Teacher, string Subject, string RoomCode, byte DayIndex, byte PeriodIndex,byte homeworkAmount, bool locked, bool invisible, byte FormIndex, byte YearIndex)
        {
            this.TeacherAbbreviation = Teacher;
            this.SubjectAbbreviation = Subject;
            this.RoomCode = RoomCode;
            this.DayIndex = DayIndex;
            this.PeriodIndex = PeriodIndex;
            this.homeworkAmount = homeworkAmount;
            this.locked = locked;
            this.invisible = invisible;
            this.selected = false;
            this.YearIndex = YearIndex;
            this.FormIndex = FormIndex;
        }

        public override string ToString()
        {
            return TeacherAbbreviation + " " + SubjectAbbreviation + " " + DayIndex + " " + PeriodIndex + " " + RoomCode;
        }
        public Lesson(System.IO.BinaryReader reader)
        {
            DayIndex = reader.ReadByte();
            PeriodIndex = reader.ReadByte();
            YearIndex = reader.ReadByte();
            FormIndex = reader.ReadByte();

            TeacherAbbreviation = reader.ReadString();
            SubjectAbbreviation = reader.ReadString();
            RoomCode = reader.ReadString();
            homeworkAmount = reader.ReadByte();
            invisible = reader.ReadBoolean();
            locked = reader.ReadBoolean();
        }
        public void WriteLessonToFile(System.IO.BinaryWriter writer)
        {
            writer.Write(DayIndex);
            writer.Write(PeriodIndex);
            writer.Write(YearIndex);
            writer.Write(FormIndex);
            writer.Write(TeacherAbbreviation);
            writer.Write(SubjectAbbreviation);
            writer.Write(RoomCode);
            writer.Write(homeworkAmount);
            writer.Write(invisible);
            writer.Write(locked);

            
        }


        public Lesson Clone()
        {
            Lesson clonedObj = new Lesson(TeacherAbbreviation, SubjectAbbreviation, RoomCode, DayIndex, PeriodIndex, homeworkAmount, locked, invisible, FormIndex, YearIndex);
            return clonedObj;
        }
        public void DrawOnPage(int x, int y, int width, int height, Graphics G)
        {
            int penWidth = 2; //This is a constant, but it helps us when tuning the program.
            
            Brush brushToUse = null;
            if (locked && invisible) brushToUse = new SolidBrush(Properties.Settings.Default.Screen_Color_LockedAndInvisible);
            else if (locked) brushToUse = new SolidBrush(Properties.Settings.Default.Screen_Color_Locked);
            else if (invisible) brushToUse = new SolidBrush(Properties.Settings.Default.Screen_Color_Invisible);
            else brushToUse = new SolidBrush(Properties.Settings.Default.Screen_Color_Normal);

            Color colorToUse = selected ? Color.Yellow : Color.Black;
            Pen penToUse = new Pen(colorToUse, 2);
            Rectangle rect = new Rectangle(x + penWidth, y + penWidth, width - penWidth, height - penWidth);
            G.FillRectangle(brushToUse, rect);
            G.DrawRectangle(penToUse,rect);
            string str = SubjectAbbreviation + " " + RoomCode + "  " + TeacherAbbreviation;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            G.DrawString(str, new Font("Segoe UI", 12), Brushes.White,rect,sf);
        }

       /// <summary>
       /// Draws the grid box for the homework view onto the screen. Draws the homework amount too.
       /// </summary>
       /// <param name="x">x position of the rectangle.</param>
       /// <param name="y">y position of the rectangle.</param>
       /// <param name="width">width of the rectangle.</param>
       /// <param name="height">height of the rectangle.</param>
       /// <param name="G">The graphics object needed to draw onto the screen.</param>
        public void DrawHomework(int x, int y, int width, int height, Graphics G)
        {
            if (homeworkAmount == 0) return; //don't draw no homework on the screen.
            G.DrawRectangle(Pens.Black, x, y, width, height);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            string str = SubjectAbbreviation + " " + RoomCode + " " + TeacherAbbreviation + " " + homeworkAmount;
            G.DrawString(str, new Font("Segoe UI", 12), Brushes.White, x, y,sf);


        }
    }
}
