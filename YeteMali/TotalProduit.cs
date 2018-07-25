using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YeteMali
{
    public partial class TotalProduit : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public TotalProduit()
        {
            InitializeComponent();
        }

        private void TotalProduit_Load(object sender, EventArgs e)
        {
            lbMontantProduit.Visible = false;
            lb_Exercice_produit.Visible = false;
        }
        //DataSet, Object, Array, Collection-------------------------------------------------------------------
        private DataTable Total_Montant_Produit()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("grapheMontant_Produit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(d_du.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(d_au.Text));

                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //DataSet, Object, Array, Collection-------------------------------------------------------------------
        private DataTable Total_Exercice_Produit()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("grapheExercice_Produit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(d_du.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(d_au.Text));

                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }
        //---------------------------------------------------------
        //Graphe du montant des des produits 
        public void Montant_Produit()
        {
            chartProduit_MBrut.DataSource = Total_Montant_Produit();
            chartProduit_MBrut.Series["Series1"].Points.Clear();
            chartProduit_MBrut.Series["Series1"].XValueMember = "Caisse";
            chartProduit_MBrut.Series["Series1"].YValueMembers = "produit";
        }
        //Graphe du montant des charges 
        public void Exercice_Produit()
        {
            chart_Exercice_Produit.DataSource = Total_Exercice_Produit();
            chart_Exercice_Produit.Series["Series1"].Points.Clear();
            chart_Exercice_Produit.Series["Series1"].XValueMember = "Caisse";
            chart_Exercice_Produit.Series["Series1"].YValueMembers = "Exercice";
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            lbMontantProduit.Visible = true;
            lb_Exercice_produit.Visible = true;
            //Total des bilans passifs
            DataTable rest = db.TotalProduit(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr1 = rest.Rows[0];
            lbMontantProduit.Text = dr1["Total_MBrut_Produit"].ToString();
            lb_Exercice_produit.Text = dr1["total_Exercice_Produit"].ToString();
            Montant_Produit();
            Exercice_Produit();
        }

        private void TotalProduit_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MenuBilan m = new MenuBilan();
            m.Show();
        }
    }
}
