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
    public partial class ImportationBilanActif : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ImportationBilanActif()
        {
            InitializeComponent();
            chargeCombo();
        }

        public void chargeCombo()
        {
            cbCaisse.DataSource = db.ListeCaisse();
        }
        DataSet ds = new DataSet();
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

        private void button5_Click(object sender, EventArgs e)
        {

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

                foreach (DataRow rw in ds.Tables[0].Rows)
                {

                  
                    decimal Encaisse_Montant_Brut = decimal.Parse(rw["Encaisse_Montant_Brut"].ToString()); decimal Encaisse_Amortis = decimal.Parse(rw["Encaisse_Amortis"].ToString());
                    decimal Encaisse_MontantNet = decimal.Parse(rw["Encaisse_MontantNet"].ToString()); decimal Encaisse_Exercice = decimal.Parse(rw["Encaisse_Exercice"].ToString());
                    decimal Etabli_financier_Brut = decimal.Parse(rw["Etabli_financier_Brut"].ToString()); decimal Etabli_financier_Amortis = decimal.Parse(rw["Etabli_financier_Amortis"].ToString());
                    decimal Etabli_financier_MontantNet = decimal.Parse(rw["Etabli_financier_MontantNet"].ToString()); decimal Etabli_financier_Exercice = decimal.Parse(rw["Etabli_financier_Exercice"].ToString());
                    decimal Fond_garantie_MBrut = decimal.Parse(rw["Fond_garantie_MBrut"].ToString()); decimal Fond_garantie_Amortis = decimal.Parse(rw["Fond_garantie_Amortis"].ToString());
                    decimal Fond_garantie_MontantNet = decimal.Parse(rw["Fond_garantie_MontantNet"].ToString()); decimal Fond_garantie_Exercice = decimal.Parse(rw["Fond_garantie_Exercice"].ToString());
                    //---------------La somme des opérations avec les institutions
                    decimal OpInstituFinanceMBrut = Encaisse_Montant_Brut + Etabli_financier_Brut + Fond_garantie_MBrut; 
                    decimal Op_Institu_Finance_Amortis = Encaisse_Amortis + Etabli_financier_Amortis + Fond_garantie_Amortis ;
                    decimal Op_Institu_Finance_Montant = Etabli_financier_MontantNet + Encaisse_MontantNet + Fond_garantie_MontantNet;
                    decimal OpInstituFinanceExercice = Encaisse_Exercice + Etabli_financier_Exercice + Fond_garantie_Exercice;
                    
                   
                    decimal Credit_Sain_MBrut = decimal.Parse(rw["Credit_Sain_MBrut"].ToString()); decimal Credit_Sain_Amortis = decimal.Parse(rw["Credit_Sain_Amortis"].ToString());
                    decimal Credit_Sain_MontantNet = decimal.Parse(rw["Credit_Sain_MontantNet"].ToString()); decimal Credit_Sain_Exercice = decimal.Parse(rw["Credit_Sain_Exercice"].ToString());
                    decimal Creance_Rattache_MBrut = decimal.Parse(rw["Creance_Rattache_MBrut"].ToString()); decimal Creance_Rattache_Amortis = decimal.Parse(rw["Creance_Rattache_Amortis"].ToString());
                    decimal Creance_Rattache_MontantNet = decimal.Parse(rw["Creance_Rattache_MontantNet"].ToString()); decimal Creance_Rattache_Exercice = decimal.Parse(rw["Creance_Rattache_Exercice"].ToString());
                    decimal Créance_En_Souffrance_MBrut = decimal.Parse(rw["Créance_En_Souffrance_MBrut"].ToString()); decimal Créance_En_Souffrance_Amortis = decimal.Parse(rw["Créance_En_Souffrance_Amortis"].ToString());
                    decimal Créance_En_Souffrance_MontantNet = decimal.Parse(rw["Créance_En_Souffrance_MontantNet"].ToString()); decimal Créance_En_Souffrance_Exercice = decimal.Parse(rw["Créance_En_Souffrance_Exercice"].ToString());
                   //--------------------La somme des opération avec les membres
                    decimal Op_Avec_Membre_MBrut = Credit_Sain_MBrut + Creance_Rattache_MBrut + Créance_En_Souffrance_MBrut;
                    decimal Op_Avec_Membre_Amortis = Credit_Sain_Amortis + Creance_Rattache_Amortis + Créance_En_Souffrance_Amortis;
                    decimal Op_Avec_Membre_MontantNet = Credit_Sain_MontantNet + Creance_Rattache_MontantNet + Créance_En_Souffrance_MontantNet;
                    decimal Op_Avec_Membre_Exercice = Credit_Sain_Exercice + Creance_Rattache_Exercice + Créance_En_Souffrance_Exercice;
                    //------------------------------------------------------------------------------------Fin de la somme des opérations avec les membres ------------------
                    
                    decimal Stock_Montant_Brut = decimal.Parse(rw["Stock_Montant_Brut"].ToString()); decimal Stock_Montant_Amortis = decimal.Parse(rw["Stock_Montant_Amortis"].ToString());
                    decimal Stock_Montant_MontantNet = decimal.Parse(rw["Stock_Montant_MontantNet"].ToString()); decimal Stock_Montant_Exercice = decimal.Parse(rw["Stock_Montant_Exercice"].ToString());
                    decimal Debiteur_Diver_MBrut = decimal.Parse(rw["Debiteur_Diver_MBrut"].ToString()); decimal Debiteur_Diver_Amortis = decimal.Parse(rw["Debiteur_Diver_Amortis"].ToString());
                    decimal Debiteur_Diver_MontantNet = decimal.Parse(rw["Debiteur_Diver_MontantNet"].ToString()); decimal Debiteur_Diver_Exercice = decimal.Parse(rw["Debiteur_Diver_Exercice"].ToString());
                    decimal Compte_Regulier_Actif_MBrut = decimal.Parse(rw["Debiteur_Diver_MontantNet"].ToString()); decimal Compte_Regulier_Actif_Amortis = decimal.Parse(rw["Debiteur_Diver_MontantNet"].ToString());
                    decimal Compte_Regulier_Actif_MontantNet = decimal.Parse(rw["Debiteur_Diver_MontantNet"].ToString()); decimal Compte_Regulier_Actif_Exercice = decimal.Parse(rw["Debiteur_Diver_MontantNet"].ToString());
                    decimal Compte_Attente_Actif_MBrut = decimal.Parse(rw["Compte_Attente_Actif_MBrut"].ToString()); decimal Compte_Attente_Actif_Amortis = decimal.Parse(rw["Compte_Attente_Actif_Amortis"].ToString());
                    decimal Compte_Attente_Actif_MontantNet = decimal.Parse(rw["Compte_Attente_Actif_MontantNet"].ToString()); decimal Compte_Attente_Actif_Exercice = decimal.Parse(rw["Compte_Attente_Actif_Exercice"].ToString());
                    
                    decimal Depot_Cautionnement_MBrut = decimal.Parse(rw["Depot_Cautionnement_MBrut"].ToString()); decimal Depot_Cautionnement_Amort = decimal.Parse(rw["Depot_Cautionnement_Amort"].ToString());
                    decimal Depot_Cautionnement_MontantNet = decimal.Parse(rw["Depot_Cautionnement_MontantNet"].ToString()); decimal Depot_Cautionnement_Exercice = decimal.Parse(rw["Depot_Cautionnement_Exercice"].ToString());

                    decimal Autre_MBrut = decimal.Parse(rw["Autre_MBrut"].ToString()); decimal Autre_Amortis = decimal.Parse(rw["Autre_Amortis"].ToString());
                    decimal Autre_MontantNet = decimal.Parse(rw["Autre_MontantNet"].ToString()); decimal Autre_Exercice = decimal.Parse(rw["Autre_Exercice"].ToString());
                    decimal Titre_Participation_MontantBrut = decimal.Parse(rw["Titre_Participation_MontantBrut"].ToString()); decimal Titre_Participation_Amortis = decimal.Parse(rw["Titre_Participation_Amortis"].ToString());
                    decimal Titre_Participation_MontantNet = decimal.Parse(rw["Titre_Participation_MontantNet"].ToString()); decimal Titre_Participation_Exercice = decimal.Parse(rw["Titre_Participation_Exercice"].ToString());
                    decimal Compte_Liaison_MBrut = decimal.Parse(rw["Compte_Liaison_MBrut"].ToString()); decimal Compte_Liaison_Amortis = decimal.Parse(rw["Compte_Liaison_Amortis"].ToString());
                    decimal Compte_Liaison_MontantNet = decimal.Parse(rw["Compte_Liaison_MontantNet"].ToString()); decimal Compte_Liaison_Exercice = decimal.Parse(rw["Compte_Liaison_Exercice"].ToString());
                    //-------------------------------La somme des opération diverses----------------------------------------------
                    decimal Op_Diverse_MBrut = Stock_Montant_Brut + Debiteur_Diver_MBrut + Compte_Regulier_Actif_MBrut + Compte_Attente_Actif_MBrut + Depot_Cautionnement_MBrut + Autre_MBrut + Titre_Participation_MontantBrut + Compte_Liaison_MBrut;
                    decimal Op_Diverse_Amortis = Stock_Montant_Amortis + Debiteur_Diver_Amortis + Compte_Regulier_Actif_Amortis + Compte_Attente_Actif_Amortis + Depot_Cautionnement_Amort + Autre_Amortis + Titre_Participation_Amortis + Compte_Liaison_Amortis;
                    decimal Op_Diverse_MontantNet = Stock_Montant_MontantNet + Debiteur_Diver_MontantNet + Compte_Regulier_Actif_MontantNet + Compte_Attente_Actif_MontantNet + Depot_Cautionnement_MontantNet + Autre_MontantNet + Titre_Participation_MontantNet + Compte_Liaison_MontantNet;
                    decimal Op_Diverse_Exercice = Stock_Montant_Exercice + Debiteur_Diver_Exercice + Compte_Regulier_Actif_Exercice + Compte_Attente_Actif_Exercice + Depot_Cautionnement_Exercice + Autre_Exercice + Titre_Participation_Exercice + Compte_Liaison_Exercice;
                    //------------------------------------------------------------------Fin de la sommes des opérations diverses------------------------------------------

                    decimal Immobilisation_Corporelle_MBrut = decimal.Parse(rw["Immobilisation_Corporelle_MBrut"].ToString()); decimal Immobilisation_Corporelle_Amortis = decimal.Parse(rw["Immobilisation_Corporelle_Amortis"].ToString());
                    decimal Immobilisation_Corporelle_MontantNet = decimal.Parse(rw["Immobilisation_Corporelle_MontantNet"].ToString()); decimal Immobilisation_Corporelle_Exercice = decimal.Parse(rw["Immobilisation_Corporelle_Exercice"].ToString());
                    decimal Immobilisation_Incorporelle_MBrut = decimal.Parse(rw["Immobilisation_Incorporelle_MBrut"].ToString()); decimal Immobilisation_Incorporelle_Amortis = decimal.Parse(rw["Immobilisation_Incorporelle_Amortis"].ToString());
                    decimal Immobilisation_Incorporelle_MontantNet = decimal.Parse(rw["Immobilisation_Incorporelle_MontantNet"].ToString()); decimal Immobilisation_Incorporelle_Exercice = decimal.Parse(rw["Immobilisation_Incorporelle_Exercice"].ToString());
                    decimal Immobilisation_Financiere_MBrut = decimal.Parse(rw["Immobilisation_Financiere_MBrut"].ToString()); decimal Immobilisation_Financiere_Amortis = decimal.Parse(rw["Immobilisation_Financiere_Amortis"].ToString());
                    decimal Immobilisation_Financiere_MontantNet = decimal.Parse(rw["Immobilisation_Financiere_MontantNet"].ToString()); decimal Immobilisation_Financiere_Exercice = decimal.Parse(rw["Immobilisation_Financiere_Exercice"].ToString());
                    //------------------------------------------------------------------Debut de la sommes des immobilisations------------------------------------------                    
                    decimal Immobilisation_MBrut =  Immobilisation_Corporelle_MBrut + Immobilisation_Incorporelle_MBrut + Immobilisation_Financiere_MBrut;
                    decimal Immobilisation_Amortis =  Immobilisation_Corporelle_Amortis + Immobilisation_Incorporelle_Amortis + Immobilisation_Financiere_Amortis;
                    decimal Immobilisation_MontantNet =  Immobilisation_Corporelle_MontantNet + Immobilisation_Incorporelle_MontantNet + Immobilisation_Financiere_MontantNet;
                    decimal Immobilisation_Exercice =  Immobilisation_Corporelle_Exercice + Immobilisation_Incorporelle_Exercice + Immobilisation_Financiere_Exercice;
                    //------------------------------------------------------------------Fin de la sommes des immobilisations ------------------------------------------

                    decimal Total_MontantBrut = OpInstituFinanceMBrut + Op_Avec_Membre_MBrut + Op_Diverse_MBrut + Immobilisation_MBrut;
                    decimal Total_Amortissement = Op_Institu_Finance_Amortis + Op_Avec_Membre_Amortis + Op_Diverse_Amortis + Immobilisation_Amortis;
                    decimal Total_MontantNet = Op_Institu_Finance_Montant + Op_Avec_Membre_MontantNet + Op_Diverse_MontantNet + Immobilisation_MontantNet;
                    decimal Total_Exercice = OpInstituFinanceExercice + Op_Avec_Membre_Exercice + Op_Diverse_Exercice + Immobilisation_Exercice;

                    db.AjoutBilanActif(cbCaisse.SelectedValue.ToString(), DateTime.Parse(cbdate.Text), OpInstituFinanceMBrut, Op_Institu_Finance_Amortis, Op_Institu_Finance_Montant, OpInstituFinanceExercice,
                   Encaisse_Montant_Brut, Encaisse_Amortis, Encaisse_MontantNet, Encaisse_Exercice, Etabli_financier_Brut, Etabli_financier_Amortis,
                    Etabli_financier_MontantNet, Etabli_financier_Exercice, Fond_garantie_MBrut, Fond_garantie_Amortis, Fond_garantie_MontantNet, Fond_garantie_Exercice,
                    Op_Avec_Membre_MBrut, Op_Avec_Membre_Amortis, Op_Avec_Membre_MontantNet, Op_Avec_Membre_Exercice, Credit_Sain_MBrut, Credit_Sain_Amortis,
                     Credit_Sain_MontantNet, Credit_Sain_Exercice, Creance_Rattache_MBrut, Creance_Rattache_Amortis, Creance_Rattache_MontantNet, Creance_Rattache_Exercice,
                    Créance_En_Souffrance_MBrut, Créance_En_Souffrance_Amortis, Créance_En_Souffrance_MontantNet, Créance_En_Souffrance_Exercice, Op_Diverse_MBrut, Op_Diverse_Amortis,
                    Op_Diverse_MontantNet, Op_Diverse_Exercice, Stock_Montant_Brut, Stock_Montant_Amortis, Stock_Montant_MontantNet, Stock_Montant_Exercice, Debiteur_Diver_MBrut,
                    Debiteur_Diver_Amortis, Debiteur_Diver_MontantNet, Debiteur_Diver_Exercice, Compte_Regulier_Actif_MBrut, Compte_Regulier_Actif_Amortis, Compte_Regulier_Actif_MontantNet,
                    Compte_Regulier_Actif_Exercice, Compte_Attente_Actif_MBrut, Compte_Attente_Actif_Amortis, Compte_Attente_Actif_MontantNet, Compte_Attente_Actif_Exercice,
                     Depot_Cautionnement_MBrut,Depot_Cautionnement_Amort,Depot_Cautionnement_MontantNet,Depot_Cautionnement_Exercice,
                    Autre_MBrut,Autre_Amortis,Autre_MontantNet ,Autre_Exercice,
                    Compte_Liaison_MBrut,Compte_Liaison_Amortis,Compte_Liaison_MontantNet,Compte_Liaison_Exercice,
                    Immobilisation_MBrut, Immobilisation_Amortis, Immobilisation_MontantNet, Immobilisation_Exercice, Titre_Participation_MontantBrut, Titre_Participation_Amortis,
                    Titre_Participation_MontantNet, Titre_Participation_Exercice, Immobilisation_Corporelle_MBrut, Immobilisation_Corporelle_Amortis, Immobilisation_Corporelle_MontantNet,
                    Immobilisation_Corporelle_Exercice, Immobilisation_Incorporelle_MBrut, Immobilisation_Incorporelle_Amortis, Immobilisation_Incorporelle_MontantNet, Immobilisation_Incorporelle_Exercice,
                    Immobilisation_Financiere_MBrut, Immobilisation_Financiere_Amortis, Immobilisation_Financiere_MontantNet, Immobilisation_Financiere_Exercice, Total_MontantBrut,
                    Total_Amortissement, Total_MontantNet, Total_Exercice);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportationBilanActif_Load(object sender, EventArgs e)
        {

        }
    }
}
