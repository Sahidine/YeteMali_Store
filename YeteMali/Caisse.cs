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
    public partial class Caisse : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public Caisse()
        {
            InitializeComponent();
        }


        //------Liste des caisses----------
        public void listeCaisse()
        {
            dg_Caisse.DataSource = db.ListeCaisse();
        }
        private void Caisse_Load(object sender, EventArgs e)
        {
            listeCaisse();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("Etes-vous sur de vouloir ajouter?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {


                    if (db.AjoutCaisse(txtIdentifiant.Text, txtcaisse.Text, txtlocalite.Text) > 0)
                    {
                        MessageBox.Show("Enregistrement effectué avec succes");
                    }
                    listeCaisse();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
