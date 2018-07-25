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
    public partial class StatistiqueSuivi : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public StatistiqueSuivi()
        {
            InitializeComponent();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            DataTable restric = db.ResultatLiquidite(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr = restric.Rows[0];
            lblLiquidite.Text = String.Format("{0:0,000}", dr["total"]);
            lblPret_Rebourse.Text = String.Format("{0:0,000}",dr["pret_Rebourse"]);
             lbl_PretSolde.Text = String.Format("{0:0,000}",dr["FRAIS2"]);
           lbl_Rembourse.Text = dr["PAR"] + " % ";
           lbl_credit_retard.Text = String.Format("{0:0,000}",dr["montant_retard"]);
            lbl_Resultat.Text = String.Format("{0:0,000}",dr["resultat"]);
            lbl_volume_epargne.Text = String.Format("{0:0,000}",dr["volume_epargne"]);
            lbl_encoure_credit.Text = String.Format("{0:0,000}",dr["encours_credit"]);
            lbl_assurance.Text = String.Format("{0:0,000}", dr["assurance"]);
            lbl_Remboursement_attendu.Text = String.Format("{0:0,000}", dr["Montant10"]);
          
           
         /*ResultatLiquidite();
         ResultatP_Rembours();*/
        }

        private void StatistiqueSuivi_FormClosed(object sender, FormClosedEventArgs e)
        {
            MenuSuiviQuotidien m = new MenuSuiviQuotidien();
            m.Show();
        }

        private void StatistiqueSuivi_Load(object sender, EventArgs e)
        {

        }
          
        

        /*Graphe du montant général
        public void ResultatLiquidite()
        {
            chartLiquidite.DataSource = db.ResultatLiquidite(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            chartLiquidite.Series["Series1"].Points.Clear();
            chartLiquidite.Series["Series1"].YValueMembers = "total";
        }

        //Graphe du montant général
        public void ResultatP_Rembours()
        {
            chartPretRembourse.DataSource = db.ResultatLiquidite(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            chartPretRembourse.Series["Series1"].Points.Clear();
            chartPretRembourse.Series["Series1"].YValueMembers = "pret_Rebourse";
        }*/

    }
}
