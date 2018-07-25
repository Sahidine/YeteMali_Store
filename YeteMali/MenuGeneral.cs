using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YeteMali
{
    public partial class MenuGeneral : Form
    {
        public MenuGeneral()
        {
            InitializeComponent();
        }

        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MenuGeneral_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int a = rand.Next(0,255);
            int b = rand.Next(0, 255);
            int c = rand.Next(0, 255);
            int d = rand.Next(0, 255);
            lbMenu.ForeColor = Color.FromArgb(a, b, c, d);


        }

        private void btConnexion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSuiviQuotidien_Click(object sender, EventArgs e)
        {
            ConnexionSuivi c = new ConnexionSuivi();
            c.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConnexionBilan b = new ConnexionBilan();
            b.Show();
        }
    }
}
