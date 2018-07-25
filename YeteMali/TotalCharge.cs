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
    public partial class TotalCharge : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public TotalCharge()
        {
            InitializeComponent();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            lb_Exercice_charges.Visible = true;
          //  lbMontantCharge.Visible = true;
            //Total des bilans passifs
            DataTable rest = db.TotalCharge(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr1 = rest.Rows[0];
           // lbMontantCharge.Text = dr1["Total_MBrut_Charge"].ToString();
            lb_Exercice_charges.Text = dr1["total_Exercice_Charge"].ToString();
            charge_Exercice();
            charge_MBrut();
        }

        private void TotalCharge_Load(object sender, EventArgs e)
        {
            lb_Exercice_charges.Visible = false;
           // lbMontantCharge.Visible = false;
            
        }

         //DataSet, Object, Array, Collection-------------------------------------------------------------------
        private DataTable TotalChargeBrut()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("graphCharge_MBrut", conn))
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
        private DataTable Total_Exercice_Charge()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("graphe_Charge_Exercice", conn))
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
        //Graphe du montant des charges 
        public void charge_MBrut()
        {
            chartMontantBrut.DataSource = TotalChargeBrut();
            chartMontantBrut.Series["Series1"].Points.Clear();
            chartMontantBrut.Series["Series1"].XValueMember = "Caisse";
            chartMontantBrut.Series["Series1"].YValueMembers = "Charge";
        }
        //Graphe du montant des charges 
        public void charge_Exercice()
        {
            chartExercice_charge.DataSource = Total_Exercice_Charge();
            chartExercice_charge.Series["Series1"].Points.Clear();
            chartExercice_charge.Series["Series1"].XValueMember = "Caisse";
            chartExercice_charge.Series["Series1"].YValueMembers = "Exercice";
        }

        private void TotalCharge_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MenuBilan m = new MenuBilan();
            m.Show();
        }
    }
}
