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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void RefreshScreenData()
        {
            btn_background.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_Background;
            btn_normal.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_Normal;
            btn_invisible.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_Invisible;
            btn_locked.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_Locked;
            btn_invAndLckd.BackColor = TimeLord.Properties.Settings.Default.Screen_Color_LockedAndInvisible;
            
            nup_cellWidth.Value = TimeLord.Properties.Settings.Default.Screen_CellWidth;
            nup_cellHeight.Value = TimeLord.Properties.Settings.Default.Screen_CellHeight;
            nud_marginLeft.Value = TimeLord.Properties.Settings.Default.Screen_MarginLeft;
            nud_marginTop.Value = TimeLord.Properties.Settings.Default.Screen_MarginTop;
            
        
        }
        private void RefreshPrintingData()
        {
            nud_Homework_Height.Value = Properties.Settings.Default.Print_Homework_Height;
            nud_Homework_Width.Value = Properties.Settings.Default.Print_Homework_Width;
            nud_Homework_Margin.Value = Properties.Settings.Default.Print_Homework_MarginLeft;

            nud_Room_Height.Value = Properties.Settings.Default.Print_Rooms_Height;
            nud_Room_Width.Value = Properties.Settings.Default.Print_Rooms_Width;
            nud_Room_Margin.Value = Properties.Settings.Default.Print_Rooms_MarginLeft;

            nud_School_Height.Value = Properties.Settings.Default.Print_School_Height;
            nud_School_Width.Value = Properties.Settings.Default.Print_School_Width;
            nud_School_Margin.Value = Properties.Settings.Default.Print_School_MarginLeft;

            nud_Staff_Height.Value = Properties.Settings.Default.Print_Staff_Height;
            nud_Staff_Width.Value = Properties.Settings.Default.Print_Staff_Width;
            nud_Staff_Margin.Value = Properties.Settings.Default.Print_Staff_MarginLeft;

            nud_Subject_Height.Value = Properties.Settings.Default.Print_Subject_Height;
            nud_Subject_Width.Value = Properties.Settings.Default.Print_Subject_Width;
            nud_Subject_Margin.Value = Properties.Settings.Default.Print_Subject_MarginLeft;

            nud_Year_Height.Value = Properties.Settings.Default.Print_Year_Height;
            nud_Year_Width.Value = Properties.Settings.Default.Print_Year_Width;
            nud_Year_Margin.Value = Properties.Settings.Default.Print_Year_MarginLeft;




        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshScreenData();
            RefreshPrintingData();
        }

        private void btn_background_Click(object sender, EventArgs e)
        {
            cp_main.Color = TimeLord.Properties.Settings.Default.Screen_Color_Background;

            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                btn_background.BackColor = cp_main.Color;
                TimeLord.Properties.Settings.Default.Screen_Color_Background = cp_main.Color;
                cp_main.Reset();
                
            }
        }

        private void btn_normal_Click(object sender, EventArgs e)
        {
            cp_main.Color = TimeLord.Properties.Settings.Default.Screen_Color_Normal;
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_normal.BackColor = cp_main.Color;
                TimeLord.Properties.Settings.Default.Screen_Color_Normal = cp_main.Color;
                cp_main.Reset();
            }
        }

        private void btn_invisible_Click(object sender, EventArgs e)
        {

            cp_main.Color = TimeLord.Properties.Settings.Default.Screen_Color_Invisible;
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_invisible.BackColor = cp_main.Color;
                TimeLord.Properties.Settings.Default.Screen_Color_Invisible = cp_main.Color;
                cp_main.Reset();
            }
        }

        private void btn_locked_Click(object sender, EventArgs e)
        {
            cp_main.Color = TimeLord.Properties.Settings.Default.Screen_Color_Locked;
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_locked.BackColor = cp_main.Color;
                TimeLord.Properties.Settings.Default.Screen_Color_Locked = cp_main.Color;
                cp_main.Reset();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cp_main.Color = TimeLord.Properties.Settings.Default.Screen_Color_LockedAndInvisible;
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_invAndLckd.BackColor = cp_main.Color;
                TimeLord.Properties.Settings.Default.Screen_Color_LockedAndInvisible = cp_main.Color;
                cp_main.Reset();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void nup_cellWidth_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Screen_CellWidth = Convert.ToInt32(nup_cellWidth.Value);
        }

        private void nup_cellHeight_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Screen_CellHeight = Convert.ToInt32(nup_cellHeight.Value);
        }

        private void nud_marginTop_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Screen_MarginTop = Convert.ToInt32(nud_marginTop.Value);
        }

        private void nud_marginLeft_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Screen_MarginLeft = Convert.ToInt32(nud_marginLeft.Value);
        }


        private void nud_Year_Width_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Year_Width = Convert.ToInt32(nud_Year_Width.Value);

        }

        private void nud_Year_Height_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Year_Height = Convert.ToInt32(nud_Year_Height.Value);
        }

        private void nud_Year_Margin_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Year_MarginLeft = Convert.ToInt32(nud_Year_Margin.Value);
        }

        private void nud_Staff_Width_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Staff_Width = Convert.ToInt32(nud_Staff_Width.Value);

        }

        private void nud_Staff_Height_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Staff_Height = Convert.ToInt32(nud_Staff_Height.Value);
        }

        private void nud_Staff_Margin_ValueChanged(object sender, EventArgs e)
        {
            TimeLord.Properties.Settings.Default.Print_Staff_MarginLeft = Convert.ToInt32(nud_Staff_Margin.Value);
        }

        private void nud_Homework_Width_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Homework_Width = Convert.ToInt32(nud_Homework_Width.Value);
        }

        private void nud_Homework_Height_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Homework_Height = Convert.ToInt32(nud_Homework_Height.Value);
        }

        private void nud_Homework_Margin_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Homework_MarginLeft = Convert.ToInt32(nud_Homework_Margin.Value);
        }

        private void nud_Subject_Width_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Subject_Width = Convert.ToInt32(nud_Subject_Width.Value);
        }

        private void nud_Subject_Height_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Subject_Height = Convert.ToInt32(nud_Subject_Height.Value);
        }

        private void nud_Subject_Margin_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Subject_MarginLeft = Convert.ToInt32(nud_Subject_Margin.Value);
        }

        private void nud_Room_Width_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Rooms_Width = Convert.ToInt32(nud_Room_Width.Value);
        }

        private void nud_Room_Height_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Rooms_Height = Convert.ToInt32(nud_Room_Height.Value);
        }

        private void nud_Room_Margin_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_Rooms_MarginLeft = Convert.ToInt32(nud_Room_Margin.Value);

        }

        private void nud_School_Width_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_School_Width = Convert.ToInt32(nud_School_Width.Value);
        }

        private void nud_School_Height_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_School_Height = Convert.ToInt32(nud_School_Height.Value);
        }

        private void nud_School_Margin_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Print_School_MarginLeft = Convert.ToInt32(nud_School_Margin.Value);
        }
    }
}
