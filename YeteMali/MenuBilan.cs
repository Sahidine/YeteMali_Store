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
    public partial class MenuBilan : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public MenuBilan()
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ImportationBilanActif i = new ImportationBilanActif();
            i.Show();
         
        }

        
        private void MenuBilan_Load(object sender, EventArgs e)
        {
            //--------------Nombre de bilans actifs
            DataTable nombreBilan = db.NombreBilanActif();
            DataRow dr = nombreBilan.Rows[0];
            lb_bilanActif.Text = dr["nombre"].ToString();

            //-----------------Nombre de bilans passifs
            DataTable nombreBilanPassif = db.NombreBilanPassif();
            DataRow dr1 = nombreBilanPassif.Rows[0];
            lb_bilan_passif.Text = dr1["nombre"].ToString();

            //-----------------Nombre de charges
            DataTable nombreCharge = db.nombreCharge();
            DataRow dr2 = nombreCharge.Rows[0];
            lb_charge.Text = dr2["nombre"].ToString();

            //-----------------Nombre de produits
            DataTable nombreProduit = db.nombreProduit();
            DataRow dr3 = nombreProduit.Rows[0];
            lb_charge.Text = dr3["nombre"].ToString();


            listeBilanActif();
           
        }

        //----------------Methode pour la liste des bilans actifs----------------
        public void listeBilanActif()
        {
            //------------------Liste des bilans Actif
            dgListesuivi.DataSource = db.ListeBilanActif();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            ImportationBilanPassif i = new ImportationBilanPassif();
            i.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Importation_Charge i = new Importation_Charge();
            i.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionBilanActif g = new GestionBilanActif();
            g.Show();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            GestionBilanPassif l = new GestionBilanPassif();
            l.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {

            this.Hide();
            TotalCharge c = new TotalCharge();
            c.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionEtatProduit c = new GestionEtatProduit();
            c.Show();
        }
    }
}
