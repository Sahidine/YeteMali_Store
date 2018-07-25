using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.IO;



namespace YeteMali
{
    public partial class SuiviQuotidien : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public SuiviQuotidien()
        {
            InitializeComponent();
            chargeCombo();
            chargeComboCaisse();
        }

        DataSet ds = new DataSet();
        //Charge combo caisse

        public void chargeComboCaisse()
        {
            CbCaisse_importation.DataSource = db.ListeCaisse();
        }
        //Parametre pour la date suivi Caisse
        public void DateSuiviCaisse()
        {
            dgListesuivi.DataSource = db.DateSuivi(DateTime.Parse(cbdate1.Text), DateTime.Parse(cbdate2.Text), cbcaisse.Text);
        }
        public void chargeCombo()
        {
            cbcaisse.DataSource = db.ListeCaisse();
        }
       //------------------------------------------------Debut classement des différentes caisses-------------------------------------
        //--------------------La methode pour le rang-----------------------------
        public void classement()
        {
            int i = 1; 
            dgRang.DataSource=db.RangCaisse(DateTime.Parse(d_du1.Text), DateTime.Parse(d_au2.Text));
            foreach (DataGridViewRow dg in dgRang.Rows)
            {
                dg.Cells["rang2"].Value = (i++) + (i==2?" er " :  " ème " ); 
            }

 
        }
        //-------------La methode classement PAR-----------------
        public void classementPar()
        {
            Dictionary<string, float> dc;
            dc = new Dictionary<string, float>();


            foreach (DataGridViewRow item in dgRang.Rows)
            {
                dc.Add(item.Cells["cais"].Value + "", float.Parse(item.Cells["p"].Value.ToString()));
            }

            var items = from pair in dc
                        orderby pair.Value ascending
                        select pair;
            int k = 1;
            foreach (KeyValuePair<string, float> comp in items)
            {
                foreach (DataGridViewRow r in dgRang.Rows)
                {
                    if (r.Cells["cais"].Value.ToString().Equals(comp.Key))
                    {
                        r.Cells["rang1"].Value = (k++) + (k == 2 ? " er " : " ème ");

                    }
                }
            }
        }
        //-------------La methode classement du Volume d'epargne-----------------
        public void classementVolumeEpargne()
        {
            Dictionary<string, decimal> dc;
            dc = new Dictionary<string, decimal>();


            foreach (DataGridViewRow item in dgRang.Rows)
            {
                dc.Add(item.Cells["cais"].Value + "", decimal.Parse(item.Cells["epargne"].Value.ToString()));
            }

            var items = from pair in dc
                        orderby pair.Value descending
                        select pair;
            int k = 1;
            foreach (KeyValuePair<string, decimal> comp in items)
            {
                foreach (DataGridViewRow r in dgRang.Rows)
                {
                    if (r.Cells["cais"].Value.ToString().Equals(comp.Key))
                    {
                        r.Cells["rang3"].Value = (k++) + (k == 2 ? " er " : " ème "); ;

                    }
                }
            }
        }
        //-------------------La methode pour le resultat des caisses--------------------------
        public void classementResultat()
        {
            Dictionary<string, decimal> dc;
            dc = new Dictionary<string, decimal>();


            foreach (DataGridViewRow item in dgRang.Rows)
            {
                dc.Add(item.Cells["cais"].Value + "", decimal.Parse(item.Cells["resultat"].Value.ToString()));
            }

            var items = from pair in dc
                        orderby pair.Value descending
                        select pair;
            int j = 1;
            foreach (KeyValuePair<string, decimal> comp in items)
            {
                foreach (DataGridViewRow r in dgRang.Rows)
                {
                    if (r.Cells["cais"].Value.ToString().Equals(comp.Key))
                    {
                        r.Cells["rangResulta"].Value = (j++) + (j == 2 ? " er " : " ème "); ;

                    }
                }
            }
        }
        //-------------------La methode pour les crédits encours--------------------------
        public void classementCréditEncours()
        {
            Dictionary<string, decimal> dc;
            dc = new Dictionary<string, decimal>();


            foreach (DataGridViewRow item in dgRang.Rows)
            {
                dc.Add(item.Cells["cais"].Value + "", decimal.Parse(item.Cells["credit"].Value.ToString()));
            }

            var items = from pair in dc
                        orderby pair.Value descending
                        select pair;
            int u = 1;
            foreach (KeyValuePair<string, decimal> comp in items)
            {
                foreach (DataGridViewRow r in dgRang.Rows)
                {
                    if (r.Cells["cais"].Value.ToString().Equals(comp.Key))
                    {
                        r.Cells["rang4"].Value = (u++) + (u == 2 ? " er " : " ème "); ;

                    }
                }
            }
        }
        //----------------------------------Fin des methodes de classement---------------------------
        


        private void SuiviQuotidien_Load(object sender, EventArgs e)
        {

            cbcaisse.Text = "";
            CbCaisse_importation.Text = "";
        }

        private void SuiviQuotidien_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MenuSuiviQuotidien m = new MenuSuiviQuotidien();
            m.Show();
        }
        //DataSet, Object, Array, Collection-------------------------------------------------------------------
        private DataTable GetData()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ParamDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);


                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }
        //Montant pret rembourse-------------------------------------------------------------------
        private DataTable MontantPretRembourse()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("MontantPretRembourser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant général-------------------------------------------------------------------
        private DataTable MontantGénéral()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Resultat", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant pret solde-------------------------------------------------------------------
        private DataTable PretSolde()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PretSolde", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant Remboursement effectue-------------------------------------------------------------------
        private DataTable RemboursEff()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RemboursementEffectue", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant Remboursement attendu-------------------------------------------------------------------
        private DataTable RemboursAttendu()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RemboursementAttendu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }
        //Montant le credit en retard-------------------------------------------------------------------
        private DataTable CreditRetard()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CrediRetard", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant le volume epargne-------------------------------------------------------------------
        private DataTable VolumeEpargne()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VolumeEpargne", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }
        //Montant l'encour de credit-------------------------------------------------------------------
        private DataTable CreditEnCour()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EncourCredit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        //Montant l'encour de credit-------------------------------------------------------------------
        private DataTable AssuranceFrais()
        {
            DataTable dtchart = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Frais", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Du", DateTime.Parse(cbdate1.Text));
                    cmd.Parameters.AddWithValue("@Au", DateTime.Parse(cbdate2.Text));
                    cmd.Parameters.AddWithValue("@nomcaisse", cbcaisse.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtchart.Load(reader);
                }
            }

            return dtchart;
        }

        // Partie graphique-------------------------------------------------------------------

        //Graphe du montant remboursement effectue
        public void ChartRemboursEff()
        {
            chartRemboursementEffectue.DataSource = RemboursEff();
            chartRemboursementEffectue.Series["Series1"].Points.Clear();
            chartRemboursementEffectue.Series["Series1"].XValueMember = "Caisse";
            chartRemboursementEffectue.Series["Series1"].YValueMembers = "Rembours_effectue";
        }

        //Graphe du montant général
        public void ChartResultat()
        {
            chartResult.DataSource = MontantGénéral();
            chartResult.Series["Series1"].Points.Clear();
            chartResult.Series["Series1"].XValueMember = "Caisse";
            chartResult.Series["Series1"].YValueMembers = "MontantGeneral";
        }

        //Graphe du montant pret rembourser
        public void ChartPretRembourse()
        {
            ChartRebourse.DataSource = MontantPretRembourse();
            ChartRebourse.Series["Series1"].Points.Clear();
            ChartRebourse.Series["Series1"].XValueMember = "Caisse";
            ChartRebourse.Series["Series1"].YValueMembers = "P_Rembourse";
        }
        //Graphique pour la liquidite
        public void Chart()
        {
            chartCaisse.DataSource = GetData();
            chartCaisse.Series["Series1"].Points.Clear();
            chartCaisse.Series["Series1"].XValueMember = "Caisse";
            chartCaisse.Series["Series1"].YValueMembers = "Liquidite";
        }
        //Graphique pour le pret solde
        public void ChartPretSol()
        {
            /*chartPretSolde.DataSource = PretSolde();
            chartPretSolde.Series["Series1"].Points.Clear();
            chartPretSolde.Series["Series1"].XValueMember = "Caisse";
            chartPretSolde.Series["Series1"].YValueMembers = "P_Solde";*/
        }
        //Graphique pour le remboursement attendu
        public void ChartRemboursementAttendu()
        {
            chartRemAttendu.DataSource = RemboursAttendu();
            chartRemAttendu.Series["Series1"].Points.Clear();
            chartRemAttendu.Series["Series1"].XValueMember = "Caisse";
            chartRemAttendu.Series["Series1"].YValueMembers = "Rembours_attendu";
        }

        //Graphique pour le credit en retard
        public void ChartCredi_Retard()
        {
            chartCreditRetar.DataSource = CreditRetard();
            chartCreditRetar.Series["Series1"].Points.Clear();
            chartCreditRetar.Series["Series1"].XValueMember = "Caisse";
            chartCreditRetar.Series["Series1"].YValueMembers = "credi_Retard";
        }

        //Graphique pour le volume epargne
        public void ChartVolum_epargne()
        {
            chartVolumeEpargn.DataSource = VolumeEpargne();
            chartVolumeEpargn.Series["Series1"].Points.Clear();
            chartVolumeEpargn.Series["Series1"].XValueMember = "Caisse";
            chartVolumeEpargn.Series["Series1"].YValueMembers = "volume_epargne";
        }

        //Graphique pour l'encour credit
        public void EncourCredit()
        {
            chartEncourCredi.DataSource = CreditEnCour();
            chartEncourCredi.Series["Series1"].Points.Clear();
            chartEncourCredi.Series["Series1"].XValueMember = "Caisse";
            chartEncourCredi.Series["Series1"].YValueMembers = "Encour_credit";
        }

        //Graphique pour le frais de l'assurance
        public void FraisAssurance()
        {
            chartAssuranc.DataSource = AssuranceFrais();
            chartAssuranc.Series["Series1"].Points.Clear();
            chartAssuranc.Series["Series1"].XValueMember = "Caisse";
            chartAssuranc.Series["Series1"].YValueMembers = "Frais";
        }
       
        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            //dgListesuivi.Columns(5).DefaultCellStyle.Format = "#, ###";
            DateSuiviCaisse();
            Chart(); FraisAssurance(); EncourCredit(); ChartVolum_epargne();
            ChartCredi_Retard(); ChartResultat(); //ChartRemboursEff();
           /*ChartRemboursementAttendu(); ChartPretSol(); */ ChartPretRembourse();

        }

        private void chartEncourCredi_Click(object sender, EventArgs e)
        {

        }

        private void chartRemboursementEffectue_Click(object sender, EventArgs e)
        {

        }

        private void chartAssuranc_Click(object sender, EventArgs e)
        {

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            classement();
            classementVolumeEpargne();    
             classementCréditEncours(); 
            classementPar();
            classementResultat();

        }

        //--------La methode pour l'observation de PAR------------------------
        public void observation_PAR()
        {
            dg_observation.DataSource = db.AppreciationPar(DateTime.Parse(d_observation1.Text), DateTime.Parse(d_observation2.Text));
            foreach (DataGridViewRow observation in dg_observation.Rows)
            {
                if (decimal.Parse(observation.Cells["apre_par"].Value.ToString()) < 5)
                {
                    observation.Cells["observation"].Value = "Très Bien";
                    observation.Cells["observation"].Style.BackColor = Color.LightGreen;
                }
                if (decimal.Parse(observation.Cells["apre_par"].Value.ToString()) == 5)
                {
                    observation.Cells["observation"].Value = "Assez bien";
                    observation.Cells["observation"].Style.BackColor = Color.YellowGreen;
                }
               
                if (decimal.Parse(observation.Cells["apre_par"].Value.ToString()) > 5 && decimal.Parse(observation.Cells["apre_par"].Value.ToString()) <= 10)
                {
                    observation.Cells["observation"].Value = "passable";
                    observation.Cells["observation"].Style.BackColor = Color.Yellow;
                }
                if (decimal.Parse(observation.Cells["apre_par"].Value.ToString()) > 10 )
                    {
                        observation.Cells["observation"].Value = "Mediocre";
                        observation.Cells["observation"].Style.BackColor = Color.Red;
                    }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            observation_PAR();
           
        }

        private void btImporter_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();  //create openfileDialog Object
                openFileDialog1.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";//open file format define Excel Files(.xls)|*.xls| Excel Files(.xlsx)|*.xlsx| 
                openFileDialog1.FilterIndex = 3;

                openFileDialog1.Multiselect = false;        //not allow multiline selection at the file selection level
                openFileDialog1.Title = "Open Text File-R13";   //define the name of openfileDialog
                openFileDialog1.InitialDirectory = @"Desktop"; //define the initial directory



                if (openFileDialog1.ShowDialog() == DialogResult.OK)        //executing when file open
                {
                    string pathName = openFileDialog1.FileName;
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    DataTable tbContainer = new DataTable();
                    string strConn = string.Empty;
                    string sheetName = fileName;

                    FileInfo file = new FileInfo(pathName);
                    if (!file.Exists) { throw new Exception("Error, file doesn't exists!"); }
                    string extension = file.Extension;
                    switch (extension)
                    {
                        case ".xls":
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                        case ".xlsx":
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                            break;
                        default:
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                    }

                    //Get the name of the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(strConn))
                    {

                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            con.Close();

                            ds = new DataSet();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.CommandType = CommandType.Text;
                            OleDbDataAdapter oda = new OleDbDataAdapter();
                            oda.SelectCommand = cmd;
                            oda.Fill(ds);
                            dgvImporation.DataSource = ds.Tables[0];
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           try
            {
                DialogResult res = MessageBox.Show("Etes-vous sur de vouloir importer la liste des Suivis?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    if (ds != null && ds.Tables.Count == 0)
                    {
                        MessageBox.Show("L'importation de la liste effectuée avec succès", "Action Reussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                //La boucle Foreach

                foreach (DataRow rw in ds.Tables[0].Rows)
                {

                    decimal encaisse = decimal.Parse(rw["Encaisse"].ToString()); decimal reserve = decimal.Parse(rw["Reserve"].ToString());
                    decimal Nbre1 = decimal.Parse(rw["Nbre1"].ToString()); decimal Montant1 = decimal.Parse(rw["Montant1"].ToString());
                    /*decimal Nbre2 = decimal.Parse(rw["Nbre2"].ToString()); decimal Montant2 = decimal.Parse(rw["Montant2"].ToString());
                    decimal Nbre3 = decimal.Parse(rw["Nbre3"].ToString()); decimal Montant3 = decimal.Parse(rw["Montant3"].ToString());
                    decimal Nbre4 = decimal.Parse(rw["Nbre4"].ToString()); decimal capital = decimal.Parse(rw["CAPITAL"].ToString());
                    decimal interet = decimal.Parse(rw["INTERET"].ToString());*/
                    decimal Nbre5 = decimal.Parse(rw["Nbre5"].ToString()); decimal Montant4 = decimal.Parse(rw["Montant4"].ToString());
                    decimal Montant5 = decimal.Parse(rw["Montant5"].ToString());
                    decimal Nbre6 = decimal.Parse(rw["Nbre6"].ToString()); decimal VOLUME = decimal.Parse(rw["VOLUME"].ToString());
                    decimal MontantG = decimal.Parse(rw["MontantG"].ToString()); decimal Nbre7 = decimal.Parse(rw["Nbre7"].ToString());
                    decimal Montant6 = decimal.Parse(rw["Montant6"].ToString()); decimal Nbre8 = decimal.Parse(rw["Nbre8"].ToString());
                    decimal Montant7 = decimal.Parse(rw["Montant7"].ToString()); decimal Nbre9 = decimal.Parse(rw["Nbre9"].ToString());
                    decimal Montant8 = decimal.Parse(rw["Montant8"].ToString()); decimal Nbre10 = decimal.Parse(rw["Nbre10"].ToString());
                    decimal Montant9 = decimal.Parse(rw["Montant9"].ToString()); decimal FRAIS1 = decimal.Parse(rw["FRAIS1"].ToString());
                    decimal Nbre11 = decimal.Parse(rw["Nbre11"].ToString()); decimal Montant10 = decimal.Parse(rw["Montant10"].ToString());
                    decimal Nbre12 = decimal.Parse(rw["Nbre12"].ToString()); decimal FRAIS2 = decimal.Parse(rw["FRAIS2"].ToString());
                    decimal Nbre13 = decimal.Parse(rw["Nbre13"].ToString()); decimal FRAIS3 = decimal.Parse(rw["FRAIS3"].ToString());
                    decimal liquidite = encaisse + reserve;
                    double PAR = ((double)Montant4 / (double)VOLUME) * 100;
                    string caisse = rw["id"].ToString(); DateTime dateSuiviCaisse = DateTime.Parse(rw["DateSuivi"].ToString());

                    //  MessageBox.Show(PAR+"");
                    db.AjoutSuiviCaisses(encaisse, reserve, liquidite, Nbre1, Montant1,/* Nbre2, Montant2, Nbre3, Montant3, Nbre4, capital, interet,*/ Nbre5, Montant4, Montant5, Nbre6, VOLUME, MontantG, Nbre7, Montant6, Nbre8, Montant7,
                        Nbre9, Montant8, Nbre10, Montant9, FRAIS1, Nbre11, Montant10, Nbre12, FRAIS2, Nbre13, FRAIS3, caisse, dateSuiviCaisse, PAR);
                }
                this.Close();
                ListeSuivi l = new ListeSuivi();
                l.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
