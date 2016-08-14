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
   

    public partial class subjectManager : Form
    {

        public Timetable currentTT = null;

        public subjectManager()
        {
            InitializeComponent();
        }

        private void subjectManager_Load(object sender, EventArgs e)
        {
            if (currentTT == null)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();

            }

            foreach (Subject sub in currentTT.Subjects)
            {
                ListViewItem lvi = new ListViewItem(sub.SubjectAbbreviation);
                lvi.SubItems.Add(sub.SubjectName);
                list_subjects.Items.Add(lvi);

            }



        }

        private void btn_addSubject_Click(object sender, EventArgs e)
        {
            Subject newSub = new Subject("Subject Name", "XX");
            currentTT.Subjects.Add(newSub);

            ListViewItem lvi = new ListViewItem(newSub.SubjectAbbreviation);
            lvi.SubItems.Add(newSub.SubjectName);
            list_subjects.Items.Add(lvi);
            

            
        }

        private void btn_removeSubject_Click(object sender, EventArgs e)
        {
            if (list_subjects.Items.Count == 0) return;
            if (list_subjects.SelectedItems.Count == 0) return;
            
            int index = list_subjects.SelectedIndices[0];
            if (index >= list_subjects.Items.Count) return;
            list_subjects.Items.RemoveAt(index);
            currentTT.Staff.RemoveAt(index);
            int nextIndex = list_subjects.Items.Count - 1;

            if (nextIndex < 0)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }

            list_subjects.Items[nextIndex].Selected = true;


        }

        private void list_subjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_subjects.Items.Count == 0) return;
            if (list_subjects.SelectedItems.Count == 0) return;

            int index = list_subjects.SelectedIndices[0];
            tb_code.Text = currentTT.Subjects[index].SubjectAbbreviation;
            tb_name.Text = currentTT.Subjects[index].SubjectName;

        }

        private void tb_code_TextChanged(object sender, EventArgs e)
        {
            if (list_subjects.SelectedIndices.Count == 0)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }
            int index = list_subjects.SelectedIndices[0];
            currentTT.Subjects[index].SubjectAbbreviation = tb_code.Text;
            list_subjects.Items[index].SubItems[0].Text = tb_code.Text;


        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            if (list_subjects.SelectedIndices.Count == 0)
            {
                tb_code.Text = "";
                tb_name.Text = "";
                return;
            }

            int index = list_subjects.SelectedIndices[0];
            currentTT.Subjects[index].SubjectName = tb_name.Text;
            list_subjects.Items[index].SubItems[1].Text = tb_name.Text;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
