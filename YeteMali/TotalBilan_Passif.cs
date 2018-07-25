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
    public partial class TotalBilan_Passif : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();

        public TotalBilan_Passif()
        {
            InitializeComponent();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            //Total des bilans passifs
            DataTable rest = db.ParaTotalBilanPassif(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr1 = rest.Rows[0];
            lbMontantPassif.Text = String.Format("{0:0,000}",dr1["Total_MBrut_Passif"]);
            lb_Exercice_Passif.Text = String.Format("{0:0,000}",dr1["total_Exercice_Passif"]);
            MontantBrut_Passif();
            Exercice_Passif();
        }
        //DataSet, Object, Array, Collection-------------------------------------------------------------------
        private DataTable Montantbrut()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("grapheMontantBrut_passif", conn))
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
        private DataTable Exercice()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("graphe_Exercice_Passif", conn))
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
        //Graphe du montant net 
        public void MontantBrut_Passif()
        {
            chartMontantBrut.DataSource = Montantbrut();
            chartMontantBrut.Series["Series1"].Points.Clear();
            chartMontantBrut.Series["Series1"].XValueMember = "Caisse";
            chartMontantBrut.Series["Series1"].YValueMembers = "montantbrut";
        }
        //Graphe du montant net 
        public void Exercice_Passif()
        {
            chartExercice_Passif.DataSource = Exercice();
            chartExercice_Passif.Series["Series1"].Points.Clear();
            chartExercice_Passif.Series["Series1"].XValueMember = "Caisse";
            chartExercice_Passif.Series["Series1"].YValueMembers = "Exercice";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListeBilan m = new ListeBilan();
            m.Show();
        }

        private void TotalBilan_Passif_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MenuBilan m = new MenuBilan();
            m.Show();
        }
    }
}
