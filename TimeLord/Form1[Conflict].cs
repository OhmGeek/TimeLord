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
    public partial class Form1 : Form
    {
        Timetable currentTT = null;
        private Brush headingBrush = Brushes.White;
        private Lesson displayedItem = null;
        private Brush invisibleBrush = Brushes.Yellow;
        private Brush presentLesson = Brushes.Blue;
        private Brush lockedBrush = Brushes.Red;
        private bool addLessonViaMenu = false;
        private const int cellWidth = 100;
        private const int cellHeight = 20;
        private const int marginTop = 2;
        private const int MarginLeft = 2;
        private string currentFilename = "";
        private Weeks weekView = null; //stores the week view when we load it.
        private Years yearView = null;
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

        private void viewAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {

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
                MessageBox.Show("DELETE!!!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region Enabled/Disabled things
            SetStatusOfStructureForms(true);
            #endregion



          
            currentTT = new Timetable();

            if (!System.IO.File.Exists("normal.TTT"))
            {
                System.IO.File.WriteAllBytes("normal.TTT", TimeLord.Properties.Resources.Template); //write the contents of the TimeLord Standard template to the drive, if not found.
            }
            currentTT.Load("normal.TTT"); //load the standard template in. NB: Creating a new timetable will clear this template.

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

        private void pb_mainView_Click(object sender, EventArgs e)
        {
            
        }

        //drawing headings onto screen.
        private void DrawDefaultHeadings(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            int col = 2 * cellWidth;
            
            foreach (YearGroup year in currentTT.Years)
	            {
                    year.yearBounds = new Rectangle(col + marginLeft, marginTop, cellWidth * year.Forms.Count, (currentTT.numberOfTotalPeriods + 2) * cellHeight);
                    
                    G.FillRectangle(headingBrush, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                    G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                    G.DrawString(year.YearName,new Font("Segoe UI",12),Brushes.Black,col,marginTop);
                    
                foreach (FormClass fc in year.Forms)
                {
                    fc.formBounds = new Rectangle(col + marginLeft, 2 * cellHeight + marginTop, cellWidth, cellHeight * currentTT.numberOfTotalPeriods);
                    G.FillRectangle(headingBrush, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                    G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, cellHeight + marginTop);
                    col += cellWidth;
                   
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

            //change this to a repeat until loop, as we will need to skip over some of the things.
            foreach (Room room in currentTT.Rooms)
            {

                room.roomBounds = new Rectangle(col + marginLeft, marginTop + cellHeight, cellWidth, cellHeight * currentTT.numberOfTotalPeriods);
                G.FillRectangle(headingBrush, col + marginLeft, marginTop, cellWidth, cellHeight);
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
                G.DrawString(room.RoomCode, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, marginTop);
                col += cellWidth;
            }


            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                schoolDay.dayBounds = new Rectangle(marginLeft, row + marginTop, 2 * cellWidth * col, cellHeight);
                G.FillRectangle(headingBrush, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);

                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    singlePeriod.periodBounds = new Rectangle(marginLeft + cellWidth, row + marginTop, cellWidth * col, cellHeight);
                    G.FillRectangle(headingBrush, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
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
                        for (int formIndex = 0; formIndex < currentTT.Years[yearIndex].Forms.Count; formIndex++)
                        {
                            Lesson obj = currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex];
                            if (obj != null) currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].DrawOnPage(xCoord, yCoord, cellWidth, cellHeight,G,presentLesson,invisibleBrush, lockedBrush);
                            xCoord += cellWidth;
                          
                        }
                     
                    }
                }
            }

        }
        private void DrawStaffViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            int yCoord = marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + marginLeft;
                    yCoord += cellHeight;

                    for (int staffIndex = 0; staffIndex < currentTT.Staff.Count; staffIndex++)
                    {
                        Lesson obj = currentTT.staffTT[dayIndex][periodIndex][staffIndex];
                        if (obj != null) obj.DrawOnPage(xCoord, yCoord, cellWidth, cellHeight, G,presentLesson,invisibleBrush, lockedBrush);
                        xCoord += cellWidth;

                    }
                    // xCoord -= cellWidth;

                }
            }

        }
        private void DrawRoomViewLessons(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop)
        {
            int yCoord = marginTop;
            for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
            {
                for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                {
                    int xCoord = 2 * cellWidth + MarginLeft;
                    yCoord += cellHeight;

                    for (int roomIndex = 0; roomIndex < currentTT.Rooms.Count; roomIndex++)
                    {
                        Lesson obj = currentTT.roomTT[dayIndex][periodIndex][roomIndex];
                        if (obj != null) obj.DrawOnPage(xCoord, yCoord, cellWidth, cellHeight, G, presentLesson, invisibleBrush, lockedBrush);
                        xCoord += cellWidth;
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
                        for (int formIndex = 0; formIndex < currentTT.Years[yearIndex].Forms.Count; formIndex++)
                        {
                            Lesson obj = currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex];
                            if (obj != null) currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].DrawHomework(xCoord, yCoord, cellWidth, cellHeight, G);
                            xCoord += cellWidth;

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
            width *= cellWidth;
            width += marginLeft;
            return width > pbWidth ? width : pbWidth; //return the greatest of the two values.
        }
        private int calculateWidthOfRoomPB(int cellWidth, int marginLeft, int pbWidth)
        {
            int width = currentTT.Rooms.Count;
            width *= cellWidth;
            width += marginLeft;
            return width > pbWidth ? width : pbWidth;
        }
        private int calculateWidthOfStaffPB(int cellWidth, int marginLeft, int pbWidth)
        {
            int width = currentTT.Staff.Count;
            width *= cellWidth;
            width += marginLeft;
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
        private void pb_mainView_Paint(object sender, PaintEventArgs e)
        {

            if (currentTT == null) return;


            pb_mainView.Width = calculateWidthOfMainPB(cellWidth, MarginLeft, tabPage1.Width - 4);
            pb_mainView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage1.Height - 4);



            //Bitmap img = new Bitmap(ewidth, eheight);
            //pb_mainView.Image = img;


            DrawDefaultHeadings(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

            if (!currentTT.IsFinalised()) return;
            DrawMainViewLessons(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

        }
        private void pb_homeworkView_Paint(object sender, PaintEventArgs e)
        {
            if (currentTT == null) return;

            pb_homeworkView.Width = calculateWidthOfMainPB(cellWidth, MarginLeft, tabPage1.Width - 4);
            pb_homeworkView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage1.Height - 4);

            DrawDefaultHeadings(e.Graphics, 100, 20, 2, 2);

            if (!currentTT.IsFinalised()) return;
            DrawHWViewLessons(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

           
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
        private void pm_staffView_Click(object sender, EventArgs e)
        {
           
        }
        private void pm_staffView_Paint(object sender, PaintEventArgs e)
        {
            if (currentTT == null) return;
            pb_staffView.Width = calculateWidthOfStaffPB(cellWidth, MarginLeft, tabPage2.Width - 4);
            pb_staffView.Height = calculateWidthOfStaffPB(cellHeight, marginTop, tabPage2.Height - 4);


            DrawStaffHeadings(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

            if (!currentTT.IsFinalised()) return;
            DrawStaffViewLessons(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);
        }
        private void modifyRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roomView = new roomManager();
            roomView.currentTT = currentTT;
            roomView.ShowDialog();
            roomView = null;            
        }
        private void pb_roomView_Click(object sender, EventArgs e)
        {
            
        }
        private void pb_roomView_Paint(object sender, PaintEventArgs e)
        {
            if (currentTT == null) return;
            pb_roomView.Width = calculateWidthOfRoomPB(cellWidth, MarginLeft, tabPage3.Width - 4);
            pb_roomView.Height = calculateHeightOfIMG(cellHeight, marginTop, tabPage3.Height - 4);
            DrawRoomHeadings(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

           if (!currentTT.IsFinalised()) return;
           DrawRoomViewLessons(e.Graphics, cellWidth, cellHeight, MarginLeft, marginTop);

        }
        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach  (YearGroup year in currentTT.Years)
                year.Visible = true;
            

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
        private void pb_homeworkView_Click(object sender, EventArgs e)
        {

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
                default:
                    break;
            }
        }

        


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void pb_mainView_MouseClick(object sender, MouseEventArgs e)
        {
            if (displayedItem != null)
            {
                displayedItem.selected = false;
                
            }
   
         
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int DayIndex = -1;

            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count-1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

            int PeriodIndex = -1;

            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
      
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

           // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

            int YearIndex = -1;

            do
            {
                YearIndex++;
            } while ((YearIndex < currentTT.Years.Count - 1) && (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)));
           
           // if (!currentTT.Years[YearIndex].yearBounds.IntersectsWith(cursor)) return;
            if (YearIndex >= currentTT.Years.Count) return;
            int FormIndex = -1;

            do
            {
                FormIndex++;
            } while ((FormIndex < currentTT.Years[YearIndex].Forms.Count() - 1) && (!currentTT.Years[YearIndex].Forms[FormIndex].formBounds.IntersectsWith(cursor)));
           
            if (FormIndex >= currentTT.Years[YearIndex].Forms.Count()) return;
            Lesson selectedLesson = null;
            try{
            selectedLesson = currentTT.mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex];
            }
            catch
            {

            }
            if (addLessonViaMenu)
            {
                lessonView = new AddLesson();
                lessonView.currentTT = currentTT;
                lessonView.LoadData();
                lessonView.cb_day.SelectedIndex = DayIndex;

                lessonView.cb_periodStart.SelectedIndex = PeriodIndex;
                lessonView.cb_yearGroup.SelectedIndex = YearIndex;
                lessonView.cb_class.SelectedIndex = FormIndex;
                lessonView.ShowDialog();
                addLessonViaMenu = false;
                pb_mainView.Refresh();
                lbl_status.Text = "Ready";
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
                displayedItem = selectedLesson;
                pb_mainView.Refresh();

                cb_year.SelectedIndex = YearIndex;
                cb_room.SelectedIndex = currentTT.GetIndexOfRoom(selectedLesson.RoomCode);
                cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(selectedLesson.TeacherAbbreviation);
                cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);


                cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
                cb_form.Text = currentTT.Years[YearIndex].Forms[FormIndex].FormName;
                
            }
    }

        private void cb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_year.Text == "") return; //probably a faster way of doing this.
            if (currentTT == null) return;
            cb_form.Items.Clear();
         
            YearGroup yearToDisplay = (YearGroup)cb_year.SelectedItem;

            foreach (FormClass f in yearToDisplay.Forms)
                cb_form.Items.Add(f);

            cb_form.SelectedIndex = 0;
            if (displayedItem == null) return;
            if (displayedItem.YearIndex == cb_year.SelectedIndex) return;
            currentTT.MoveYears(displayedItem, Convert.ToByte(cb_year.SelectedIndex));
            RefreshViews();
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
            currentTT.MoveForms(displayedItem, Convert.ToByte(cb_form.SelectedIndex));
            RefreshViews();
        }

        private void cb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayedItem != null) displayedItem.SubjectAbbreviation = cb_subject.Text;
            RefreshViews();
        }

        private void cb_room_SelectedIndexChanged(object sender, EventArgs e) {
            if (displayedItem == null) return;
            if (displayedItem.RoomCode == cb_room.Text) return;
            currentTT.MoveRooms(displayedItem, Convert.ToByte(cb_room.SelectedIndex));
            RefreshViews();
        }

        private void cb_room_Click(object sender, EventArgs e)
        {

        }

        private void pb_staffView_MouseClick(object sender, MouseEventArgs e)
        {
            if (displayedItem != null)
            {
                displayedItem.selected = false;

            }

           
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int DayIndex = -1;

            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
            
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

            int PeriodIndex = -1;

            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
        
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

            // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

            int StaffIndex = -1;

            do
            {
                StaffIndex++;
            } while ((StaffIndex < currentTT.Staff.Count - 1) && (!currentTT.Staff[StaffIndex].staffBounds.IntersectsWith(cursor)));
      
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

            if (addLessonViaMenu == true)
            {
                lessonView = new AddLesson();
                lessonView.currentTT = currentTT;
                lessonView.LoadData();
                lessonView.cb_teacherCode.SelectedIndex = StaffIndex;
                lessonView.cb_day.SelectedIndex = DayIndex;
                lessonView.cb_periodStart.SelectedIndex = PeriodIndex;
                lessonView.ShowDialog();
                addLessonViaMenu = false;
                lbl_status.Text = "Ready";
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


                cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
                cb_form.Text = currentTT.Years[selectedLesson.YearIndex].Forms[selectedLesson.FormIndex].FormName;
            }
        }

        private void pb_roomView_MouseClick(object sender, MouseEventArgs e)
        {
            if (displayedItem != null)
            {
                displayedItem.selected = false;

            }

         
            Rectangle cursor = new Rectangle(e.X, e.Y, 1, 1);

            int DayIndex = -1;

            do
            {
                DayIndex++;
            } while ((DayIndex < currentTT.Week.Count - 1) && (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)));
        
            if (DayIndex >= currentTT.Week.Count) return;
            //if (!currentTT.Week[DayIndex].dayBounds.IntersectsWith(cursor)) return;

            int PeriodIndex = -1;

            do
            {
                PeriodIndex++;
            } while ((PeriodIndex < currentTT.Week[DayIndex].PeriodsInDay.Count - 1) && (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)));
          
            if (PeriodIndex >= currentTT.Week[DayIndex].PeriodsInDay.Count) return;

            // if (!currentTT.Week[DayIndex].PeriodsInDay[PeriodIndex].periodBounds.IntersectsWith(cursor)) return;

            int RoomIndex = -1;

            do
            {
                RoomIndex++;
            } while ((RoomIndex < currentTT.Rooms.Count - 1) && (!currentTT.Rooms[RoomIndex].roomBounds.IntersectsWith(cursor)));
       
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
            if (selectedLesson == null) return;
            selectedLesson.selected = true;
            displayedItem = selectedLesson;
            pb_roomView.Refresh();

            cb_year.SelectedIndex = selectedLesson.YearIndex;
            cb_room.SelectedIndex = currentTT.GetIndexOfRoom(selectedLesson.RoomCode);
            cb_teacher.SelectedIndex = currentTT.GetIndexOfStaff(selectedLesson.TeacherAbbreviation);
            cb_subject.SelectedIndex = currentTT.GetIndexOfSubject(selectedLesson.SubjectAbbreviation);


            cb_homework.Text = Convert.ToString(selectedLesson.homeworkAmount);
            cb_form.Text = currentTT.Years[selectedLesson.YearIndex].Forms[selectedLesson.FormIndex].FormName;
            btn_invisible.Checked = selectedLesson.invisible;
            btn_locked.Checked = selectedLesson.locked;
        }

        private void btn_locked_Click(object sender, EventArgs e)
        {
            if (displayedItem != null) displayedItem.locked = btn_locked.Checked;
        }

        private void btn_invisible_Click(object sender, EventArgs e)
        {
            if (displayedItem != null) displayedItem.invisible = btn_invisible.Checked;
        }

        private void cb_form_Click(object sender, EventArgs e)
        {

        }

        private void cb_year_Click(object sender, EventArgs e)
        {

        }

        private void cb_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            if (displayedItem.TeacherAbbreviation == cb_teacher.Text) return;
            currentTT.MoveTeacher(displayedItem, Convert.ToByte(cb_teacher.SelectedIndex));
            RefreshViews();

        }

        private void pb_mainView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
          
        }

        private void tabPage1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        void tabPage1_LostFocus(object sender, System.EventArgs e)
        {
          
        }
        private void tabPage1_Leave(object sender, EventArgs e)
        {
           

        }

        private void btn_addLessonViaSelection_Click(object sender, EventArgs e)
        {
            if (!currentTT.IsFinalised()) currentTT.Finalise();
            addLessonViaMenu = true; //this makes sure that we select blank places to add new lessons to.
            lbl_status.Text = "Click where the lesson should be added.";
            
        }

        private void cb_homework_TextChanged(object sender, EventArgs e)
        {
            if (displayedItem == null) return;
            int homeworkAmount = 0;
            if (Int32.TryParse(cb_homework.Text,out homeworkAmount))
            {
                if (homeworkAmount < 256 && 0 <= homeworkAmount)
                    displayedItem.homeworkAmount = Convert.ToByte(homeworkAmount);
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
                this.Text = appName + " |" + currentFilename;
                PopulateEditToolbar();

                RefreshViews();
                SetStatusOfStructureForms(false);
            }
        }

      

}

}
    
