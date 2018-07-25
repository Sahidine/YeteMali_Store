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
    public partial class PapierTerrain : Form
    {
        Service_PapierTerrain db = new Service_PapierTerrain();
        Service_SuiviCaisse data = new Service_SuiviCaisse();
        public PapierTerrain()
        {
            InitializeComponent();
            chargeCombo();
            ListePapierTerrain();
        }
        DataSet ds = new DataSet();
        //Liste de la caisse
        public void chargeCombo()
        {
            cbCaisse.DataSource = data.ListeCaisse();
        }
        //Vider les champs
        public void ResetForm()
        {
            cbCaisse.Text="";
            txtNomPrenom.Clear();
            TxtMontantDebours.Clear();
            TxtTaux.Clear();
            TxtFrais.Text = "";
            TxtFraisNotaire.Clear();
            TxtGarantie.Clear();
            TxtReference.Clear();
        }

        private void PapierTerrain_Load(object sender, EventArgs e)
        {
            cbCaisse.Text = "";
            this.reportViewer1.RefreshReport();

          
        }
        //Liste des papiers de terrain
        public void ListePapierTerrain()
        {
            dgListePapier.DataSource = db.ListePapierTerrain();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbCaisse.Text == "")
                {
                    MessageBox.Show("Veuillez choisir la caisse", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtNomPrenom.Text == "")
                {
                    MessageBox.Show("Veuillez saisir le nom et prénom du membre", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (TxtMontantDebours.Text == "")
                {
                    MessageBox.Show("Veuillez saisir le montant déboursé", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cbDate.Text == "")
                {
                    MessageBox.Show("Veuillez choisir la date", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (TxtGarantie.Text == "")
                {
                    MessageBox.Show("Veuillez saisir la garantie du papier", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (TxtReference.Text == "")
                {
                    MessageBox.Show("Veuillez saisir la référence du papier", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult res = MessageBox.Show("Etes-vous sur de vouloir ajouter?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {


                    if (db.AjoutPapierTerrain(cbCaisse.SelectedValue.ToString(),txtNomPrenom.Text,decimal.Parse(TxtMontantDebours.Text.ToString()),DateTime.Parse(cbDate.Text),float.Parse(TxtTaux.Text.ToString()),
                        decimal.Parse(TxtFrais.Text.ToString()),decimal.Parse(TxtFraisNotaire.Text.ToString()),TxtGarantie.Text,TxtReference.Text) > 0)
                    {
                        MessageBox.Show("Enregistrement effectué avec succes");
                    }
                    ResetForm();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dgListePapier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow attribution in dgListePapier.Rows)
            {
               attribution.Cells["cai"].Value = lb_caisse.Text;
                
            }
           
        }
    }
}
