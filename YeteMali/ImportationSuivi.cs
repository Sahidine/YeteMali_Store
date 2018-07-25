using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YeteMali
{
    public partial class ImportationSuivi : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ImportationSuivi()
        {
            InitializeComponent();
            chargeCombo();
        }
        DataSet ds = new DataSet();
        //Charge liste Caisse
        public void chargeCombo()
        {
            cbCaisse.DataSource = db.ListeCaisse();
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

        private void btEnregistrer_Click(object sender, EventArgs e)
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
              //  decimal encaisse = 0;
               // decimal reserve = 0;
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    /*foreach (DataGridViewRow data in dgvImporation.Rows)
                    {
                        if (data.Cells[0].Value.ToString() == "" || data.Cells[1].Value.ToString() == "")
                        {
                            encaisse = 0; reserve = 0;
                        }
                        else 
                        {
                            encaisse = decimal.Parse(rw["Encaisse"].ToString()); reserve = decimal.Parse(rw["Reserve"].ToString());

                        }

                     }*/
                     decimal  encaisse = decimal.Parse(rw["Encaisse"].ToString()); decimal reserve = decimal.Parse(rw["Reserve"].ToString());
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
                    double PAR;
                    if (Montant4 == 0)
                    {
                        PAR = 0;
                    }
                    else
                    {
                        PAR = ((double)Montant4 / (double)VOLUME) * 100;     
                    }
                        
                  //  MessageBox.Show(PAR+"");
                    db.AjoutSuiviCaisses(encaisse, reserve,liquidite, Nbre1, Montant1, /*Nbre2, Montant2, Nbre3, Montant3,Nbre4, capital, interet,*/  Nbre5, Montant4, Montant5, Nbre6, VOLUME, MontantG, Nbre7, Montant6, Nbre8, Montant7,
                        Nbre9, Montant8, Nbre10, Montant9, FRAIS1, Nbre11, Montant10, Nbre12, FRAIS2, Nbre13, FRAIS3, cbCaisse.SelectedValue.ToString(), DateTime.Parse(cbdate.Text),PAR);
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

        private void ImportationSuivi_Load(object sender, EventArgs e)
        {
            cbCaisse.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
       
    }
}
