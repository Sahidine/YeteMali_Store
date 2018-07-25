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
    public partial class Importation_Produit : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public Importation_Produit()
        {
            InitializeComponent();
            chargeCombo();
        }
        public void chargeCombo()
        {
            cbCaisse.DataSource = db.ListeCaisse();
        }
        private void Importation_Produit_Load(object sender, EventArgs e)
        {
            cbCaisse.Text = "";
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
         //------------------Les produits financiers----------------------------------------------      
         decimal Interet_Recu_MBrut= decimal.Parse(rw["Interet_Recu_MBrut"].ToString());  decimal Interet_Recu_Exercice= decimal.Parse(rw["Interet_Recu_Exercice"].ToString()); 
         decimal Autre_Produit_Financier_MBrut= decimal.Parse(rw["Autre_Produit_Financier_MBrut"].ToString()); decimal Autre_Produit_Financier_Exercice= decimal.Parse(rw["Autre_Produit_Financier_Exercice"].ToString());
         decimal Interet_Recu_etablissement_MBrut = decimal.Parse(rw["Interet_Recu_etablissement_MBrut"].ToString());decimal Interet_Recu_etablissement_Exercice = decimal.Parse(rw["Interet_Recu_etablissement_Exercice"].ToString());        
            //---------------------------------La somme des produits financiers----------------------------------------
         decimal Produit_Financier_MBrut = Interet_Recu_MBrut + Autre_Produit_Financier_MBrut + Interet_Recu_etablissement_MBrut;
         decimal Produit_Financier_Exercice = Interet_Recu_Exercice + Autre_Produit_Financier_Exercice + Interet_Recu_etablissement_Exercice;
            //----------------------------------------------------------------------------------------------------------------------------------
         //--------------------------Les autres revenus et services----------------------------------------------------
         decimal Surplus_Caisse_MBrut= decimal.Parse(rw["Surplus_Caisse_MBrut"].ToString()); decimal Surplus_Caisse_Exercice= decimal.Parse(rw["Surplus_Caisse_Exercice"].ToString());
         decimal Frais_Adhésion_MBrut= decimal.Parse(rw["Frais_Adhésion_MBrut"].ToString()); decimal Frais_Adhésion_Exercice= decimal.Parse(rw["Frais_Adhésion_Exercice"].ToString()); 
         decimal Frais_Service_MBrut= decimal.Parse(rw["Frais_Service_MBrut"].ToString()); decimal Frais_Service_Exercice= decimal.Parse(rw["Frais_Service_Exercice"].ToString());                   
         decimal Frais_Remplacement_Carnet_MBrut= decimal.Parse(rw["Frais_Remplacement_Carnet_MBrut"].ToString()); decimal Frais_Remplacement_Carnet_Exercice= decimal.Parse(rw["Frais_Remplacement_Carnet_Exercice"].ToString());
         decimal Pénalite_Retard_MBrut = decimal.Parse(rw["Pénalite_Retard_MBrut"].ToString()); decimal Pénalite_Retard_Exercice= decimal.Parse(rw["Pénalite_Retard_Exercice"].ToString()); 
         decimal Frais_Tenue_Compte_MBrut = decimal.Parse(rw["Frais_Tenue_Compte_MBrut"].ToString()); decimal Frais_Tenue_Compte_Exercice = decimal.Parse(rw["Frais_Tenue_Compte_Exercice"].ToString());
         decimal Frais_Divers_MBrut = decimal.Parse(rw["Frais_Divers_MBrut"].ToString()); decimal Frais_Divers_Exercice = decimal.Parse(rw["Frais_Divers_Exercice"].ToString());
        //-----------------------------La somme des revenus et services---------------------------------------------------
        decimal Revenu_Frais_Service_MBrut= Surplus_Caisse_MBrut + Frais_Adhésion_MBrut + Frais_Service_MBrut + Frais_Remplacement_Carnet_MBrut + Pénalite_Retard_MBrut + Frais_Tenue_Compte_MBrut + Frais_Divers_MBrut;
        decimal Revenu_Frais_Service_Exercice = Surplus_Caisse_Exercice + Frais_Adhésion_Exercice + Frais_Service_Exercice + Frais_Remplacement_Carnet_Exercice + Pénalite_Retard_Exercice + Frais_Tenue_Compte_Exercice + Frais_Divers_Exercice;
       
          //------------------------------------Les autres revenus et services--------------------------------------------
         decimal Produits_divers_MBrut= decimal.Parse(rw["Produits_divers_MBrut"].ToString());  decimal Produits_divers_Exercice= decimal.Parse(rw["Produits_divers_Exercice"].ToString());
         decimal Produit_Exceptionnel_MBrut= decimal.Parse(rw["Produit_Exceptionnel_MBrut"].ToString()); decimal Produit_Exceptionnel_Exercice= decimal.Parse(rw["Produit_Exceptionnel_Exercice"].ToString());
         decimal Autre_Revenu_Un_MBrut = decimal.Parse(rw["Autre_Revenu_Un_MBrut"].ToString()); decimal Autre_Revenu_Un_Exercice = decimal.Parse(rw["Autre_Revenu_Un_Exercice"].ToString());
         decimal Autre_Revenus_MBrut= decimal.Parse(rw["Autre_Revenus_MBrut"].ToString());
         decimal Autre_Revenus_Exercice = decimal.Parse(rw["Autre_Revenus_Exercice"].ToString());
          //------------------------------------------------------------------------------------
         decimal Reprise_Amortissement_MBrut= decimal.Parse(rw["Reprise_Amortissement_MBrut"].ToString());decimal Reprise_Amortissement_Exercice= decimal.Parse(rw["Reprise_Amortissement_Exercice"].ToString());
         decimal Total_Produit_MBrut = Produit_Financier_MBrut + Revenu_Frais_Service_MBrut + Autre_Revenus_MBrut + Reprise_Amortissement_MBrut;
         decimal Total_Produit_Exercice = Produit_Financier_Exercice + Revenu_Frais_Service_Exercice + Autre_Revenus_Exercice + Reprise_Amortissement_Exercice;    
      
                    db.AjoutEtatProduit(cbCaisse.SelectedValue.ToString(), DateTime.Parse(cbdate.Text), Produit_Financier_MBrut, Produit_Financier_Exercice,  Interet_Recu_MBrut, Interet_Recu_Exercice,
                                        Interet_Recu_etablissement_MBrut, Interet_Recu_etablissement_Exercice,Autre_Produit_Financier_MBrut, Autre_Produit_Financier_Exercice, Revenu_Frais_Service_MBrut, 
                                        Revenu_Frais_Service_Exercice, Surplus_Caisse_MBrut, Surplus_Caisse_Exercice,
                                        Frais_Adhésion_MBrut,  Frais_Adhésion_Exercice, Frais_Service_MBrut, Frais_Service_Exercice,  Frais_Remplacement_Carnet_MBrut,
                                        Frais_Remplacement_Carnet_Exercice,  Pénalite_Retard_MBrut,
                                        Pénalite_Retard_Exercice,  Frais_Tenue_Compte_MBrut,  Frais_Tenue_Compte_Exercice, Frais_Divers_MBrut, Frais_Divers_Exercice,
                                        Autre_Revenus_MBrut, Autre_Revenus_Exercice, Autre_Revenu_Un_MBrut, Autre_Revenu_Un_Exercice,Produits_divers_MBrut,
                                        Produits_divers_Exercice,  Produit_Exceptionnel_MBrut,  Produit_Exceptionnel_Exercice, Reprise_Amortissement_MBrut,
                                        Reprise_Amortissement_Exercice,
                                        Total_Produit_MBrut, Total_Produit_Exercice
                                       );
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
