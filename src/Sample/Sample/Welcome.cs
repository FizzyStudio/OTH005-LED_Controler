using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class Welcome : Form
    {
        int count = 0;
        int opacity = 100;

        public Welcome()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            count++;
            opacity--;
            this.Opacity = opacity / 50.0;
            if (count == 100)
            {
                this.Close();
            }
        }

        private void FormWelcome_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.timer1.Interval = 30;
        }

        private void FormWelcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }
    }
}
