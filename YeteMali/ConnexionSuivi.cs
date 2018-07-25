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
    public partial class ConnexionSuivi : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ConnexionSuivi()
        {
            InitializeComponent();
            txtEncryptage.Enabled = false;
        }
        const int maxTentative = 3;
        int tentative = 0;
        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ConnexionSuivi_Load(object sender, EventArgs e)
           
        {
            timer1.Start();
            timer1.Enabled = true;
            txtEncryptage.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int a = rand.Next(0, 255);
            int b = rand.Next(0, 255);
            int c = rand.Next(0, 255);
            int d = rand.Next(0, 255);
            lbServiceExploitation.ForeColor = Color.FromArgb(a, b, c, d);
        }

        private void btConnexion_Click(object sender, EventArgs e)
        {
            if (txtPseudo.Text == "" || txtMdp.Text == "")
            {
                MessageBox.Show("Veuillez saisir un pseudo et un mot de passe");
            }
            else
            {
                // DataTable MyData = db.NomUserEtMotDePasse(txtLogin.Text,  db.md5Cryptage(txtMdp.Text));
                DataTable MyData = db.Connexion(txtPseudo.Text, txtMdp.Text);
                if (MyData.Rows.Count > 0)
                {
                    tentative = 0;
                    this.Hide();
                    MenuGeneral f1 = new MenuGeneral();
                    f1.Close();

                    this.Hide();
                    MenuSuiviQuotidien f2 = new MenuSuiviQuotidien();
                    f2.Show();
                }
                else
                {
                    if (tentative >= maxTentative)
                    {
                        MessageBox.Show("Trop de tentative sur l'application, espace de piraterie!! Bye Bye " + tentative + " !");
                        Application.Exit();
                    }
                    tentative += 1;
                    MessageBox.Show("Login ou mot de passe incorrecte !");
                }
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
