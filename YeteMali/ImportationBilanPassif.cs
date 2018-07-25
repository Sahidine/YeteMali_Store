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
    public partial class ImportationBilanPassif : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ImportationBilanPassif()
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

                    
                    decimal Epargne_a_Vue_MBrut = decimal.Parse(rw["Epargne_a_Vue_MBrut"].ToString()); decimal Epargne_a_Vue_Exercice = decimal.Parse(rw["Epargne_a_Vue_Exercice"].ToString());
                    decimal Epargne_a_Terme_Unique_MBrut = decimal.Parse(rw["Epargne_a_Terme_Unique_MBrut"].ToString()); decimal Epargne_a_Terme_Unique_Exercice = decimal.Parse(rw["Epargne_a_Terme_Unique_Exercice"].ToString());
                    decimal Depot_periodique_MBrut = decimal.Parse(rw["Depot_periodique_MBrut"].ToString()); decimal Depot_periodique_Exercice = decimal.Parse(rw["Depot_periodique_Exercice"].ToString());
                    decimal Epargne_Stable_MBrut = decimal.Parse(rw["Epargne_Stable_MBrut"].ToString()); decimal Epargne_Stable_Exercice = decimal.Parse(rw["Epargne_Stable_Exercice"].ToString());
                    decimal Epargne_bloque_Nantie_MBrut = decimal.Parse(rw["Epargne_bloque_Nantie_MBrut"].ToString()); decimal Epargne_bloque_Nantie_Exercice = decimal.Parse(rw["Epargne_bloque_Nantie_Exercice"].ToString());
                    decimal Dette_Rattache_MBrut = decimal.Parse(rw["Dette_Rattache_MBrut"].ToString()); decimal Dette_Rattache_Exercice = decimal.Parse(rw["Dette_Rattache_Exercice"].ToString());
                    decimal Compte_inactif_MBrut = decimal.Parse(rw["Compte_inactif_MBrut"].ToString()); decimal Compte_inactif_Exercice = decimal.Parse(rw["Compte_inactif_Exercice"].ToString());
                   //----------------------------La somme des opération avec les membres---------------------------------------------
                    decimal Op_Avec_Membre_Mbrut = Epargne_a_Vue_MBrut + Epargne_a_Terme_Unique_MBrut + Depot_periodique_MBrut + Epargne_Stable_MBrut + Epargne_bloque_Nantie_MBrut + Dette_Rattache_MBrut + Compte_inactif_MBrut;
                    decimal Op_Avec_Membre_Exercice = Epargne_a_Vue_Exercice + Epargne_a_Terme_Unique_Exercice + Depot_periodique_Exercice + Epargne_Stable_Exercice + Epargne_bloque_Nantie_Exercice + Dette_Rattache_Exercice + Compte_inactif_Exercice;
                    
                    
                    decimal Crediteur_divers_MBrut = decimal.Parse(rw["Crediteur_divers_MBrut"].ToString()); decimal Crediteur_divers_Exercice = decimal.Parse(rw["Crediteur_divers_Exercice"].ToString());
                    decimal Autre_Crediteur_MBrut = decimal.Parse(rw["Autre_Crediteur_MBrut"].ToString()); decimal Autre_Crediteur_Exercice = decimal.Parse(rw["Autre_Crediteur_Exercice"].ToString());
                    decimal Compte_Regularisation_Passif_MBrut = decimal.Parse(rw["Compte_Regularisation_Passif_MBrut"].ToString()); decimal Compte_Regularisation_Passif_Exercice = decimal.Parse(rw["Compte_Regularisation_Passif_Exercice"].ToString());
                    decimal Compte_Attente_Passif_MBrut = decimal.Parse(rw["Compte_Attente_Passif_MBrut"].ToString()); decimal Compte_Attente_Passif_Exercice = decimal.Parse(rw["Compte_Attente_Passif_Exercice"].ToString());
                    //---------------------La somme des opérations diverses---------------------------------------------
                    decimal Op_Diverse_MBrut = Crediteur_divers_MBrut + Autre_Crediteur_MBrut + Compte_Regularisation_Passif_MBrut + Compte_Attente_Passif_MBrut;
                    decimal Op_Diverse_Exercice = Crediteur_divers_Exercice + Autre_Crediteur_Exercice + Compte_Regularisation_Passif_Exercice + Compte_Attente_Passif_Exercice;

                    
                    decimal Fond_Garantie_MBrut = decimal.Parse(rw["Fond_Garantie_MBrut"].ToString()); decimal Fond_Garantie_Exercice = decimal.Parse(rw["Fond_Garantie_Exercice"].ToString());
                    decimal Ligne_Credit_MBrut = decimal.Parse(rw["Ligne_Credit_MBrut"].ToString()); decimal Ligne_Credit_Exercice = decimal.Parse(rw["Ligne_Credit_Exercice"].ToString());
                    decimal Emprunt_MBrut = decimal.Parse(rw["Emprunt_MBrut"].ToString()); decimal Emprunt_Exercice = decimal.Parse(rw["Emprunt_Exercice"].ToString());
                    //---------------------------La somme des sources de fonds extérieurs-----------------------------
                    decimal Source_Fond_Exterieur_MBrut = Fond_Garantie_MBrut + Ligne_Credit_MBrut + Emprunt_MBrut;
                    decimal Source_Fond_Exterieur_Exercice = Fond_Garantie_Exercice + Ligne_Credit_Exercice +  Emprunt_Exercice;
                    
                   
                    decimal Subvention_Recu_MBrut = decimal.Parse(rw["Subvention_Recu_MBrut"].ToString()); decimal Subvention_Recu_Exercice = decimal.Parse(rw["Subvention_Recu_Exercice"].ToString());
                    decimal Provisions_Pour_Risque_MBrut = decimal.Parse(rw["Provisions_Pour_Risque_MBrut"].ToString()); decimal Provisions_Pour_Risque_Exercice = decimal.Parse(rw["Provisions_Pour_Risque_Exercice"].ToString());
                    decimal Report_a_Nouveau_MBrut = decimal.Parse(rw["Report_a_Nouveau_MBrut"].ToString()); decimal Report_a_Nouveau_Exercice = decimal.Parse(rw["Report_a_Nouveau_Exercice"].ToString());
                    decimal Reserve_MBrut = decimal.Parse(rw["Reserve_MBrut"].ToString()); decimal Reserve_Exercice = decimal.Parse(rw["Reserve_Exercice"].ToString());
                    decimal Parts_Sociale_MBrut = decimal.Parse(rw["Parts_Sociale_MBrut"].ToString()); decimal Parts_Sociale_Exercice = decimal.Parse(rw["Parts_Sociale_Exercice"].ToString());
                    decimal Resultat_Attente_Affectation_MBrut = decimal.Parse(rw["Resultat_Attente_Affectation_MBrut"].ToString()); decimal Resultat_Attente_Affectation_Exercice = decimal.Parse(rw["Resultat_Attente_Affectation_Exercice"].ToString());
                    decimal Excedent_MBrut = decimal.Parse(rw["Excedent_MBrut"].ToString()); decimal Excedent_Exercice = decimal.Parse(rw["Excedent_Exercice"].ToString());
                    //-----------------------------------La somme des provision, fond propre--------------------------------------------
                    decimal Provision_MBrut = Subvention_Recu_MBrut + Provisions_Pour_Risque_MBrut + Report_a_Nouveau_MBrut + Reserve_MBrut + Parts_Sociale_MBrut + Resultat_Attente_Affectation_MBrut + Excedent_MBrut;
                    decimal Provision_Exercice = Subvention_Recu_Exercice + Provisions_Pour_Risque_Exercice + Report_a_Nouveau_Exercice + Reserve_Exercice + Parts_Sociale_Exercice + Resultat_Attente_Affectation_Exercice + Excedent_Exercice;
                    
                    decimal Total_MBrut = Op_Avec_Membre_Mbrut + Op_Diverse_MBrut + Source_Fond_Exterieur_MBrut + Provision_MBrut;
                    decimal Total_Exercice = Op_Avec_Membre_Exercice + Op_Diverse_Exercice + Source_Fond_Exterieur_Exercice + Provision_Exercice;


                    db.AjoutBilanPassif(cbCaisse.SelectedValue.ToString(), DateTime.Parse(cbdate.Text), Op_Avec_Membre_Mbrut, Op_Avec_Membre_Exercice,
                                               Epargne_a_Vue_MBrut, Epargne_a_Vue_Exercice, Epargne_a_Terme_Unique_MBrut, Epargne_a_Terme_Unique_Exercice,
                                                Depot_periodique_MBrut, Depot_periodique_Exercice, Epargne_Stable_MBrut, Epargne_Stable_Exercice, Epargne_bloque_Nantie_MBrut,
                                                Epargne_bloque_Nantie_Exercice, Dette_Rattache_MBrut, Dette_Rattache_Exercice, Compte_inactif_MBrut, Compte_inactif_Exercice,
                                                Op_Diverse_MBrut, Op_Diverse_Exercice, Crediteur_divers_MBrut, Crediteur_divers_Exercice, Autre_Crediteur_MBrut,
                                                Autre_Crediteur_Exercice, Compte_Regularisation_Passif_MBrut, Compte_Regularisation_Passif_Exercice, Compte_Attente_Passif_MBrut,
                                                Compte_Attente_Passif_Exercice, Source_Fond_Exterieur_MBrut, Source_Fond_Exterieur_Exercice, Fond_Garantie_MBrut, Fond_Garantie_Exercice,
                                                Ligne_Credit_MBrut, Ligne_Credit_Exercice, Emprunt_MBrut, Emprunt_Exercice, Provision_MBrut, Provision_Exercice, Subvention_Recu_MBrut,
                                                 Subvention_Recu_Exercice, Provisions_Pour_Risque_MBrut, Provisions_Pour_Risque_Exercice, Report_a_Nouveau_MBrut, Report_a_Nouveau_Exercice,
                                                 Reserve_MBrut, Reserve_Exercice, Parts_Sociale_MBrut, Parts_Sociale_Exercice, Resultat_Attente_Affectation_MBrut, Resultat_Attente_Affectation_Exercice,
                                                 Excedent_MBrut, Excedent_Exercice, Total_MBrut, Total_Exercice);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportationBilanPassif_Load(object sender, EventArgs e)
        {
            cbCaisse.Text = "";
        }
    }
}
