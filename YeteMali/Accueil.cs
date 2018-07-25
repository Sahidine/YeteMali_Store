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
    public partial class Accueil : Form
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            this.btConnexion.BackColor = ColorTranslator.FromHtml("#fdbc02");
        }

        private void btConnexion_Click(object sender, EventArgs e)
        {
            Connexion c = new Connexion();
            c.Show();
        }

        private void btConnexion_MouseLeave(object sender, EventArgs e)
        {
            this.btConnexion.BackColor = ColorTranslator.FromHtml("#80ff80");
            
        }

        private void btpropos_MouseHover(object sender, EventArgs e)
        {
            this.btpropos.BackColor = ColorTranslator.FromHtml("#fdbc02");

        }

        private void btpropos_MouseLeave(object sender, EventArgs e)
        {
            this.btpropos.BackColor = ColorTranslator.FromHtml("#80ff80");

        }

        private void btCaisse_MouseHover(object sender, EventArgs e)
        {
            this.btCaisse.BackColor = ColorTranslator.FromHtml("#fdbc02");

        }

        private void btCaisse_MouseLeave(object sender, EventArgs e)
        {
            this.btCaisse.BackColor = ColorTranslator.FromHtml("#80ff80");

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#fdbc02");

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = ColorTranslator.FromHtml("#804000");

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
