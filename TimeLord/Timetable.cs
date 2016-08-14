using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TimeLord
{
    public class Timetable
    {

        public List<Day> Week;
        public List<YearGroup> Years;
        public Lesson[][][][] mainTT;  //[day][period][yeargroup][form]
        public Lesson[][][] staffTT; //[day][period][staff]
        public Lesson[][][] roomTT; //[day][period][room]
        public List<Subject> Subjects;
        public List<Room> Rooms;
        public List<Lesson> Tray;
        public List<Teacher> Staff;
        public int numberOfTotalPeriods;
        private bool finalised;
        public bool mainSelectedForPrinting;
        public bool homeworkSelectedForPrinting;
      
        /// <summary>
        /// Gets the number of visible forms
        /// </summary>
        /// <param name="year">The year group to find the number of visible forms for.</param>
        /// <returns>Number of Visible Forms (integer)</returns>
        public int numberOfVisibleForms(YearGroup year)
        {
            int number = 0;
            foreach(FormClass form in year.Forms)
            {
                if (form.visible) number++;
            }
            return number;
        }

        /// <summary>
        /// Sets all printing attributes to a certain state.
        /// </summary>
        /// <param name="state">The state to set it to.</param>
        public void SetAllPrintingAttributes(bool state)
        {
            mainSelectedForPrinting = state;
            homeworkSelectedForPrinting = state;

            foreach (YearGroup y in Years)
                y.selectedForPrint = state;
            foreach (Room r in Rooms)
                r.selectedForPrint = state;
            foreach (Subject s in Subjects)
                s.selectedForPrinting = state;
            foreach (Teacher t in Staff)
                t.selectedForPrint = state;
        }

        /// <summary>
        /// Get the free room codes for a specified point on the timetable.
        /// </summary>
        /// <param name="dayIndex">The dayIndex of where to check.</param>
        /// <param name="periodIndex">The periodIndex of where to check.</param>
        /// <returns></returns>
        public string FreeRoomCodes(byte dayIndex, byte periodIndex)
        {

            string str = "Free Rooms: ";
            bool roomFound = false;
            for (int s = 0; s < Rooms.Count; s++)
            {
                if (roomTT[dayIndex][periodIndex][s] == null) {
                    str += Rooms[s].RoomCode + ", ";
                    roomFound = true;
                }
            }
            str = str.Remove(str.Length - 2, 2);
            str = roomFound == true ? str : "Free Rooms: None";
            str += Environment.NewLine;
            return str;
        }
        /// <summary>
        ///Determine whether a clash for staff occurs.
        /// </summary>
        /// <param name="dayIndex">The dayIndex of the cell to check</param>
        /// <param name="periodIndex">The periodIndex of the cell to check</param>
        /// <param name="staffIndex">The staffIndex of the cell to check</param>
        /// <param name="errorMessage">This references a string, whcih can display the possible error that occurs.</param>
        /// <returns>Whether a clash has occured.</returns>
        public bool IsStaffClash(int dayIndex, int periodIndex, int staffIndex, out string errorMessage)
        {
            bool rtn = (staffTT[dayIndex][periodIndex][staffIndex] != null);
            if (rtn) errorMessage = "A staff clash has occured. " + FreeStaffCodes(Convert.ToByte(dayIndex), Convert.ToByte(periodIndex)) + Environment.NewLine;
            else errorMessage = "";
            return rtn;
        }
        /// <summary>
        /// Determines whether a room clash has occurred.
        /// </summary>
        /// <param name="dayIndex">The dayIndex of the time to test.</param>
        /// <param name="periodIndex">The periodindex of the time to test.</param>
        /// <param name="roomIndex">The roomIndex of the time to test.</param>
        /// <param name="errorMessage">The error message to output. (uses out)</param>
        /// <returns>errorOccurred (boolean)</returns>
        public bool IsRoomClash(int dayIndex, int periodIndex, int roomIndex, out string errorMessage)
        {
            bool rtn = (roomTT[dayIndex][periodIndex][roomIndex] != null);
            if (rtn) errorMessage = "A room clash has occured. " + FreeRoomCodes(Convert.ToByte(dayIndex), Convert.ToByte(periodIndex)) + Environment.NewLine;
            else errorMessage = "";
            return rtn;
        }
        /// <summary>
        /// Determiens whether a class clash has occurred.
        /// </summary>
        /// <param name="dayIndex">The dayindex of the time to test.</param>
        /// <param name="periodIndex">The periodIndex of the time to test.</param>
        /// <param name="yearIndex">The yearIndex of the time to test.</param>
        /// <param name="formIndex">The formIndex of the time to test.</param>
        /// <param name="errorMessage">The error message to output. (uses out)</param>
        /// <returns>Whether a class clash has occurred (boolean)</returns>
        public bool IsClassClash(int dayIndex, int periodIndex, int yearIndex, int formIndex, out string errorMessage)
        {
            bool rtn = (mainTT[dayIndex][periodIndex][yearIndex][formIndex] != null);
            if (rtn) errorMessage = "A class clash has occured. Please set the year and form to other values, and try again." + Environment.NewLine;
            else errorMessage = "";
            return rtn;
        }


        /// <summary>
        /// Finds the staff members that are free at a particular time.
        /// </summary>
        /// <param name="dayIndex">The DayIndex of the cell.</param>
        /// <param name="periodIndex">The PeriodIndex of the cell.</param>
        /// <returns>A string for display, indicating the free staff codes.</returns>
        public string FreeStaffCodes(byte dayIndex, byte periodIndex)
        {

            string str = "Free Staff: ";
            bool staffFound = false;
            for (int s = 0; s < Staff.Count; s++)
            {
                if (staffTT[dayIndex][periodIndex][s] == null) { 
                    str += Staff[s].TeacherAbbreviation + ", ";
                    staffFound = true;
                }
            }
            str = str.Remove(str.Length - 2, 2);
            return staffFound == true ? str : "Free Staff: None";
        }
        /// <summary>
        /// Allows one to move teachers from one location to another.
        /// </summary>
        /// <param name="LessonToMove">The lesson object to move</param>
        /// <param name="newTeacherIndex">The new teacher index to move to.</param>
        public void MoveTeacher(Lesson LessonToMove, byte newTeacherIndex)
        {
            
            if (LessonToMove.TeacherAbbreviation == Staff[newTeacherIndex].TeacherAbbreviation) return; //work is done.
            if (staffTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][newTeacherIndex] != null) return; //should throw an exception really.

            byte oldTeacherIndex = Convert.ToByte(GetIndexOfStaff(LessonToMove.TeacherAbbreviation));
            Lesson oldLessonToMove = LessonToMove.Clone();
            LessonToMove.TeacherAbbreviation = Staff[newTeacherIndex].TeacherAbbreviation;
            int i = 0;

            do {
                staffTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex + i][newTeacherIndex] = LessonToMove;
                i++;
            } while((LessonToMove.PeriodIndex + i < Week[LessonToMove.DayIndex].PeriodsInDay.Count) && (staffTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex + i][oldTeacherIndex] == oldLessonToMove));

            for (int j = 0; j < i; j++)
                staffTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex + j][oldTeacherIndex] = null;

        }
        /// <summary>
        /// Move a lesson from one year to another.
        /// </summary>
        /// <param name="LessonToMove">The lesson object to move.</param>
        /// <param name="newYearIndex">The new year index to move to.</param>
        public void MoveYears(Lesson LessonToMove, byte newYearIndex)
        {
            if (LessonToMove.YearIndex == newYearIndex) return; //already done
            if (mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][newYearIndex][0] != null) return; //should throw exception really.
            Lesson oldLessonToMove = LessonToMove.Clone();
            byte oldYearIndex = oldLessonToMove.YearIndex;
            LessonToMove.YearIndex = newYearIndex;

            if (Years[newYearIndex].Forms.Count > oldLessonToMove.FormIndex)
            {
                LessonToMove.FormIndex = oldLessonToMove.FormIndex;
            }
            else
            {
                LessonToMove.FormIndex = 0;
            }

            if (Years[newYearIndex].Forms.Count == 0) return;

           
                 mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][newYearIndex][LessonToMove.FormIndex] = LessonToMove;
           
                mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][oldYearIndex][oldLessonToMove.FormIndex] = null;
        }

        /// <summary>
        /// Move a lesson from one form to another.
        /// </summary>
        /// <param name="LessonToMove">The lesson object to move.</param>
        /// <param name="newFormIndex">The new form index to be moved to.</param>
        public void MoveForms(Lesson LessonToMove, byte newFormIndex)
        {
            if (LessonToMove.FormIndex == newFormIndex) return; //it is already done.
            if (newFormIndex >= Years[LessonToMove.YearIndex].Forms.Count) return; //if we have odd information, then reject it and return without moving anything.
            if (mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][LessonToMove.YearIndex][newFormIndex] != null) return;//we should throw an exception really.

            
            byte oldFormIndex = LessonToMove.FormIndex;
            LessonToMove.FormIndex = newFormIndex;

          
                mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][LessonToMove.YearIndex][newFormIndex] = LessonToMove;
                mainTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][LessonToMove.YearIndex][oldFormIndex] = null;

        }
    /// <summary>
    /// Move a room to a new room index
    /// </summary>
    /// <param name="LessonToMove">Lesson to move</param>
    /// <param name="newRoomIndex">new Room Index to move to (byte)</param>
        public void MoveRooms(Lesson LessonToMove, byte newRoomIndex)
        {
            if (LessonToMove.RoomCode == Rooms[newRoomIndex].RoomCode) return; //work is done already.
            if (roomTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][newRoomIndex] != null) return; //should throw exception really
            Lesson oldLessonToMove = LessonToMove.Clone();
            byte oldRoomIndex = Convert.ToByte(GetIndexOfRoom(LessonToMove.RoomCode));
            LessonToMove.RoomCode = Rooms[newRoomIndex].RoomCode;
                 roomTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][newRoomIndex] = LessonToMove;
                 roomTT[LessonToMove.DayIndex][LessonToMove.PeriodIndex][oldRoomIndex] = null;
          
        }
        /// <summary>
        /// Move a lesson to a new period.
        /// </summary>
        /// <param name="LessonToMove">The lesson to move.</param>
        /// <param name="newPeriodIndex">The new period index to move to.</param>
        /// <returns>Success</returns>
        public bool MovePeriods(Lesson LessonToMove, byte newPeriodIndex)
        {
           //updating for main tt also means we must update for staff, adn other tables too. This is important to remember.
           //we essentially go through the current value, and save to newperiodindex + i. repeat for the other tables.
            if (newPeriodIndex == LessonToMove.PeriodIndex) return true;
            
            //move main one to new period index
            //if oldPeriodIndex + i is the same lesson, move that to newPeriodIndex + i.
            //The same is true for staff
            //the same is true for rooms
            byte oldPeriodIndex = LessonToMove.PeriodIndex;
            int roomIndex = GetIndexOfRoom(LessonToMove.RoomCode);
            int staffIndex = GetIndexOfStaff(LessonToMove.TeacherAbbreviation);
            if (roomIndex < 0 | staffIndex < 0) return false;
            if (mainTT[LessonToMove.DayIndex][newPeriodIndex][LessonToMove.YearIndex][LessonToMove.FormIndex] != null) return false;
            if (staffTT[LessonToMove.DayIndex][newPeriodIndex][staffIndex] != null) return false;
            if (roomTT[LessonToMove.DayIndex][newPeriodIndex][roomIndex] != null) return false;
            LessonToMove.PeriodIndex = newPeriodIndex;
            int baseIndex = newPeriodIndex > oldPeriodIndex ? newPeriodIndex : oldPeriodIndex; //get the lowest of the two. This is to prevent an overflow.


            mainTT[LessonToMove.DayIndex][newPeriodIndex][LessonToMove.YearIndex][LessonToMove.FormIndex] = LessonToMove;
            mainTT[LessonToMove.DayIndex][oldPeriodIndex][LessonToMove.YearIndex][LessonToMove.FormIndex] = null;

           
            roomTT[LessonToMove.DayIndex][newPeriodIndex][roomIndex] = LessonToMove;
            roomTT[LessonToMove.DayIndex][oldPeriodIndex][roomIndex] = null; //clear the old memory pointer.
        
            staffTT[LessonToMove.DayIndex][newPeriodIndex][staffIndex] = LessonToMove;
            staffTT[LessonToMove.DayIndex][oldPeriodIndex][staffIndex] = null; //clear the old memory pointer.
          

            return true;

        }

        /// <summary>
        /// Load a file
        /// </summary>
        /// <param name="filename">The filename to read.</param>
        /// <returns>Read successfully.</returns>
        public bool Load(string filename)
        {
            bool error = false;
            FileStream fileStream = null;
            BinaryReader reader = null;
            try
            {
                fileStream = new FileStream(filename, FileMode.Open);
                reader = new BinaryReader(fileStream);

                if (reader.ReadString() != "TLF") throw new Exception("The file loaded isn't a TT file.");
                finalised = reader.ReadBoolean();
                int numberOfYears = reader.ReadInt32();

                for (int i = 0; i < numberOfYears; i++)
                {
                    YearGroup thisYear = new YearGroup(reader.ReadString());
                    int numberOfForms = reader.ReadInt32();

                    for (int j = 0; j < numberOfForms; j++)
                    {
                        FormClass thisForm = new FormClass(reader.ReadString());
                        thisYear.Forms.Add(thisForm);
                    }
                    this.Years.Add(thisYear);
                }


                int numberOfDays = reader.ReadInt32();
                byte identifier = 0;
                for (int i = 0; i < numberOfDays; i++)
                {
                    Day newDay = new Day(reader.ReadString());
                    int numberOfPeriods = reader.ReadInt32();

                    for (int j = 0; j < numberOfPeriods; j++)
                    {
                        newDay.AddPeriod(reader.ReadString(), reader.ReadString());
                        identifier++;
                    }
                    this.Week.Add(newDay);
                }

                int numberOfRooms = reader.ReadInt32();
                for (int i = 0; i < numberOfRooms; i++)
                {
                    Room newRoom = new Room(reader.ReadString());
                    Rooms.Add(newRoom);
                }

                int numberOfTeachers = reader.ReadInt32();
                for (int i = 0; i < numberOfTeachers; i++)
                {
                    Teacher newTeacher = new Teacher(reader.ReadString(), reader.ReadString());
                    Staff.Add(newTeacher);
                }


                int numberOfSubjects = reader.ReadInt32();
                for (int i = 0; i < numberOfSubjects; i++)
                {
                    Subject newSubject = new Subject(reader.ReadString(), reader.ReadString());
                    Subjects.Add(newSubject);
                }

              
                //start writing lessons
                if (finalised) Finalise(); //finalise the timetable, so that we won't crash the program.

                for (int dayptr = 0; dayptr < Week.Count; dayptr++)
                    for (int periodptr = 0; periodptr < Week[dayptr].PeriodsInDay.Count; periodptr++)
                        for (int yearptr = 0; yearptr < Years.Count; yearptr++)
                            for (int formptr = 0; formptr < Years[yearptr].Forms.Count; formptr++)
                            {
                                if (reader.ReadBoolean() == true)
                                {
                                    //lesson is present, so read it.
                                    Lesson newLesson = new Lesson(reader);
                                    int indexOfRoom = GetIndexOfRoom(newLesson.RoomCode);
                                    int indexOfStaff = GetIndexOfStaff(newLesson.TeacherAbbreviation);
                                    mainTT[dayptr][periodptr][yearptr][formptr] = newLesson;
                                    roomTT[dayptr][periodptr][indexOfRoom] = newLesson;
                                    staffTT[dayptr][periodptr][indexOfStaff] = newLesson;
                                }
                            }

               
                   





            }
            catch (Exception ex)
            {
                error = true;

            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
            return !error;
        }

        /// <summary>
        /// Save a timetable.
        /// </summary>
        /// <param name="filename">The filename to save to.</param>
        /// <returns>Successfully saved.</returns>
        public bool Save(string filename)
        {
            bool error = false;
            FileStream fileStream = null;
            BinaryWriter writer = null;
            try
            {
                fileStream = new FileStream(filename, FileMode.Create);
                writer = new BinaryWriter(fileStream);

                writer.Write("TLF");
                writer.Write(finalised);
                writer.Write(this.Years.Count);
                foreach (YearGroup year in Years)
                {
                    writer.Write(year.YearName);
                    writer.Write(year.Forms.Count);
                    foreach (FormClass form in year.Forms)
                    {
                        writer.Write(form.ToString()); //the form name is the only thing we are interested in...
                    }
                }

                writer.Write(this.Week.Count);
                foreach (Day weekDay in Week)
                {
                    writer.Write(weekDay.DayName);
                    writer.Write(weekDay.PeriodsInDay.Count);
                    foreach (Period p in weekDay.PeriodsInDay)
                    {
                        writer.Write(p.PeriodDisplay);
                        writer.Write(p.PeriodDescription);
                    }
                }
                
                writer.Write(Rooms.Count);
                foreach (Room room in Rooms)
                {
                    writer.Write(room.RoomCode);
                }
                
                writer.Write(Staff.Count);
                foreach (Teacher teach in Staff)
                {
                    writer.Write(teach.TeacherName);
                    writer.Write(teach.TeacherAbbreviation);
                }

                writer.Write(Subjects.Count);
                foreach (Subject sub in Subjects)
                {
                    writer.Write(sub.SubjectName);
                    writer.Write(sub.SubjectAbbreviation);
                }
                //start writing lessons
                if (finalised)
                {
                    for (int dayptr = 0; dayptr < Week.Count; dayptr++)
                        for (int periodptr = 0; periodptr < Week[dayptr].PeriodsInDay.Count; periodptr++)
                            for (int yearptr = 0; yearptr < Years.Count; yearptr++)
                                for (int formptr = 0; formptr < Years[yearptr].Forms.Count; formptr++)
                                {
                                    Lesson toSave = mainTT[dayptr][periodptr][yearptr][formptr];
                                    if (toSave != null)
                                    {
                                        writer.Write(true);
                                        toSave.WriteLessonToFile(writer);
                                    }
                                    else
                                    {
                                        writer.Write(false);
                                    }
                                }

                    
               
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                error = true;
                
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
            return !error;
        }
        /// <summary>
        /// Find the index of a staff member.
        /// </summary>
        /// <param name="staffCode">The staff code to find.</param>
        /// <returns>The index</returns>
        public int GetIndexOfStaff(string staffCode)
        {
            int j = -1;
            do
            {
                j++;
            } while (j < Staff.Count - 1 && Staff[j].TeacherAbbreviation != staffCode);
            if (j <= Staff.Count - 1) return j;
            return -1;
        }

        /// <summary>
        /// Find the room index of a room.
        /// </summary>
        /// <param name="RoomCode">The room code</param>
        /// <returns>The index</returns>
        public int GetIndexOfRoom(string RoomCode)
        {
            int j = -1;
            do
            {
                j++;
            } while (j < Rooms.Count - 1 && Rooms[j].RoomCode != RoomCode);
            if (j <= Rooms.Count - 1) return j;
            return -1;
        }

        /// <summary>
        /// Delete a lesson
        /// </summary>
        /// <param name="lessonToDelete">The lesson to delete</param>
        /// <returns>success in deleting</returns>
        public bool DeleteLesson(Lesson lessonToDelete)
        {
            if (lessonToDelete == null) return false;
            byte StaffIndex = Convert.ToByte(GetIndexOfStaff(lessonToDelete.TeacherAbbreviation));
            byte RoomIndex = Convert.ToByte(GetIndexOfRoom(lessonToDelete.RoomCode));
            byte DayIndex = lessonToDelete.DayIndex;
            byte PeriodIndex = lessonToDelete.PeriodIndex;
            byte YearIndex = lessonToDelete.YearIndex;
            byte FormIndex = lessonToDelete.FormIndex;

            int Ptr = 0;
            do
            {
                mainTT[DayIndex][PeriodIndex + Ptr][YearIndex][FormIndex] = null;
                Ptr++;
            } while ((Ptr < Week[DayIndex].PeriodsInDay.Count - 1) && (mainTT[DayIndex][PeriodIndex + Ptr][YearIndex][FormIndex] == lessonToDelete));
            
            Ptr = 0;
             do
            {
                roomTT[DayIndex][PeriodIndex + Ptr][RoomIndex] = null;
                Ptr++;

            } while ((Ptr < Week[DayIndex].PeriodsInDay.Count - 1) && (roomTT[DayIndex][PeriodIndex + Ptr][RoomIndex] == lessonToDelete));
            
            Ptr = 0;
             do
            {
                staffTT[DayIndex][PeriodIndex + Ptr][StaffIndex] = null;
                Ptr++;
            } while ((Ptr < Week[DayIndex].PeriodsInDay.Count - 1) && (staffTT[DayIndex][PeriodIndex + Ptr][StaffIndex] == lessonToDelete));
            
            //the code above deletes the consecutive lessons (which typically are automatically added).

            return true;
        }

        /// <summary>
        /// Get the index of a subject
        /// </summary>
        /// <param name="Subjectcode"></param>
        /// <returns></returns>
        public int GetIndexOfSubject(string Subjectcode)
        {
            
            int j = -1;

            do
            {
                j++;
            } while (j < Subjects.Count -1 && Subjects[j].SubjectAbbreviation != Subjectcode);
            if (j <= Subjects.Count - 1) return j;
            return -1;
           
        
        
        }
       
        public void MoveToTray(Lesson lessonToMove)
        {
            DeleteLesson(lessonToMove);
            lessonToMove.DayIndex = 0;
            lessonToMove.PeriodIndex = 0;
            lessonToMove.YearIndex = 0;
            lessonToMove.FormIndex = 0;
            Tray.Add(lessonToMove);
        }
        public void MoveFromTrayToMainTT(int DayIndex, int PeriodIndex, int YearIndex, int FormIndex, int TrayIndex)
        {
            if (mainTT[DayIndex][PeriodIndex][YearIndex][FormIndex] != null) return;
            Lesson lessonToMove = Tray[TrayIndex];

            
            Tray.Remove(lessonToMove);
            AddLesson(Convert.ToByte(DayIndex), Convert.ToByte(PeriodIndex), 1, lessonToMove.SubjectAbbreviation, lessonToMove.TeacherAbbreviation, lessonToMove.RoomCode, Convert.ToByte(YearIndex), Convert.ToByte(FormIndex), lessonToMove.homeworkAmount, lessonToMove.locked, lessonToMove.invisible);

        }
        public Timetable()
        {

            Week = new List<Day>();
            Years = new List<YearGroup>();
            Staff = new List<Teacher>();
            mainTT = null;
            Subjects = new List<Subject>();
            Rooms = new List<Room>();
            finalised = false;
            mainSelectedForPrinting = false;
            homeworkSelectedForPrinting = false;
            numberOfTotalPeriods = 0; //this means that one cannot click on anything until the timetable has been finalised.
            Tray = new List<Lesson>();
        }
        /// <summary>
        /// Finalise the timetable. This will prevent the structure from being changed...
        /// </summary>
        public void Finalise()
        {
            mainTT = new Lesson[Week.Count][][][];
            
            staffTT = new Lesson[Week.Count][][];
            roomTT = new Lesson[Week.Count][][];
            for (int ptr = 0; ptr < Week.Count; ptr++)
            {
                numberOfTotalPeriods += Week[ptr].PeriodsInDay.Count();
                mainTT[ptr] = new Lesson[Week[ptr].PeriodsInDay.Count][][];
                staffTT[ptr] = new Lesson[Week[ptr].PeriodsInDay.Count][];
                roomTT[ptr] = new Lesson[Week[ptr].PeriodsInDay.Count][];

                for (int pptr = 0; pptr < Week[ptr].PeriodsInDay.Count; pptr++)
                {
                    this.mainTT[ptr][pptr] = new Lesson[Years.Count][];

                    for (int yptr = 0; yptr < Years.Count; yptr++)
                        this.mainTT[ptr][pptr][yptr] = new Lesson[Years[yptr].Forms.Count];

                    staffTT[ptr][pptr] = new Lesson[Staff.Count];
                    roomTT[ptr][pptr] = new Lesson[Rooms.Count];
                }
            }






            this.finalised = true;
            
        }

        /// <summary>
        /// Determines whether the timetable has been finalised.
        /// </summary>
        /// <returns>Boolean: Has the timetable been finalised?</returns>
        public bool IsFinalised()
        {
            return finalised;
        }

        /// <summary>
        /// Adds a lesson to the timetable
        /// </summary>
        /// <param name="dayIndex">The index of the day needed.</param>
        /// <param name="periodIndex">The index of the period needed.</param>
        /// <param name="periodLength">The number of periods the lesson covers.</param>
        /// <param name="subjectCode">The subject code associated with the type of subject.</param>
        /// <param name="teacherCode">The teacher code associated with the teacher name.</param>
        /// <param name="roomName">The room name.</param>
        /// <param name="yearIndex">The index of the year group, for the lesson.</param>
        /// <param name="formIndex">The index of the form in the year group specified.</param>
        /// <param name="homeworkAmount">The amount of homework that is expected to be set in the lesson.</param>
        /// <returns>Whether the lesson addition has been successful.</returns>
        public bool AddLesson(byte dayIndex, byte periodIndex, byte periodLength, string subjectCode, string teacherCode, string roomName, byte yearIndex, byte formIndex,byte homeworkAmount, bool locked, bool invisible)
        {
            int staffIndex = GetIndexOfStaff(teacherCode);
            int roomIndex = GetIndexOfRoom(roomName);

            if (roomIndex < 0) return false;
            if (staffIndex < 0) return false;
            if (!finalised) return false;
            if ((subjectCode == "") || (teacherCode == "") || (roomName == "")) return false;
            if (dayIndex >= this.Week.Count) return false;
            if (periodIndex + periodLength - 1 >= this.Week[dayIndex].PeriodsInDay.Count) return false;
            //check to see whether any lessons currently exist in that space. This will therefore check for clashes.
            if (roomTT[dayIndex][periodIndex][roomIndex] != null) return false;
            if (staffTT[dayIndex][periodIndex][staffIndex] != null) return false;
            if (mainTT[dayIndex][periodIndex][yearIndex][formIndex] != null) return false;
            
            
            
            
            Lesson newLesson = new Lesson(teacherCode, subjectCode, roomName, dayIndex, periodIndex,homeworkAmount,locked,invisible,formIndex,yearIndex);
            





            for (int periodptr = 0; periodptr < periodLength; periodptr++)
            {
                Lesson l = newLesson.Clone();
                l.PeriodIndex = Convert.ToByte(periodIndex + periodptr);
                
                mainTT[dayIndex][periodIndex+periodptr][yearIndex][formIndex] = l;
                staffTT[dayIndex][periodIndex+periodptr][staffIndex] = l;
                roomTT[dayIndex][periodIndex+periodptr][roomIndex] = l;
            }
            return true;
        }


    }
}
