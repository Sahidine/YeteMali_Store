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
    public partial class Connexion : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public Connexion()
        {
            InitializeComponent();
        }
        const int maxTentative = 3;
        int tentative = 0;
        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Connexion_Load(object sender, EventArgs e)
        {
            txtEncryptage.Visible = false;
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
                    Accueil f1 = new Accueil();
                    f1.Close();

                    this.Hide();
                    MenuGeneral f2 = new MenuGeneral();
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

        private void btConnexion_MouseHover(object sender, EventArgs e)
        {
            this.btConnexion.BackColor = ColorTranslator.FromHtml("#fdbc02");

        }

        private void btConnexion_MouseLeave(object sender, EventArgs e)
        {
            this.btConnexion.BackColor = ColorTranslator.FromHtml("#80ff80");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
