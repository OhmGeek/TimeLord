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
    public partial class PrintDialog : Form
    {
        //define the variables used in the printing. We print one to each page.
        private int pTypeIndex = 0;
        private int pSelectionIndex = -1;
        private int expectedNumberOfPages = 0;
        private int pageCounter = 0;
       
        public Timetable currentTT = null;
        public PrintDialog()
        {
            InitializeComponent();
        }
        private void DrawPageNumbers(Graphics G, int number)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            RectangleF rect = new RectangleF(0, print_document.DefaultPageSettings.Bounds.Height - 30,print_document.DefaultPageSettings.Bounds.Width, 30);
            G.FillRectangle(Brushes.White, rect); //draw white, so that anything extra below the footer is covered up. 
            
            G.DrawString(number.ToString(), new Font("Segoe UI", 14), Brushes.Black, rect,sf);
            G.DrawLine(Pens.Black, 0, print_document.DefaultPageSettings.Bounds.Height - 30, print_document.DefaultPageSettings.Bounds.Width, print_document.DefaultPageSettings.Bounds.Height - 30);

        }
        private void DrawTitle(Graphics G, string TitleText)
        {
            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            G.DrawString(TitleText, new Font("Segoe UI", 14, FontStyle.Underline), Brushes.Black, new RectangleF(0, 10, Width, 30),sf);
            G.DrawLine(Pens.Gray,0,40,print_document.DefaultPageSettings.Bounds.Width,40);
        }

        private void DrawSchoolView(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop,bool printInvisible)
        {
            int col = 2 * cellWidth;

            foreach (YearGroup year in currentTT.Years)
            {
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                G.DrawString(year.YearName, new Font("Segoe UI", 12), Brushes.Black, col, marginTop);

                foreach (FormClass fc in year.Forms)
                {
                    G.DrawRectangle(Pens.Black, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                    G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, cellHeight + marginTop);
                    col += cellWidth;

                }
            }

            int row = 2 * cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.FillRectangle(Brushes.SkyBlue, schoolDay.dayBounds);
                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                    // G.FillRectangle(Brushes.Purple,singlePeriod.periodBounds);
                }

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
                                if ((obj != null) && (!obj.invisible | (obj.invisible & printInvisible)))
                                {
                                    G.DrawRectangle(Pens.Black, xCoord, yCoord, cellWidth, cellHeight);
                                    G.DrawString(currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].ToString(), new Font("Segoe UI", 12), Brushes.Black, xCoord, yCoord);
                                }
                                xCoord += cellWidth;

                            }

                        }
                    }
                }
            }


        }
        private void DrawHomework(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop, bool printInvisible)
        {
            int col = 2 * cellWidth;

            foreach (YearGroup year in currentTT.Years)
            {
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                G.DrawString(year.YearName, new Font("Segoe UI", 12), Brushes.Black, col, marginTop);

                foreach (FormClass fc in year.Forms)
                {
                    G.DrawRectangle(Pens.Black, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                    G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, cellHeight + marginTop);
                    col += cellWidth;

                }
            }

            int row = 2 * cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.FillRectangle(Brushes.SkyBlue, schoolDay.dayBounds);
                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                    
                }

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
                                if (obj != null && (!obj.invisible | (obj.invisible & printInvisible)))
                                {
                                    G.DrawRectangle(Pens.Black, xCoord, yCoord, cellWidth, cellHeight);
                                    G.DrawString(currentTT.mainTT[dayIndex][periodIndex][yearIndex][formIndex].homeworkAmount + " mins.", new Font("Segoe UI", 12), Brushes.Black, xCoord, yCoord);
                                }
                                xCoord += cellWidth;

                            }

                        }
                    }
                }
            }


        }
        private void DrawSubject(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop,int subjectIndex,bool printInvisible)
        {
            int col = 2 * cellWidth;

            foreach (YearGroup year in currentTT.Years)
            {
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth * year.Forms.Count, cellHeight);
                G.DrawString(year.YearName, new Font("Segoe UI", 12), Brushes.Black, col, marginTop);

                foreach (FormClass fc in year.Forms)
                {
                    G.DrawRectangle(Pens.Black, col + marginLeft, cellHeight + marginTop, cellWidth, cellHeight);
                    G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, cellHeight + marginTop);
                    col += cellWidth;

                }
            }

            int row = 2 * cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.FillRectangle(Brushes.SkyBlue, schoolDay.dayBounds);
                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                    G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                    // G.FillRectangle(Brushes.Purple,singlePeriod.periodBounds);
                }

                if (currentTT.IsFinalised())
                {
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
                                    if ((obj != null) && (currentTT.Subjects[subjectIndex].SubjectAbbreviation == obj.SubjectAbbreviation) && (!obj.invisible | (obj.invisible & printInvisible)))
                                    {
                                        G.DrawRectangle(Pens.Black, xCoord, yCoord, cellWidth, cellHeight);
                                        G.DrawString(obj.ToString(), new Font("Segoe UI", 12), Brushes.Black, xCoord, yCoord);
                                    }
                                    xCoord += cellWidth;

                                }

                            }
                        }
                    }
                }
            }

        }       
        private void DrawStaff(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop, int staffIndex, bool printInvisible)
        {
            int col = cellWidth;
            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
               G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft + col, marginTop);
               col += cellWidth;
                
                

            }

            
            foreach (Period singlePeriod in currentTT.Week[0].PeriodsInDay)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                row += cellHeight;
            }

            if (currentTT.IsFinalised())
            {

                col = cellWidth;
                for (int d = 0; d < currentTT.Week.Count; d++)
                {
                    row = cellHeight;
                    for (int p = 0; p < currentTT.Week[d].PeriodsInDay.Count; p++)
                    {
                        Lesson selectedLesson = null;
                        selectedLesson = currentTT.staffTT[d][p][staffIndex];

                        if ((selectedLesson != null) && (!selectedLesson.invisible | (selectedLesson.invisible & printInvisible)))
                        {
                            G.DrawRectangle(Pens.Black, marginLeft + col, row + marginTop, cellWidth, cellHeight);
                            G.DrawString(selectedLesson.ToString(), new Font("Segoe UI", 12), Brushes.Black, marginLeft + col, row + marginTop);
                        }
                        row += cellHeight;
                    }
                    col += cellWidth;
                }
            }

        }
        private void DrawRoom(Graphics G, int cellWidth, int cellHeight, int marginLeft, int marginTop, int roomIndex, bool printInvisible)
        {
            int col = cellWidth;
            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft + col, marginTop);
                col += cellWidth;



            }


            foreach (Period singlePeriod in currentTT.Week[0].PeriodsInDay)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                row += cellHeight;
            }

            col = cellWidth;

            if (currentTT.IsFinalised())
            {

                for (int d = 0; d < currentTT.Week.Count; d++)
                {
                    row = cellHeight;
                    for (int p = 0; p < currentTT.Week[d].PeriodsInDay.Count; p++)
                    {
                        Lesson selectedLesson = null;
                        selectedLesson = currentTT.roomTT[d][p][roomIndex];
            

                        if (selectedLesson != null && (!selectedLesson.invisible | (selectedLesson.invisible & printInvisible)))
                        {
                            G.DrawRectangle(Pens.Black, marginLeft + col,row + marginTop, cellWidth, cellHeight);
                            G.DrawString(currentTT.roomTT[d][p][roomIndex].ToString(), new Font("Segoe UI", 12), Brushes.Black, marginLeft + col, row + marginTop);
                        }
                        row += cellHeight;
                    }
                    col += cellWidth;
                }
            }

        }

        private void lb_typeOfPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            //here, change the items in the other lb.
            lb_selectedItems.Enabled = true;
            lb_selectedItems.Items.Clear();
            switch (lb_typeOfPrint.SelectedIndex)
            {
                case 0:
                    //Year Timetables
                    for (int i = 0; i < currentTT.Years.Count; i++)
                    {
                        lb_selectedItems.Items.Add(currentTT.Years[i]);
                        if (currentTT.Years[i].selectedForPrint) lb_selectedItems.SetItemCheckState(i, CheckState.Checked);
                  
                    }
                    break;
                case 1:
                    //Staff Timetables
                    for (int i = 0; i < currentTT.Staff.Count; i++) {
                        lb_selectedItems.Items.Add(currentTT.Staff[i]);
                          if (currentTT.Staff[i].selectedForPrint) lb_selectedItems.SetItemCheckState(i,CheckState.Checked);
                  
                    }
                    break;
                case 2:
                    //Room Timetables
                    for (int i = 0; i < currentTT.Rooms.Count; i++) {
                        lb_selectedItems.Items.Add(currentTT.Rooms[i]);
                        if (currentTT.Rooms[i].selectedForPrint) lb_selectedItems.SetItemCheckState(i, CheckState.Checked);
                    }
                    break;
                case 3:
                    //Subject Timetables
                    for (int i = 0; i < currentTT.Subjects.Count; i++) { 
                        lb_selectedItems.Items.Add(currentTT.Subjects[i]);
                        if (currentTT.Subjects[i].selectedForPrinting) lb_selectedItems.SetItemCheckState(i, CheckState.Checked);
                    }
                    break;
                case 4:
                    //Homework Timetables
                    lb_selectedItems.Items.Add("Whole School Homework Timetable");
                    if (currentTT.homeworkSelectedForPrinting) lb_selectedItems.SetItemCheckState(0, CheckState.Checked);
                    break;
                case 5:
                    //Whole School Timetable
                    lb_selectedItems.Items.Add("Whole School Timetable");
                    if (currentTT.mainSelectedForPrinting) lb_selectedItems.SetItemCheckState(0, CheckState.Checked);
                    break;
                default:
                    lb_selectedItems.Enabled = false;
                    break;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void PrintDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void lb_selectedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            print_previewWindow.Refresh();
        }

        private void lb_selectedItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool changed = false;
            

            switch (lb_typeOfPrint.SelectedIndex)
            {
                    
                case 0:
                    changed = currentTT.Years[e.Index].selectedForPrint != (e.NewValue == CheckState.Checked);
                    currentTT.Years[e.Index].selectedForPrint = e.NewValue == CheckState.Checked;
                    break;
                case 1:
                    changed = currentTT.Staff[e.Index].selectedForPrint != (e.NewValue == CheckState.Checked);
                    currentTT.Staff[e.Index].selectedForPrint = e.NewValue == CheckState.Checked;
                    break;
                case 2:
                    changed = currentTT.Rooms[e.Index].selectedForPrint != (e.NewValue == CheckState.Checked);
                    currentTT.Rooms[e.Index].selectedForPrint = e.NewValue == CheckState.Checked;
                    break;
                case 3:
                    changed = currentTT.Subjects[e.Index].selectedForPrinting != (e.NewValue == CheckState.Checked);
                    currentTT.Subjects[e.Index].selectedForPrinting = e.NewValue == CheckState.Checked;
                    break;
                case 4:
                    changed = currentTT.homeworkSelectedForPrinting != (e.NewValue == CheckState.Checked);
                    currentTT.homeworkSelectedForPrinting = e.NewValue == CheckState.Checked;
                    break;
                case 5:
                    changed = currentTT.mainSelectedForPrinting != (e.NewValue == CheckState.Checked);
                    currentTT.mainSelectedForPrinting = e.NewValue == CheckState.Checked;
                    break;
                   
                default:
                    break;
            }
            if (e.NewValue == CheckState.Checked && changed) expectedNumberOfPages++;
            if (e.NewValue == CheckState.Unchecked) expectedNumberOfPages--;
            //Console.WriteLine("Expected Number of Pages: " + expectedNumberOfPages);
            
                
        }

        private void DrawYear(Graphics G, int Width, int Height,int cellWidth, int cellHeight, int marginTop, int marginLeft,int YearIndex)
        {
            int col = 2 * cellWidth;
            YearGroup year = currentTT.Years[YearIndex];
                foreach (FormClass fc in year.Forms)
                {
                   
                    G.DrawRectangle(Pens.Black, col + marginLeft, marginTop, cellWidth, cellHeight);
                    G.DrawString(fc.FormName, new Font("Segoe UI", 12), Brushes.Black, col + marginLeft, marginTop);
                    col += cellWidth;

                }
            

            int row = cellHeight;

            foreach (Day schoolDay in currentTT.Week)
            {
                G.DrawRectangle(Pens.Black, marginLeft, row + marginTop, cellWidth, cellHeight);
                G.DrawString(schoolDay.DayName, new Font("Segoe UI", 12), Brushes.Black, marginLeft, row + marginTop);
                //G.FillRectangle(Brushes.SkyBlue, schoolDay.dayBounds);
                foreach (Period singlePeriod in schoolDay.PeriodsInDay)
                {
                   G.DrawRectangle(Pens.Black, marginLeft + cellWidth, row + marginTop, cellWidth, cellHeight);
                    G.DrawString(singlePeriod.PeriodDisplay, new Font("Segoe UI", 12), Brushes.Black, marginLeft + cellWidth, row + marginTop);
                    row += cellHeight;
                    // G.FillRectangle(Brushes.Purple,singlePeriod.periodBounds);
                }
            }

                bool printInvisible = ck_printInvisible.Checked;
                if (!currentTT.IsFinalised()) return;
                //int xCoord =  cellWidth + MarginLeft;
                int yCoord =  marginTop;
                for (int dayIndex = 0; dayIndex < currentTT.Week.Count; dayIndex++)
                {
                    for (int periodIndex = 0; periodIndex < currentTT.Week[dayIndex].PeriodsInDay.Count; periodIndex++)
                    {
                        int xCoord = 2 * cellWidth + marginLeft;
                        yCoord += cellHeight;
                        
                            for (int formIndex = 0; formIndex < currentTT.Years[YearIndex].Forms.Count; formIndex++)
                            {
                                Lesson obj = currentTT.mainTT[dayIndex][periodIndex][YearIndex][formIndex];
                                if ((obj != null) && (!obj.invisible | (obj.invisible & printInvisible)))
                                {
                                    G.DrawRectangle(Pens.Black, xCoord, yCoord, cellWidth, cellHeight);
                                    G.DrawString(currentTT.mainTT[dayIndex][periodIndex][YearIndex][formIndex].ToString(), new Font("Segoe UI", 12), Brushes.Black, xCoord, yCoord);
                                }
                                xCoord += cellWidth;

                            }

                        }
                    }
                
            
           
        }

        private void print_document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            bool pageFinished = false;
            Graphics G = e.Graphics;
            int Width = e.MarginBounds.Width;
            int Height = e.MarginBounds.Height;
            
            try
            {
                if (expectedNumberOfPages <= 0) throw new Exception("You have not selected anything to print!");
                //go through the type of print until we find a selected one.
               
                do
                {
                    
                    pSelectionIndex++;
                    
                    switch (pTypeIndex)
                    {
                        case 0:
                            if (pSelectionIndex > currentTT.Years.Count - 1)
                            {
                                pTypeIndex++;
                                pSelectionIndex = -1;
                                break;
                            }
                            if (currentTT.Years[pSelectionIndex].selectedForPrint) {
                                DrawTitle(G,currentTT.Years[pSelectionIndex].YearName);
                                DrawYear(G, Width, Height, Properties.Settings.Default.Print_Year_Width, Properties.Settings.Default.Print_Year_Height, Properties.Settings.Default.Print_Header_Height, Properties.Settings.Default.Print_Staff_MarginLeft, pSelectionIndex);
                                
                                pageFinished = true;
                                pageCounter++;
                            }
                            
                            break;
                        case 1:

                            if (pSelectionIndex > currentTT.Staff.Count - 1)
                            {
                                pTypeIndex++;
                                pSelectionIndex = -1;
                                break;
                            }
                            if (currentTT.Staff[pSelectionIndex].selectedForPrint)
                            {
                                DrawTitle(G,currentTT.Staff[pSelectionIndex].TeacherName);
                                DrawStaff(G, Properties.Settings.Default.Print_Staff_Width, Properties.Settings.Default.Print_Staff_Height, Properties.Settings.Default.Print_Staff_MarginLeft, Properties.Settings.Default.Print_Header_Height, pSelectionIndex, ck_printInvisible.Checked);
                                
                                pageFinished = true;
                                pageCounter++;
                            }

                            break;
                        case 2:
                            #region determine whether to jump into the next section
                            if (pSelectionIndex > currentTT.Rooms.Count - 1)
                            {
                                pTypeIndex++;
                                pSelectionIndex = -1;
                                break;
                            }
                            #endregion

                            if (currentTT.Rooms[pSelectionIndex].selectedForPrint)
                            {
                                DrawTitle(G, currentTT.Rooms[pSelectionIndex].RoomCode);
                                DrawRoom(G, Properties.Settings.Default.Print_Rooms_Width, Properties.Settings.Default.Print_Rooms_Height, Properties.Settings.Default.Print_Rooms_MarginLeft, 40, pSelectionIndex, ck_printInvisible.Checked);
                                pageFinished = true;
                                pageCounter++;
                            }
                            break;
                        case 3:
                             if (pSelectionIndex > currentTT.Subjects.Count - 1)
                            {
                                pTypeIndex++;
                                pSelectionIndex = -1;
                                break;
                            }
                            if (currentTT.Subjects[pSelectionIndex].selectedForPrinting) {
                                DrawTitle(G,currentTT.Subjects[pSelectionIndex].SubjectName);
                                DrawSubject(G, Properties.Settings.Default.Print_Subject_Width, Properties.Settings.Default.Print_Subject_Height, Properties.Settings.Default.Print_Subject_MarginLeft, Properties.Settings.Default.Print_Header_Height, pSelectionIndex, ck_printInvisible.Checked);
                                pageFinished = true;
                                pageCounter++;
                            }
                            break;
                        case 4:
                             if (pSelectionIndex > 0)
                            {
                                pTypeIndex++;
                                pSelectionIndex = -1;
                                break;
                            }
                            if (currentTT.homeworkSelectedForPrinting)
                            {
                                DrawTitle(G,"School Homework Timetable");
                                DrawHomework(G, Properties.Settings.Default.Print_Homework_Width, Properties.Settings.Default.Print_Homework_Height, Properties.Settings.Default.Print_Homework_MarginLeft, Properties.Settings.Default.Print_Header_Height, ck_printInvisible.Checked);
                               // DrawHomework()
                                pageFinished = true;
                                pageCounter++;
                            }
                            break;
                        case 5:
                            if (pSelectionIndex > 0)
                            {
                                break;
                            }
                            if (currentTT.mainSelectedForPrinting)
                            {
                                DrawTitle(G,"School Timetable");
                                DrawSchoolView(G, Properties.Settings.Default.Print_School_Width, Properties.Settings.Default.Print_School_Height, Properties.Settings.Default.Print_School_MarginLeft, Properties.Settings.Default.Print_Header_Height, ck_printInvisible.Checked);
                                pageFinished = true;
                                pageCounter++;
                            }
                            break;
                        default:
                            
                            break;
                    }
                    //find the title, now draw everything else.
                    
                } while ((!pageFinished) && (pTypeIndex < lb_typeOfPrint.Items.Count));
                DrawPageNumbers(G, pageCounter);
                if (pageCounter < expectedNumberOfPages) e.HasMorePages = true;
                if (pageCounter >= expectedNumberOfPages) e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occured: " + Environment.NewLine + ex.Message);
            }
            finally
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            print_previewWindow.Document = print_document;
            pageCounter = 0;
            pSelectionIndex = -1;
            pTypeIndex = 0;
            print_previewWindow.StartPage = 0;
            lbl_pageDisplayed.Text = "Page " + (print_previewWindow.StartPage+1) + " / " + expectedNumberOfPages;
            print_previewWindow.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (print_previewWindow.StartPage != 0) print_previewWindow.StartPage--;
            lbl_pageDisplayed.Text = "Page " + (print_previewWindow.StartPage+1) + " / " + expectedNumberOfPages;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            print_previewWindow.StartPage++;
           
            lbl_pageDisplayed.Text = "Page " + (print_previewWindow.StartPage+1) + " / " + expectedNumberOfPages;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            print_document.DocumentName = "TimeLord Timetable";
            printDialog1.Document = print_document;
            
            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                print_document.PrintController = new System.Drawing.Printing.StandardPrintController();
                print_document.Print();
                
            }

        }

        private void PrintDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentTT.SetAllPrintingAttributes(false); //deselect all from being printed. This works!
        }

        private void print_previewWindow_Click(object sender, EventArgs e)
        {

        }
    }
}
