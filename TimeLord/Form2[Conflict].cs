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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btn_background_Click(object sender, EventArgs e)
        {
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_background.BackColor = cp_main.Color;
                Properties.Settings.Default.G
            }
        }

        private void btn_normal_Click(object sender, EventArgs e)
        {
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_normal.BackColor = cp_main.Color;
            }
        }

        private void btn_invisible_Click(object sender, EventArgs e)
        {
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_invisible.BackColor = cp_main.Color;
            }
        }

        private void btn_locked_Click(object sender, EventArgs e)
        {
            if (cp_main.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn_locked.BackColor = cp_main.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
