using System;
using System.Windows.Forms;

namespace Client
{
    public partial class LaunchScreen : Form
    {
        public LaunchScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            RecorderForm frm = new RecorderForm();
            frm.Show();
            this.Hide();
        }
    }
}