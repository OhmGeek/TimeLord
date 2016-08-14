//this is a comment. please disregard - used to port to github.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
namespace TimeLord
{
    public enum Event
    {
        MovePeriod, MoveYear, MoveStaff, MoveRoom, MoveForm, MoveToTray, MoveFromTray,MoveSubject,ChangeHomework,ChangeLocked,ChangeInvisible
    }
    public enum ClickMode
    {
        Normal,FindStaff,FindRooms,AddLessonViaMenu,Move,InsertFromTray
    }

    public partial class Form1 : Form
    {
        private const int timeForMove = 200;
        private Stack<undoRedoEvent> undoStack;
        private Stack<undoRedoEvent> redoStack;
        Timetable currentTT = null;
        private ClickMode mainViewMode = ClickMode.Normal;
       private Lesson displayedItem = null;
    
        private System.Diagnostics.Stopwatch sw = null;
        
      //  private bool findFreeStaff = false;
       // private bool addLessonViaMenu = false;
       
        private string currentFilename = "";
        private Weeks weekView = null; //stores the week view when we load it.
        private Years yearView = null;
        private Point cursorPosition = new Point(0, 0);
        private staffManager staffView = null;
        private roomManager roomView = null;
        private AddLesson lessonView = null;
        private subjectManager subjectView = null;
        private const string appName = "TimeLord";
      
        public Form1()
        {
            InitializeComponent();
        }

        private void SetStatusOfStructureForms(bool status)
        {
            toolStrip1.Enabled = !status;
            mi_modifyRooms.Enabled = status;
            mi_modifyStaff.Enabled = status;
            mi_modifySubjects.Enabled = status;
            mi_modifyWeek.Enabled = status;
            mi_ModifyYearGroups.Enabled = status;
            btn_addLessonViaSelection.Enabled = !status;
            btn_deleteSelectedLesson.Enabled = !status; //if we have finalised everything, then we want teh buttons to be enabled.
        }
        private void DisplayInfoAboutLesson(Lesson LessonToDisplay)
        {
            cb_year.SelectedIndex = LessonToDisplay.YearIndex;
            cb_homework.Text = Convert.ToString(LessonToDisplay.homeworkAmount);
            cb_room.SelectedIndex = currentTT.GetIndexOfRoom(LessonToDisplay.RoomCode);
            cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(LessonToDisplay.SubjectAbbreviation);
            cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(LessonToDisplay.TeacherAbbreviation);
            cb_form.SelectedIndex = LessonToDisplay.FormIndex;
            btn_invisible.Checked = LessonToDisplay.invisible;
            btn_locked.Checked = LessonToDisplay.locked;
        }
        private void modifyWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (weekView == null)
            {
                weekView = new Weeks();
                weekView.TT = currentTT;
            }
            this.Enabled = false; //focus on weekView form. Wait for a response.

            weekView.ShowDialog();
            this.Enabled = true;
            weekView = null; //clears the value from the heap
        }

        private void newTimetableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTT = new Timetable();
            pb_mainView.Refresh();
            pb_roomView.Refresh();
            pb_staffView.Refresh();
            pb_homeworkView.Refresh();
            this.Text = appName + " | Untitled Timetable";
            
        }
        void pb_mainView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) {
               
            }
        }

        private void UndoRedoEvents(undoRedoEvent e)
        {

            switch (e.task)
            {
                case Event.MovePeriod:
                    if (e.lessonToUndo.PeriodIndex == e.B) currentTT.MovePeriods(e.lessonToUndo, Convert.ToByte(e.A));
                    else throw new Exception();
                    break;
                case Event.MoveYear:
                    if (e.lessonToUndo.YearIndex == e.B) currentTT.MoveYears(e.lessonToUndo, Convert.ToByte(e.A));
                    else throw new Exception();
                    break;
                case Event.MoveStaff:
                    if (e.lessonToUndo.TeacherAbbreviation == currentTT.Staff[e.B].TeacherAbbreviation) currentTT.MoveTeacher(e.lessonToUndo, Convert.ToByte(e.A));
                    else throw new Exception();
                    break;
                case Event.MoveRoom:
                    if (e.lessonToUndo.RoomCode == currentTT.Rooms[e.B].RoomCode) currentTT.MoveRooms(e.lessonToUndo, Convert.ToByte(e.A));
                    else throw new Exception();
                    break;
                case Event.MoveForm:
                    if (e.lessonToUndo.FormIndex == e.B) currentTT.MoveForms(e.lessonToUndo, Convert.ToByte(e.A));
                    else throw new Exception();
                    break;
                case Event.MoveToTray:
                    break;
                case Event.MoveFromTray:
                    break;
                case Event.MoveSubject:
                    if (e.B == currentTT.GetIndexOfSubject(e.lessonToUndo.SubjectAbbreviation))
                    {
                        e.lessonToUndo.SubjectAbbreviation = currentTT.Subjects[e.A].SubjectAbbreviation;
                    }
                    else throw new Exception();
                    break;
                case Event.ChangeHomework:
                    e.lessonToUndo.homeworkAmount = Convert.ToByte(e.A);
                    break;
                case Event.ChangeLocked:
                    e.lessonToUndo.locked = (e.A == 255);
                    break;
                case Event.ChangeInvisible:
                    e.lessonToUndo.invisible = (e.A == 255);
                    break;
                default:
                    break;
            }
        }


        private void Redo()
        {
            if (redoStack.Count == 0) return;
            undoRedoEvent e = redoStack.Pop();


            try
            {
                
                if (e == null) throw new Exception();
                undoStack.Push(e); //push the redo event onto the undo stack, so that you can undo changes made
               // Console.WriteLine("Redo: " + e.A + " to " + e.B);
                int temp = e.A;
                e.A = e.B;
                e.B = temp; //swap directions, so that we are now able to reverse the changes.

                UndoRedoEvents(e);

                //swap back. The reason for the swapping was to save having to create another function for events.
                temp = e.A;
                e.A = e.B;
                e.B = temp;


            }
            catch
            {


            }
            finally {

                undoToolStripMenuItem.Enabled = undoStack.Count > 1;
                redoToolStripMenuItem.Enabled = redoStack.Count > 1;  
            }
            if (e.redoEventAfter) Redo();
            else RefreshViews();
        }

        private void Undo()
        {
            if (undoStack.Count == 0) return;
            undoRedoEvent e = undoStack.Pop();
            try {
                
            if (e == null) throw new Exception();
           
            redoStack.Push(e); //when undoing, we can enqueue an item, so that we can redo it later.
            UndoRedoEvents(e); //carry out the appropriate action to undo the previous command.

            }
            catch {
                undoStack.Clear();
            }
            finally {
               // Console.WriteLine(e.task);
                //Console.WriteLine(e.B + " to " + e.A);
                undoToolStripMenuItem.Enabled = undoStack.Count > 1;
                redoToolStripMenuItem.Enabled = redoStack.Count > 1;  
                
            }

            if (e.undoEventAfter == true) Undo(); //if we have to undo another event, then recursively undo the event.
            else RefreshViews();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetStatusOfStructureForms(true);
            currentTT = new Timetable();
            undoStack = new Stack<undoRedoEvent>();
            redoStack = new Stack<undoRedoEvent>();
            //get command line arguments.
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                currentTT.Load(args[1]);
                return;
            }
            string appPath = AppDomain.CurrentDomain.BaseDirectory;

            //before we can write to the directory, we MUST set permissions, so that we are able to write to the Program Files area of the hard drive.
            //Failing to do this will result in the program crashing if not being 'Run as Adminstrator'.
            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(appPath);
            DirectorySecurity security = dInfo.GetAccessControl();
            security.SetAccessRule(new FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Allow));
            dInfo.SetAccessControl(security);



            if (!System.IO.File.Exists(appPath + "normal.TTT"))
            {
                System.IO.File.WriteAllBytes(appPath + "normal.TTT", TimeLord.Properties.Resources.Template); //write the contents of the TimeLord Standard template to the drive, if not found.
                MessageBox.Show("No default template has been created. The pre-designed template will therefore be used.","No Template");
            }
            currentTT.Load(appPath + "normal.TTT"); //load the standard template in. NB: Creating a new timetable will clear this template.

        }

        private void modifyYearGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yearView == null)
            {
                yearView = new Years();
                yearView.TT = currentTT;
            }
            this.Enabled = false; //focus on weekView form. Wait for a response.

            yearView.ShowDialog();
            this.Enabled = true;
            yearView = null; //clears the value from the heap
        }

        //drawing headings onto screen.
        private void DrawDefaultHeadings(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        
            int col = 2 * cellWidth;
          



            foreach (YearGroup year in currentTT.Years)
	            {
                    if (year.Visible)
                    {

                        year.yearBounds = new Rectangle(col + marginLeft, marginTop, cellWidth * currentTT.numberOfVisibleForms(year), (currentTT.numberOfTotalPeriods + 2) * cellHeight);
                        G.FillRectangle(headingBrush, col + marginLeft, marginTop, cellWidth * currentTT.numberOfVisibleForms(year), cellHeight);
                        G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                        G.DrawString(year.YearName, new Font("Segoe UI", 12), Brushes.Black, col, marginTop);

                        foreach (FormClass fc in year.Forms)
                        {
                            if (fc.visible)
                            {
                                fc.formBounds = new Rectangle(col + marginLeft, 2 * cellHeight + marginTop, cellWidth, cellHeight * currentTT.numberOfTotalPeriods);
                                G.FillRectangle(headingBrush, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                                G.DrawRectangle(Pens.Black, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                                G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, cellHeight + marginTop);
                                col += cellWidth;
                            }
                        }
                    }
                    else
                    {
                        year.yearBounds = new Rectangle(0, 0, 0, 0); //create a rectangle that cannot be clicked on.
                    }
            }

            int row = 2 * cellHeight;
                
            foreach (Day schoolDay in currentTT.Week)
            {
                
                schoolDay.dayBounds = new Rectangle(marginLeft, row + marginTop, cellWidth + col, cellHeight * schoolDay.PeriodsInDay.Count);
                G.FillRectangle(headingBrush, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.FillRectangle(Brushes.SkyBlue, schoolDay.dayBounds);
                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    singlePeriod.periodBounds = new Rectangle(marginLeft + 2 * cellWidth, row + marginTop, cellWidth + col, cellHeight);
                    G.FillRectangle(headingBrush, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                   // G.FillRectangle(Brushes.Purple,singlePeriod.periodBounds);
                }

            }
           

        }
        private void DrawStaffHeadings(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            int col = 2 * cellWidth;
            Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        

                foreach (Teacher staff in currentTT.Staff)
                {
                    if (staff.Visible)
                    {
                        staff.staffBounds = new Rectangle(col + marginLeft, marginTop + cellHeight, cellWidth, cellHeight * currentTT.numberOfTotalPeriods);
                        G.FillRectangle(headingBrush, col + marginLeft, marginLeft, cellWidth, cellHeight);
                        G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
                        G.DrawString(staff.TeacherAbbreviation, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, marginTop);
                        col += cellWidth;
                       // G.FillRectangle(Brushes.Green, staff.staffBounds);
                    }
                }
            

            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                schoolDay.dayBounds = new Rectangle(marginLeft, row + marginTop, cellWidth * row, cellHeight * schoolDay.PeriodsInDay.Count);
                G.FillRectangle(headingBrush, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);

                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    singlePeriod.periodBounds = new Rectangle(marginLeft + cellWidth, row + marginTop, cellWidth + cellWidth * row, cellHeight);
                    G.FillRectangle(headingBrush, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                }

            }


        }
        private void DrawRoomHeadings(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            int col = 2 * cellWidth;
             Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        
            //change this to a repeat until loop, as we will need to skip over some of the things.
            foreach (Room room in currentTT.Rooms)
            {
                if (room.Visible)
                {
                    room.roomBounds = new Rectangle(col + marginLeft, marginTop + cellHeight, cellWidth, cellHeight * currentTT.numberOfTotalPeriods);
                    G.FillRectangle(headingBrush, col + marginLeft, marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
                    G.DrawString(room.RoomCode, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, marginTop);

                    col += cellWidth;
                }
            }


            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                schoolDay.dayBounds = new Rectangle(marginLeft, row + marginTop, 2 * cellWidth * col, schoolDay.PeriodsInDay.Count * cellHeight);
                G.FillRectangle(headingBrush, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.DrawRectangle(Pens.Yellow, schoolDay.dayBounds);

                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    singlePeriod.periodBounds = new Rectangle(marginLeft + cellWidth, row + marginTop, cellWidth * col, cellHeight);
                    G.FillRectangle(headingBrush, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                //    G.DrawRectangle(Pens.Yellow, singlePeriod.periodBounds);
                   // G.FillRectangle(Brushes.Red, singlePeriod.periodBounds);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                }

            }


        }
        
        private void PopulateEditToolbar()
        {
            if (currentTT == null) return;

            foreach (YearGroup year in currentTT.Years)
                cb_year.Items.Add(year);
            foreach (Subject sub in currentTT.Subjects)
                cb_subject.Items.Add(sub);
            foreach (Room room in currentTT.Rooms)
                cb_room.Items.Add(room.RoomCode);
            foreach (Teacher staff in currentTT.Staff)
                cb_teacher.Items.Add(staff);
            
        }
        //drawing lesson blocks onto the screen.
        private void DrawMainViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        
            if (!currentTT.IsFinalised()) return;
            //int xCoord =  cellWidth + MarginLeft;
            int yCoord = cellHeight + marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + marginLeft;
                    yCoord += cellHeight;
                    for (int yearIndex = 0; yearIndex < currentTT.Years.Count; yearIndex++)
                    {
                        if (currentTT.Years[yearIndex].Visible)
                        {

                            for (int formIndex = 0; formIndex < currentTT.Years[yearIndex].Forms.Count; formIndex++)
                            {
                                if (currentTT.Years[yearIndex].Forms[formIndex].visible)
                                {
                                    Lesson obj = currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex];
                                    if (obj != null) currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].DrawOnPage(xCoord, yCoord, cellWidth, cellHeight, G);
                                    else if ((mainViewMode == ClickMode.Move) || (mainViewMode == ClickMode.InsertFromTray))
                                    {
                                        if (displayedItem != null)
                                        {
                                            int teacherIndex = currentTT.GetIndexOfStaff(displayedItem.TeacherAbbreviation);
                                            int roomIndex = currentTT.GetIndexOfRoom(displayedItem.RoomCode);

                                            if ((periodIndex != displayedItem.PeriodIndex & (currentTT.roomTT[dayIndex][periodIndex][roomIndex] != null | currentTT.staffTT[dayIndex][periodIndex][teacherIndex] != null)) && sw != null && sw.ElapsedMilliseconds >= 20)
                                            {
                                                Brush hatchedRedBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Red, Properties.Settings.Default.Screen_Color_Background);
                                                G.FillRectangle(hatchedRedBrush, xCoord, yCoord, cellWidth, cellHeight);
                                            }
                                        }
                                    }

                                    xCoord += cellWidth;
                                }
                            }
                        }
                    }
                }

            }

        }
        private void DrawStaffViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {

            Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        
            int yCoord = marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + marginLeft;
                    yCoord += cellHeight;

                    for (int staffIndex = 0; staffIndex < currentTT.Staff.Count; staffIndex++)
                    {
                        if (currentTT.Staff[staffIndex].Visible)
                        {
                            Lesson obj = currentTT.staffTT[dayIndex][periodIndex][staffIndex];
                            if (obj != null) obj.DrawOnPage(xCoord, yCoord, cellWidth, cellHeight, G);
                            xCoord += cellWidth;
                        }
                    }
                    // xCoord -= cellWidth;

                }
            }

        }
        private void DrawRoomViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            Brush lockedBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Locked);
            Brush invisibleBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Invisible);
            Brush presentLesson = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Normal);
            Brush headingBrush = new SolidBrush(TimeLord.Properties.Settings.Default.Screen_Color_Headings);
        
            int yCoord = marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + marginLeft;
                    yCoord += cellHeight;

                    for (int roomIndex = 0; roomIndex < currentTT.Rooms.Count; roomIndex++)
                    {
                        if (currentTT.Rooms[roomIndex].Visible)
                        {
                            Lesson obj = currentTT.roomTT[dayIndex][periodIndex][roomIndex];
                            if (obj != null) obj.DrawOnPage(xCoord, yCoord, cellWidth, cellHeight, G);
                            xCoord += cellWidth;
                        }
                    }
                   
                }
            }



        }
        private void DrawHWViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
           
            int yCoord = cellHeight + marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + marginLeft;
                    yCoord += cellHeight;
                    for (int yearIndex = 0; yearIndex < currentTT.Years.Count; yearIndex++)
                    {
                        if (currentTT.Years[yearIndex].Visible)
                        {
                            for (int formIndex = 0; formIndex < currentTT.Years[yearIndex].Forms.Count; formIndex++)
                            {
                                if (currentTT.Years[yearIndex].Forms[formIndex].visible)
                                {
                                    Lesson obj = currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex];
                                    if (obj != null) currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].DrawHomework(xCoord, yCoord, cellWidth, cellHeight, G);
                                    xCoord += cellWidth;
                                }

                            }
                        }
                     
                    }
                }
            }
        }
        private int calculateWidthOfMainPB(int cellWidth, int marginLeft, int pbWidth)
        {
            int width = 0;

            foreach (YearGroup year in currentTT.Years)
                width += year.Forms.Count;
            width *= cellWidth + marginLeft;
            width += 2 * cellWidth;
            return width > pbWidth ? width : pbWidth; //return the greatest of the two values.
        }
        private int calculateWidthOfRoomPB(int cellWidth, int marginLeft, int pbWidth)
        {
            int width = currentTT.Rooms.Count;
            width *= cellWidth;
            width += marginLeft;
            width += 2 * cellWidth;
            return width > pbWidth ? width : pbWidth;
        }
        private int calculateWidthOfStaffPB(int cellWidth, int marginLeft, int pbWidth)
        {
            int width = currentTT.Staff.Count;
            width *= cellWidth;
            width += marginLeft;
            width += 2 * cellWidth;
            return width > pbWidth ? width : pbWidth;
        }
        private int calculateHeightOfIMG(int cellHeight, int marginTop, int pbHeight)
        {
            int height = 0;

            foreach (Day d in currentTT.Week)
                height += d.PeriodsInDay.Count;

            height *= cellHeight + marginTop;
            return height > pbHeight ? height : pbHeight;

        }
        private void drawGridLines(int marginLeft, int marginTop, int cellWidth, int cellHeight, Graphics G, int maxWidth, int maxHeight)
        {

            for (int left = marginLeft; left < maxWidth; left += cellWidth)
                G.DrawLine(Pens.Gray, left, marginTop, left, maxHeight);

            for (int down = marginTop; down < maxHeight; down += cellHeight)
                G.DrawLine(Pens.Gray, marginLeft, down, maxWidth, down);

        }
        private void pb_mainView_Paint(object sender, PaintEventArgs e)
        {
            pb_mainView.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_Background;
            if (currentTT == null) return;
            int cellWidth = Properties.Settings.Default.Screen_CellWidth;
            int cellHeight = Properties.Settings.Default.Screen_CellHeight;
            int marginLeft = Properties.Settings.Default.Screen_MarginLeft;
            int marginTop = Properties.Settings.Default.Screen_MarginTop;

            pb_mainView.Width = calculateWidthOfMainPB(cellWidth, marginLeft, tabPage1.Width - 4);
            pb_mainView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage1.Height - 4);


            drawGridLines(marginLeft + 2 * cellWidth,marginTop + 2 * cellHeight ,cellWidth,cellHeight,e.Graphics,pb_mainView.Width,pb_mainView.Height);

            //Bitmap img = new Bitmap(ewidth, eheight);
            //pb_mainView.Image = img;
            if (mainViewMode != ClickMode.Normal) {
                Brush tempBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Gray, Properties.Settings.Default.Screen_Color_Background);
                Point pToDraw = getCellHoveredAbove(cursorPosition);
                e.Graphics.FillRectangle(tempBrush, Convert.ToInt32(pToDraw.X), Convert.ToInt32(pToDraw.Y), cellWidth, cellHeight);
            }

            DrawDefaultHeadings(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);

            if (!currentTT.IsFinalised()) return;
            DrawMainViewLessons(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);
                
           

            


        }
        private Point getCellHoveredAbove(Point cursorPosition)
        {
            Point pointToReturn = new Point(0, 0);

            pointToReturn.X = cursorPosition.X;
            

            pointToReturn.Y = cursorPosition.Y;
            pointToReturn.Y -= 2 * Properties.Settings.Default.Screen_CellHeight;

            int x = 0;
            do
            {
                x += Properties.Settings.Default.Screen_CellWidth;
            } while (x < cursorPosition.X - Properties.Settings.Default.Screen_CellWidth);

            int y = 0;
            do
            {
                y += Properties.Settings.Default.Screen_CellHeight;
            } while (y <= cursorPosition.Y - Properties.Settings.Default.Screen_CellHeight);
            pointToReturn.X = x + Properties.Settings.Default.Screen_MarginLeft;
            pointToReturn.Y = y + Properties.Settings.Default.Screen_MarginTop;


            return pointToReturn;
        }
        private void pb_homeworkView_Paint(object sender, PaintEventArgs e)
        {
            int cellWidth = Properties.Settings.Default.Screen_CellWidth;
            int cellHeight = Properties.Settings.Default.Screen_CellHeight;
            int marginLeft = Properties.Settings.Default.Screen_MarginLeft;
            int marginTop = Properties.Settings.Default.Screen_MarginTop;

            pb_homeworkView.BackColor = Properties.Settings.Default.Screen_Color_Background;
            if (currentTT == null) return;

            pb_homeworkView.Width = calculateWidthOfMainPB(cellWidth, marginLeft, tabPage4.Width - 4);
            pb_homeworkView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage4.Height - 4);
            drawGridLines(marginLeft + 2*cellWidth, marginTop, cellWidth, cellHeight, e.Graphics, pb_homeworkView.Width, pb_homeworkView.Height);
            DrawDefaultHeadings(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);

            if (!currentTT.IsFinalised()) return;
            DrawHWViewLessons(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);

           
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog();
        }
        private void modifyStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staffView = new staffManager();

            staffView.currentTT = currentTT;
            staffView.ShowDialog();
            staffView = null;
        }
       
        private void pm_staffView_Paint(object sender, PaintEventArgs e)
        {
            //Console.WriteLine("Status: " + mainViewMode);
            int cellWidth = Properties.Settings.Default.Screen_CellWidth;
            int cellHeight = Properties.Settings.Default.Screen_CellHeight;
            int marginLeft = Properties.Settings.Default.Screen_MarginLeft;
            int marginTop = Properties.Settings.Default.Screen_MarginTop;
            pb_staffView.BackColor = Properties.Settings.Default.Screen_Color_Background;
            if (currentTT == null) return;
            pb_staffView.Width = calculateWidthOfStaffPB(cellWidth, marginLeft, tabPage2.Width - 4);
            pb_staffView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage2.Height - 4);

            drawGridLines(marginLeft + 2*cellWidth, marginTop + cellHeight, cellWidth, cellHeight, e.Graphics, pb_staffView.Width, pb_staffView.Height);
            if (mainViewMode != ClickMode.Normal)
            {
                Brush tempBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Gray, Properties.Settings.Default.Screen_Color_Background);
                Point pToDraw = getCellHoveredAbove(cursorPosition);
                e.Graphics.FillRectangle(tempBrush, Convert.ToInt32(pToDraw.X), Convert.ToInt32(pToDraw.Y), cellWidth, cellHeight);
            }
            DrawStaffHeadings(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);


            if (!currentTT.IsFinalised()) return;
            DrawStaffViewLessons(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);
        }
        private void modifyRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roomView = new roomManager();
            roomView.currentTT = currentTT;
            roomView.ShowDialog();
            roomView = null;            
        }
        private void pb_roomView_Paint(object sender, PaintEventArgs e)
        {
            pb_roomView.BackColor = Properties.Settings.Default.Screen_Color_Background;

            if (currentTT == null) return;
            int cellWidth = Properties.Settings.Default.Screen_CellWidth;
            int cellHeight = Properties.Settings.Default.Screen_CellHeight;
            int marginLeft = Properties.Settings.Default.Screen_MarginLeft;
            int marginTop = Properties.Settings.Default.Screen_MarginTop;
            pb_roomView.Width = calculateWidthOfRoomPB(cellWidth, marginLeft, tabPage3.Width - 4);
            pb_roomView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage3.Height - 4);

            drawGridLines(marginLeft + 2 * cellWidth, marginTop, cellWidth, cellHeight, e.Graphics, pb_roomView.Width, pb_roomView.Height);
            if (mainViewMode != ClickMode.Normal)
            {
                Brush tempBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Gray, Properties.Settings.Default.Screen_Color_Background);
                Point pToDraw = getCellHoveredAbove(cursorPosition);
                e.Graphics.FillRectangle(tempBrush, Convert.ToInt32(pToDraw.X), Convert.ToInt32(pToDraw.Y), cellWidth, cellHeight);
            }


            DrawRoomHeadings(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);

           if (!currentTT.IsFinalised()) return;
           DrawRoomViewLessons(e.Graphics, cellWidth, cellHeight, marginLeft, marginTop);
           

        }
        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (YearGroup year in currentTT.Years)
            {
                year.Visible = true;
                foreach (FormClass form in year.Forms)
                    form.visible = true;
            }
            pb_mainView.Refresh();

        }
        private void viewAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //set the property to visible.
            foreach (Teacher staff in currentTT.Staff)
                staff.Visible = true;

            pb_staffView.Refresh();
        }
        private void viewAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            foreach (YearGroup year in currentTT.Years)
                year.Visible = true;

        }
        private void addLessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool finalBefore = currentTT.IsFinalised();
            
            //ADD LESSON
            lessonView = new AddLesson();
            lessonView.currentTT = currentTT;
            lessonView.ShowDialog();
            pb_mainView.Refresh();
            pb_homeworkView.Refresh();
            pb_staffView.Refresh();
            pb_roomView.Refresh();

            if (!finalBefore)
            {
                PopulateEditToolbar();
                SetStatusOfStructureForms(false); //This prevents the user from modifying the week/yeargroups/subjects/teachers etc. This will interfere with our project.
            }


            

        }

        private void modifySubjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SUBJECT EDITOR...
            subjectView = new subjectManager();
            subjectView.currentTT = currentTT;
            subjectView.ShowDialog();

        }
        private void viewSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //view selected on mainView.
            VisibleYearsAndForms normalViewTool = new VisibleYearsAndForms();
            normalViewTool.TT = currentTT;
            normalViewTool.ShowDialog();
        }
        private void viewSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //view selected on staff view
        }
        private void viewSelectedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //view selected on room view
        }
        private void viewSelectedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //view selected on homework view.
        }
        private void saveAs()
        {//save AS 
            if (dialog_save.ShowDialog() == DialogResult.OK)
            {
                Save(dialog_save.FileName);
              
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save
            if (currentFilename == "") saveAs();
            else
                Save(currentFilename);
            
        }


        private void Save(string filename)
        {
            if (currentTT.Save(filename))
            {
                lbl_status.Text = "Saved Successfully.";
                this.Text = appName + " | " + filename;
            }
            else
            {
                lbl_status.Text = "An error occured when saving.";
                MessageBox.Show("Couldn't save due to an error...");
            }

        }
     

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        
        
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    tabControl1.SelectTab(0);
                    break;
                case Keys.F2:
                    tabControl1.SelectTab(1);
                    break;
                case Keys.F3:
                    tabControl1.SelectTab(2);
                    break;
                case Keys.F4:
                    tabControl1.SelectTab(3);
                    break;
                case Keys.F5:
                    tabControl1.SelectedTab.Refresh();
                    break;
                
                case Keys.Up:
                    if (displayedItem == null) return;
                    displayedItem.selected = false;
                    int DayIndex = displayedItem.DayIndex;
                    int PeriodIndex = displayedItem.PeriodIndex;
                    int YearIndex = displayedItem.YearIndex;
                    int FormIndex = displayedItem.FormIndex;
                    do
                    {
                        if ((PeriodIndex == 0) && (DayIndex != 0)) { 
                            DayIndex--;
                            PeriodIndex = currentTT.Week[DayIndex].PeriodsInDay.Count;
                        }
                        else if ((PeriodIndex == 0)) return;
                        PeriodIndex--;
                        displayedItem = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];

                        

                    } while (displayedItem == null);
                    displayedItem.selected = true;
                    DisplayInfoAboutLesson(displayedItem);
                    pb_mainView.Refresh();
                    break;
                case Keys.Down:
                    if (displayedItem == null) return;
                    displayedItem.selected = false;
                    DayIndex = displayedItem.DayIndex;
                    PeriodIndex = displayedItem.PeriodIndex;
                    YearIndex = displayedItem.YearIndex;
                    FormIndex = displayedItem.FormIndex;
                    
                    do
                    {
                        if ((PeriodIndex == currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (DayIndex != currentTT.Week.Count - 1)) { 
                            DayIndex++;
                            PeriodIndex = -1;
                        }
                        else if ((PeriodIndex == currentTT.Week[DayIndex].PeriodsInDay.Count - 1)) return;
                        PeriodIndex++;

                        displayedItem = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];

                       

                    } while (displayedItem == null);
                    displayedItem.selected = true;
                    DisplayInfoAboutLesson(displayedItem);
                    pb_mainView.Refresh();
                    break;
                case Keys.Left:
                    e.Handled = true; //this escapes the automatic movement of tab pages, which can be annoying.
                   
                    
                    if (displayedItem == null) return;
                    displayedItem.selected = false;
                    DayIndex = displayedItem.DayIndex;
                    PeriodIndex = displayedItem.PeriodIndex;
                    YearIndex = displayedItem.YearIndex;
                    FormIndex = displayedItem.FormIndex;
                    
                    
                    do
                    {
                        if ((FormIndex == 0) && (YearIndex != 0)) {
                            YearIndex--;
                            FormIndex = currentTT.Years[YearIndex].Forms.Count;
                        }
                        else if ((YearIndex == 0) && (FormIndex == 0)) return;
                        FormIndex--;

                        displayedItem = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];

                      

                    } while (displayedItem == null);
                    displayedItem.selected = true;
                    DisplayInfoAboutLesson(displayedItem);
                    pb_mainView.Refresh();
                    break;

                case Keys.Right:
                    e.Handled = true;
                    if (displayedItem == null) return;
                    displayedItem.selected = false;
                    DayIndex = displayedItem.DayIndex;
                    PeriodIndex = displayedItem.PeriodIndex;
                    YearIndex = displayedItem.YearIndex;
                    FormIndex = displayedItem.FormIndex;
                    
                    do
                    {
                        if ((FormIndex == currentTT.Years[YearIndex].Forms.Count - 1) && (YearIndex != currentTT.Years.Count - 1)) {
                            YearIndex++;
                            FormIndex = -1;
                        }
                        else if ((YearIndex == currentTT.Years.Count - 1) && (FormIndex == currentTT.Years[YearIndex].Forms.Count - 1)) return;
                        FormIndex++;

                        displayedItem = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];
                        //MessageBox.Show("RIGHT!!!");
                       

                    } while (displayedItem == null);
                    displayedItem.selected = true;
                    DisplayInfoAboutLesson(displayedItem);
                    pb_mainView.Refresh();
                    break;
                case Keys.Delete:
                    if (displayedItem == null)
                    {
                        MessageBox.Show("There is no item selected for deletion.", "Delete Lesson");
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to delete the selected lesson: " + displayedItem.ToString() + "?", "Delete Lesson", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        currentTT.DeleteLesson(displayedItem);
                        RefreshViews();
                    }
                    break;
                default:
                    break;
            }
        }

        //private void DetectClickForMain(out int DayIndex, out int PeriodIndex, out int YearIndex, out int FormIndex,out Lesson selectedLesson,Rectangle cursor)
        //{
           

        //    DayIndex = -1;
        //    FormIndex = -1;
        //    YearIndex = -1;
        //    PeriodIndex = -1;
        //    selectedLesson = null;
        //    do
        //    {
        //        DayIndex++;
        //    } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
        //    Console.WriteLine("DayIndex: " + DayIndex);
        //    if (DayIndex >= currentTT.Week.Count) return;
        //    //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

           

        //    do
        //    {
        //        PeriodIndex++;
        //    } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
        //    Console.WriteLine("Period Index: " + PeriodIndex);
        //    if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

        //    // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

         

        //    do
        //    {
        //        YearIndex++;
        //    } while ((YearIndex < currentTT.Years.Count - 1) && (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)));
        //    Console.WriteLine("Year Index: " + YearIndex);
        //    // if (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)) return;
        //    if (YearIndex >= currentTT.Years.Count) return;
            
       
        //    do
        //    {
        //        FormIndex++;
        //    } while ((FormIndex < currentTT.Years[YearIndex].Forms.Count() - 1) && (!currentTT.Years[YearIndex].Forms[FormIndex].formBounds.IntersectsWith(cursor)));
        //    Console.WriteLine("Form Index: " + FormIndex);
        //    if (FormIndex >= currentTT.Years[YearIndex].Forms.Count()) return;
            
        //    try
        //    {
        //        selectedLesson = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];
        //    }
        //    catch
        //    {

        //    }

        //}
        
      
       
        private void pb_mainView_MouseClick(object sender, MouseEventArgs e)
        {

            //RENAME TO MouseUp.
            if (sw != null) sw.Stop();
            if (displayedItem != null)
            {
                displayedItem.selected = false;
                
            }
            if (!currentTT.IsFinalised()) return;
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int YearIndex;
            int PeriodIndex;
            int DayIndex;
            int FormIndex;
            Lesson selectedLesson;
            DayIndex = -1;
            FormIndex = -1;
            YearIndex = -1;
            PeriodIndex = -1;
            selectedLesson = null;
            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
            //Console.WriteLine("DayIndex: " + DayIndex);
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;



            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Period Index: " + PeriodIndex);
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

            // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;



            do
            {
                YearIndex++;
            } while ((YearIndex < currentTT.Years.Count - 1) && (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Year Index: " + YearIndex);
            // if (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)) return;
            if (YearIndex >= currentTT.Years.Count) return;


            do
            {
                FormIndex++;
            } while ((FormIndex < currentTT.Years[YearIndex].Forms.Count() - 1) && (!currentTT.Years[YearIndex].Forms[FormIndex].formBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Form Index: " + FormIndex);
            if (FormIndex >= currentTT.Years[YearIndex].Forms.Count()) return;

            try
            {
                selectedLesson = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];
            }
            catch
            {

            }
        
            
            if (mainViewMode == ClickMode.AddLessonViaMenu)
            {
                lessonView = new AddLesson();
                lessonView.currentTT = currentTT;
                lessonView.LoadData();
                lessonView.cb_day.SelectedIndex = DayIndex;

                lessonView.cb_periodStart.SelectedIndex = PeriodIndex;
                lessonView.cb_yearGroup.SelectedIndex = YearIndex;
                lessonView.cb_class.SelectedIndex = FormIndex;
                mainViewMode = ClickMode.Normal;
                lessonView.ShowDialog();
               
                pb_mainView.Refresh();
                lbl_status.Text = "Ready";
                return;
            }
            if (mainViewMode == ClickMode.InsertFromTray)
            {
                if (lv_tray.SelectedIndices.Count == 0)
                {
                    mainViewMode = ClickMode.Normal;
                    return;
                }
                currentTT.MoveFromTrayToMainTT(DayIndex, PeriodIndex, YearIndex, FormIndex, lv_tray.SelectedIndices[0]);
                mainViewMode = ClickMode.Normal;
                RefreshTrayItems();
                return;
            }
            else if (mainViewMode == ClickMode.FindStaff)
            {
                mainViewMode = ClickMode.Normal;
                lbl_status.Text = currentTT.FreeStaffCodes(Convert.ToByte(DayIndex), Convert.ToByte(PeriodIndex));
                
            }
            else if (mainViewMode == ClickMode.FindRooms) {
                mainViewMode = ClickMode.Normal;
                lbl_status.Text =  currentTT.FreeRoomCodes(Convert.ToByte(DayIndex), Convert.ToByte(PeriodIndex));
          
            }

            selectedLesson = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];
            
            if (mainViewMode == ClickMode.Move && sw.ElapsedMilliseconds > timeForMove)
            {
                
                //MessageBox.Show("Move: " + FormIndex + " " + YearIndex);
                if (displayedItem.locked)
                {
                    MessageBox.Show("One cannot move a locked lesson. To move it, please unlock the lesson.", "Lesson locking");
                    mainViewMode = ClickMode.Normal;
                    return;   
                }

                bool ConditionA = (displayedItem.YearIndex != YearIndex);
                bool ConditionB = (displayedItem.PeriodIndex != PeriodIndex);
                bool ConditionC = (displayedItem.FormIndex != FormIndex);



                if (ConditionA) AddNodeToUndo(Event.MoveYear, displayedItem.YearIndex, YearIndex, displayedItem,false,ConditionB|ConditionC);
                if (ConditionB) AddNodeToUndo(Event.MovePeriod, displayedItem.PeriodIndex, PeriodIndex, displayedItem, ConditionA,ConditionC);



                if (ConditionC) AddNodeToUndo(Event.MoveForm, displayedItem.FormIndex, FormIndex,displayedItem, ConditionA | ConditionB,false);
                currentTT.MoveForms(displayedItem, Convert.ToByte(FormIndex));
                
                currentTT.MovePeriods(displayedItem, Convert.ToByte(PeriodIndex));
                currentTT.MoveYears(displayedItem, Convert.ToByte(YearIndex));
               
                
              
                mainViewMode = ClickMode.Normal;
                RefreshViews();
                displayedItem.selected = true;
                return;
            }
            
            if (selectedLesson == null)
            {
                displayedItem = null;
                pb_mainView.Refresh();
            }
            
            else
            {
                selectedLesson.selected = true;
                displayedItem = selectedLesson;;
                pb_mainView.Refresh();

                cb_year.SelectedIndex = YearIndex;
                cb_room.SelectedIndex = currentTT.GetIndexOfRoom(selectedLesson.RoomCode);
                cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(selectedLesson.TeacherAbbreviation);
                cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);


                cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
                cb_form.Text = currentTT.Years[YearIndex].Forms[FormIndex].FormName;
                btn_invisible.Checked = selectedLesson.invisible;
                btn_locked.Checked = selectedLesson.locked;
            }
            mainViewMode = ClickMode.Normal;
        }
    
        
        private void cb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            


            if (cb_year.Text == "") return; //probably a faster way of doing this.
            if (currentTT == null) return;
            cb_form.Items.Clear();
            YearGroup yearToDisplay = (YearGroup)cb_year.SelectedItem;

            foreach (FormClass f in yearToDisplay.Forms)
                cb_form.Items.Add(f);
            
            if (displayedItem == null) return;
            if (displayedItem.YearIndex == cb_year.SelectedIndex) return;
            if (displayedItem.locked)
            {
                MessageBox.Show("The Year Group cannot be changed as the lesson is locked.", "Lesson Locking");
                return;
            }
            string msg = "";
            if (currentTT.IsClassClash(displayedItem.DayIndex, displayedItem.PeriodIndex, cb_year.SelectedIndex, 0, out msg))
            {
                cb_year.SelectedIndex = displayedItem.YearIndex;
                cb_form.SelectedIndex = displayedItem.FormIndex;
                MessageBox.Show(msg, "Class Clash");
                return;
            }
            AddNodeToUndo(Event.MoveYear, displayedItem.YearIndex, cb_year.SelectedIndex,displayedItem,false,false);
            currentTT.MoveYears(displayedItem, Convert.ToByte(cb_year.SelectedIndex));
            
            RefreshViews();
            
        }
        public void AddNodeToUndo(Event task, int A, int B,Lesson lessonToUndo,bool undoEventAfter,bool redoEventAfter)
        {
            undoRedoEvent eventToAdd = new undoRedoEvent(task, A, B,lessonToUndo,undoEventAfter,redoEventAfter);
            if (redoStack.Count != 0) redoStack.Clear(); //when undoing, this will automatically get rid of all redo values.
            undoStack.Push(eventToAdd);
            undoToolStripMenuItem.Enabled = undoStack.Count > 1;
            redoToolStripMenuItem.Enabled = redoStack.Count > 1;
        }
        public void RefreshViews()
        {
            pb_mainView.Refresh();
            pb_roomView.Refresh();
            pb_staffView.Refresh();
            pb_homeworkView.Refresh();
        }
        private void cb_form_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            if (cb_form.SelectedIndex == displayedItem.FormIndex) return;
            if (displayedItem.locked)
            {
                MessageBox.Show("The form cannot be changed as the lesson is locked.", "Lesson Locking");
                return;
            }
            string msg = "";
            if (currentTT.IsClassClash(displayedItem.DayIndex, displayedItem.PeriodIndex, cb_year.SelectedIndex, cb_form.SelectedIndex, out msg))
            {
                cb_form.SelectedIndex = displayedItem.FormIndex;
                MessageBox.Show(msg, "Class Clash");
                return;
            }
            AddNodeToUndo(Event.MoveForm, displayedItem.FormIndex, cb_form.SelectedIndex,displayedItem,false,false);
            currentTT.MoveForms(displayedItem, Convert.ToByte(cb_form.SelectedIndex));
            RefreshViews();
        }

        private void cb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            if (displayedItem.SubjectAbbreviation == cb_subject.Text) return;
            if (displayedItem.locked)
            {
                MessageBox.Show("The subject cannot be changed as the lesson is locked.", "Lesson Locking");
                return;
            }
            AddNodeToUndo(Event.MoveSubject, currentTT.GetIndexOfSubject(displayedItem.SubjectAbbreviation), cb_subject.SelectedIndex, displayedItem,false,false);
            displayedItem.SubjectAbbreviation = cb_subject.Text;
            RefreshViews();
        }

        private void cb_room_SelectedIndexChanged(object sender, EventArgs e) {
            if (displayedItem == null) return;
            if (displayedItem.RoomCode == cb_room.Text) return;
            string msg = "";

            if (currentTT.IsRoomClash(displayedItem.DayIndex, displayedItem.PeriodIndex, cb_room.SelectedIndex, out msg))
            {
                cb_room.SelectedIndex = currentTT.GetIndexOfRoom(displayedItem.RoomCode);
                MessageBox.Show(msg, "Room Clash");
            }
            else
            {
                AddNodeToUndo(Event.MoveRoom, currentTT.GetIndexOfRoom(displayedItem.RoomCode), cb_room.SelectedIndex,displayedItem,false,false);
                currentTT.MoveRooms(displayedItem, Convert.ToByte(cb_room.SelectedIndex));
            }

            RefreshViews();
        }

        private void cb_room_Click(object sender, EventArgs e)
        {

        }

        private void pb_staffView_MouseClick(object sender, MouseEventArgs e)
        {
            //After adding the new feature, please be sure to update the system maintenance!!!

            if (sw != null) sw.Stop();
            if (displayedItem != null)
            {
                displayedItem.selected = false;
                btn_locked.Checked = false;
                btn_invisible.Checked = false;
            }

            //Console.WriteLine("");
            //Console.WriteLine("Click detected: ");
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int DayIndex = -1;

            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
            //Console.WriteLine("DayIndex: " + DayIndex);
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

            int PeriodIndex = -1;

            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Period Index: " + PeriodIndex);
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

            // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

            int StaffIndex = -1;

            do
            {
                StaffIndex++;
            } while ((StaffIndex < currentTT.Staff.Count - 1) && (!currentTT.Staff[StaffIndex].staffBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Staff Index: " + StaffIndex);
            // if (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)) return;
            if (StaffIndex >= currentTT.Staff.Count) return;
            Lesson selectedLesson = null;
            try
            {
                selectedLesson = (Lesson)currentTT.staffTT[DayIndex][PeriodIndex][StaffIndex];
            }
            catch (Exception)
            {
                return;
            }
            //Console.WriteLine("Status: " + mainViewMode);
            if (mainViewMode == ClickMode.AddLessonViaMenu)
            {
                mainViewMode = ClickMode.Normal;
                lessonView = new AddLesson();
                lessonView.currentTT = currentTT;
                lessonView.LoadData();
                lessonView.cb_teacherCode.SelectedIndex = StaffIndex;
                lessonView.cb_day.SelectedIndex = DayIndex;
                lessonView.cb_periodStart.SelectedIndex = PeriodIndex;
                lessonView.ShowDialog();
                
                lbl_status.Text = "Ready";
                return;
            }
       
            

            if (mainViewMode == ClickMode.Move && sw.ElapsedMilliseconds > timeForMove)
            {
                if (displayedItem.locked)
                {
                    MessageBox.Show("One cannot move a locked lesson. To move it, please unlock the lesson.", "Lesson locking");
                    return;
                }
                bool ConditionA = (displayedItem.TeacherAbbreviation != currentTT.Staff[StaffIndex].TeacherAbbreviation);
                bool ConditionB = (displayedItem.PeriodIndex != PeriodIndex);
                bool eventAfter = ConditionA;

                if (ConditionA) AddNodeToUndo(Event.MoveStaff, currentTT.GetIndexOfStaff(displayedItem.TeacherAbbreviation), StaffIndex, displayedItem, false,ConditionB);
                if (ConditionB) AddNodeToUndo(Event.MovePeriod, displayedItem.PeriodIndex, PeriodIndex, displayedItem, eventAfter,false);
               

                currentTT.MoveTeacher(displayedItem, Convert.ToByte(StaffIndex));
                currentTT.MovePeriods(displayedItem, Convert.ToByte(PeriodIndex));
               
                mainViewMode = ClickMode.Normal;
                RefreshViews();
                displayedItem.selected = true;
                return;
            }
            if (selectedLesson == null) displayedItem = null;
            else
            {

                selectedLesson.selected = true;
                displayedItem = selectedLesson;
                pb_staffView.Refresh();

                cb_year.SelectedIndex = selectedLesson.YearIndex;
                cb_room.SelectedIndex = currentTT.GetIndexOfRoom(selectedLesson.RoomCode);
                cb_teacher.SelectedIndex = StaffIndex;
                cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);
                btn_locked.Checked = selectedLesson.locked;
                btn_invisible.Checked = selectedLesson.invisible;
                
                if (selectedLesson.locked)
                {
                    editLessonToolStripMenuItem.Enabled = false; //we can't edit anything that's locked.

                }
                else
                {
                    editLessonToolStripMenuItem.Enabled = true;
                }

                cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
                cb_form.Text = currentTT.Years[selectedLesson.YearIndex].Forms[selectedLesson.FormIndex].FormName;
            }
            mainViewMode = ClickMode.Normal;
        }

        private void pb_roomView_MouseClick(object sender, MouseEventArgs e)
        {
            if (sw != null) sw.Stop();

            if (displayedItem != null)
            {
                displayedItem.selected = false;
                btn_locked.Checked = false;
                btn_invisible.Checked = false;

            }

            //Console.WriteLine("");
            //Console.WriteLine("Click detected: ");
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int DayIndex = -1;

            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
            //Console.WriteLine("DayIndex: " + DayIndex);
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

            int PeriodIndex = -1;

            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
            //lbl_status.Text =("Period Index: " + PeriodIndex);
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

            // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

            int RoomIndex = -1;

            do
            {
                RoomIndex++;
            } while ((RoomIndex < currentTT.Rooms.Count - 1) && (!currentTT.Rooms[RoomIndex].roomBounds.IntersectsWith(cursor)));
            //Console.WriteLine("Room Index: " + RoomIndex);
            // if (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)) return;
            if (RoomIndex >= currentTT.Rooms.Count) return;
            Lesson selectedLesson = null;
            try
            {
                selectedLesson = (Lesson)currentTT.roomTT[DayIndex][PeriodIndex][RoomIndex];
            }
            catch (Exception)
            {
                return;
            }
            //if (selectedLesson == null) return;
            //selectedLesson.selected = true;
            //displayedItem = selectedLesson;
            //pb_roomView.Refresh();

            //cb_year.SelectedIndex = selectedLesson.YearIndex;
            //cb_room.SelectedIndex = currentTT.GetIndexOfRoom(selectedLesson.RoomCode);
            //cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(selectedLesson.TeacherAbbreviation);
            //cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);


            //cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
            //cb_form.Text = currentTT.Years[selectedLesson.YearIndex].Forms[selectedLesson.FormIndex].FormName;
            //btn_invisible.Checked = selectedLesson.invisible;
            //btn_locked.Checked = selectedLesson.locked;

            if (mainViewMode == ClickMode.AddLessonViaMenu)
            {
                mainViewMode = ClickMode.Normal;
                lessonView = new AddLesson();
                lessonView.currentTT = currentTT;
                lessonView.LoadData();
                lessonView.cb_room.SelectedIndex = RoomIndex;
                lessonView.cb_day.SelectedIndex = DayIndex;
                lessonView.cb_periodStart.SelectedIndex = PeriodIndex;
                lessonView.ShowDialog();

                lbl_status.Text = "Ready";
                return;

            }


            if (mainViewMode == ClickMode.Move && sw.ElapsedMilliseconds > timeForMove)
            {
                if (displayedItem.locked)
                {
                    MessageBox.Show("One cannot move a locked lesson. To move it, please unlock the lesson.", "Lesson locking");
                    return;
                }
                //lbl_status.Text = "PeriodIndex: " + PeriodIndex;

                bool ConditionA = (displayedItem.PeriodIndex != PeriodIndex);
                bool ConditionB = displayedItem.RoomCode != currentTT.Rooms[RoomIndex].RoomCode;
                if (ConditionA) AddNodeToUndo(Event.MovePeriod, displayedItem.PeriodIndex, PeriodIndex, displayedItem,false,ConditionB);
                if (ConditionB) AddNodeToUndo(Event.MoveRoom, currentTT.GetIndexOfRoom(displayedItem.RoomCode), RoomIndex, displayedItem,ConditionA,false);
               



                currentTT.MovePeriods(displayedItem, Convert.ToByte(PeriodIndex));
                currentTT.MoveRooms(displayedItem, Convert.ToByte(RoomIndex));
                mainViewMode = ClickMode.Normal;
                RefreshViews();
                displayedItem.selected = true;
                return;
            }
            if (selectedLesson == null) displayedItem = null;
            else
            {
                selectedLesson.selected = true;
                displayedItem = selectedLesson;
                pb_roomView.Refresh();

                cb_year.SelectedIndex = selectedLesson.YearIndex;
                cb_room.SelectedIndex = RoomIndex;
                cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(selectedLesson.TeacherAbbreviation);
                cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);
                btn_locked.Checked = selectedLesson.locked;
                btn_invisible.Checked = selectedLesson.invisible;

                if (selectedLesson.locked)
                {
                    editLessonToolStripMenuItem.Enabled = false;

                }
                else
                {
                    editLessonToolStripMenuItem.Enabled = true;
                }

                cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
                cb_form.Text = currentTT.Years[selectedLesson.YearIndex].Forms[selectedLesson.FormIndex].FormName;



            }
            mainViewMode = ClickMode.Normal;
        }

        private void btn_locked_Click(object sender, EventArgs e)
        {
            if (displayedItem != null) {
                AddNodeToUndo(Event.ChangeLocked, displayedItem.locked ? 255 : 0, btn_locked.Checked ? 255 : 0,displayedItem,false,false);
                displayedItem.locked = btn_locked.Checked;

                
            }
            RefreshViews();
        }

        private void btn_invisible_Click(object sender, EventArgs e)
        {
            if (displayedItem != null)
            {
                AddNodeToUndo(Event.ChangeInvisible, displayedItem.invisible ? 255 : 0, btn_invisible.Checked ? 255 : 0, displayedItem, false,false);
                displayedItem.invisible = btn_invisible.Checked;
                
                
                
            }
            RefreshViews();
        }


    

        private void cb_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            if (displayedItem.TeacherAbbreviation == cb_teacher.Text) return;
            string msg = "";

            if (currentTT.IsStaffClash(displayedItem.DayIndex, displayedItem.PeriodIndex, cb_teacher.SelectedIndex, out msg))
            {
                MessageBox.Show(msg, "Staff Clash");
                cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(displayedItem.TeacherAbbreviation);
                return;
            }

            AddNodeToUndo(Event.MoveStaff, currentTT.GetIndexOfStaff(displayedItem.TeacherAbbreviation), cb_teacher.SelectedIndex,displayedItem,false,false);
            currentTT.MoveTeacher(displayedItem, Convert.ToByte(cb_teacher.SelectedIndex));
            RefreshViews();

        }

   

        private void btn_addLessonViaSelection_Click(object sender, EventArgs e)
        {
            
            mainViewMode = ClickMode.AddLessonViaMenu; //this makes sure that we select blank places to add new lessons to.
            if (displayedItem != null) displayedItem.selected = false; //deselect the current selected one.
            lbl_status.Text = "Click where the lesson should be added.";
            
        }

        private void cb_homework_TextChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            int homeworkAmount = 0;
            if (Int32.TryParse(cb_homework.Text,out homeworkAmount))
            {
                if (homeworkAmount < 256 && 0 <= homeworkAmount)
                {
                    AddNodeToUndo(Event.ChangeHomework, displayedItem.homeworkAmount, homeworkAmount,displayedItem,false,false);
               
                    displayedItem.homeworkAmount = Convert.ToByte(homeworkAmount);
                   }
                RefreshViews();
            }
        }

        private void btn_deleteLesson_Click(object sender, EventArgs e)
        {
            currentTT.DeleteLesson(displayedItem);
            RefreshViews();
        }

        private void btn_locked_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_locked.Checked) btn_locked.Image = TimeLord.Properties.Resources.locked;
            else btn_locked.Image = TimeLord.Properties.Resources.unlocked;
        }

        private void btn_invisible_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_invisible.Checked) btn_invisible.Image = TimeLord.Properties.Resources.Invisible;
            else btn_invisible.Image = TimeLord.Properties.Resources.visible;
        }

        private void deleteLessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //delete lesson
            if (displayedItem != null)
            {
                currentTT.DeleteLesson(displayedItem);
                displayedItem = null;

            }
            else
            {
                MessageBox.Show("Please select a lesson first.", "TimeLord");

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
                //are you sure you want to save notice?

            if (dialog_open.ShowDialog() == DialogResult.OK)
            {
                currentTT = null;
                currentTT = new Timetable();
                currentTT.Load(dialog_open.FileName);
                currentFilename = dialog_open.FileName;
                this.Text = appName + " | " + currentFilename;
                PopulateEditToolbar();

                RefreshViews();
                SetStatusOfStructureForms(false);
            }
        }

   

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog myPrintDialog = new PrintDialog();
            myPrintDialog.currentTT = currentTT;
            myPrintDialog.ShowDialog();
        }

     
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void editLessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //edit lesson
            if (!currentTT.IsFinalised()) return;
            if (displayedItem == null) return;
            lessonView = new AddLesson();
            lessonView.currentTT = currentTT;
            lessonView.mode = AddLessonMode.Edit; //loads in editing mode.

            lessonView.LoadLesson(displayedItem);
            lessonView.ShowDialog();
            RefreshViews();

        }


        private void pb_mainView_MouseDown(object sender, MouseEventArgs e)
        {
            sw = new System.Diagnostics.Stopwatch();
          
            if (displayedItem == null) return;
            if (currentTT == null) return;
            if (mainViewMode != ClickMode.Normal) return; //gets rid of the issue when returning from tray.
            sw.Start(); 
            mainViewMode = ClickMode.Move;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            lbl_status.Text = "Select a period on the main view to view free staff.";
            mainViewMode = ClickMode.FindStaff;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mainViewMode = ClickMode.FindRooms;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //properties button
            Form2 propertiesWindow = new Form2();
            propertiesWindow.ShowDialog();
        }

        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
        {
            
            pb_mainView.Refresh();
            cursorPosition = new Point(e.Location.X, e.Location.Y);
            
        }

        private void pb_mainView_MouseMove(object sender, MouseEventArgs e)
        {
            pb_mainView.Refresh();
            cursorPosition = new Point(e.Location.X, e.Location.Y);
            
        }

        private void lv_tray_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_tray.SelectedIndices.Count != 0)
            {
                displayedItem.selected = false;
                displayedItem = currentTT.Tray[lv_tray.SelectedIndices[0]];
                DisplayInfoAboutLesson(displayedItem);
                lv_tray.Refresh();
            }
        }
        private void RefreshTrayItems()
        {
            lv_tray.Items.Clear();
            foreach (Lesson tl in currentTT.Tray)
            {
                ListViewItem lvi = new ListViewItem(tl.ToString());
                Color colorForItem = new Color();

                if (tl.locked && tl.invisible) colorForItem = Properties.Settings.Default.Screen_Color_LockedAndInvisible;
                else if (tl.locked) colorForItem = Properties.Settings.Default.Screen_Color_Locked;
                else if (tl.invisible) colorForItem = Properties.Settings.Default.Screen_Color_Invisible;

                lvi.BackColor = colorForItem;
                lv_tray.Items.Add(lvi);
            }
            lv_tray.Refresh();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //move to tray
            if (displayedItem == null) return; //cannot move anything to tray
            currentTT.MoveToTray(displayedItem);
            RefreshViews();
            RefreshTrayItems();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (lv_tray.SelectedItems.Count == 0) return;
            mainViewMode = ClickMode.InsertFromTray;
            pb_mainView.Focus();
        }

     

        private void lv_tray_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) && (lv_tray.SelectedIndices.Count > 0))
            {
                currentTT.Tray.RemoveAt(lv_tray.SelectedIndices[0]);
            }

        }

        private void pb_staffView_MouseDown(object sender, MouseEventArgs e)
        {
            sw = new System.Diagnostics.Stopwatch();

            if (displayedItem == null) return;
            if (currentTT == null) return;

            if (mainViewMode != ClickMode.Normal) return; //gets rid of the issue when returning from tray.
            sw.Start();
            mainViewMode = ClickMode.Move;
        }

        private void pb_staffView_MouseMove(object sender, MouseEventArgs e)
        {
            pb_staffView.Refresh();
            cursorPosition = new Point(e.Location.X, e.Location.Y);
            //Console.WriteLine("Status: " + mainViewMode);
            //Console.WriteLine();
        }

        private void pb_roomView_Click(object sender, EventArgs e)
        {

        }

        private void pb_roomView_MouseDown(object sender, MouseEventArgs e)
        {
            sw = new System.Diagnostics.Stopwatch();

            if (displayedItem == null) return;
            if (currentTT == null) return;

            if (mainViewMode != ClickMode.Normal) return; // avoids return to tray issue
            sw.Start();
            mainViewMode = ClickMode.Move;
        }

        private void pb_mainView_Click(object sender, EventArgs e)
        {

        }

        private void pb_roomView_MouseMove(object sender, MouseEventArgs e)
        {
            pb_roomView.Refresh();
            cursorPosition = new Point(e.Location.X, e.Location.Y);
        }

        private void cb_year_Click(object sender, EventArgs e)
        {
            
        }
        public class undoRedoEvent
        {

            public int A;
            public int B;
            public bool undoEventAfter;
            public bool redoEventAfter;
            public Event task;
            public Lesson lessonToUndo;

            public undoRedoEvent(Event task, int A_Index, int B_Index,Lesson lessonToUndo,bool undoEventAfter, bool redoEventAfter)
            {
                A = A_Index;
                B = B_Index;
                this.undoEventAfter = undoEventAfter;
                this.redoEventAfter = redoEventAfter;
                this.task = task;
                this.lessonToUndo = lessonToUndo;
            }
            //Move from A -> B. This is where the undo function is useful.
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void hideDataCloudPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed == false)
            {

                splitContainer1.Panel2Collapsed = true;
                hideDataCloudPanelToolStripMenuItem.Text = "Show Data Cloud Panel";
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
                hideDataCloudPanelToolStripMenuItem.Text = "Hide Data Cloud Panel";

            }
        }

        private void hideEditToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Visible == true)
            {

                toolStrip1.Visible = false;
                hideEditToolbarToolStripMenuItem.Text = "Show Edit Toolbar";

            }
            else
            {
                toolStrip1.Visible = true;
                hideEditToolbarToolStripMenuItem.Text = "Hide Edit Toolbar";
            }

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                System.Diagnostics.Process.Start(appPath + "User_Manual.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't open the user manual. It might not be saved to disk.");
            }
        
        }
}

}
    
