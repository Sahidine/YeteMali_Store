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
    public partial class Importation_Charge : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public Importation_Charge()
        {
            InitializeComponent();
            chargeCombo();
        }
        public void chargeCombo()
        {
            cbCaisse.DataSource = db.ListeCaisse();
        }
        private void Importation_Charge_Load(object sender, EventArgs e)
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


                    //----------------------------Les charges financières--------------------------------------------------------------
                    decimal Interet_Verse_MBrut = decimal.Parse(rw["Interet_Verse_MBrut"].ToString()); decimal Interet_Verse_Exercice = decimal.Parse(rw["Interet_Verse_Exercice"].ToString());
                    decimal Autre_Charge_Financiere_MBrut = decimal.Parse(rw["Autre_Charge_Financiere_MBrut"].ToString()); decimal Autre_Charge_Financiere_Exercice = decimal.Parse(rw["Autre_Charge_Financiere_Exercice"].ToString());
                    //La sommes sur les charges financières( les deux lignes)
                    decimal Charge_Financiere_MBrut = Interet_Verse_MBrut + Autre_Charge_Financiere_MBrut;
                    decimal Charge_Financiere_Exercice = Interet_Verse_Exercice + Autre_Charge_Financiere_Exercice;
                   //--------------------------Les achats et services extérieurs-----------------------------------------------------------
                    decimal Achat_MBrut = decimal.Parse(rw["Achat_MBrut"].ToString()); decimal Achat_Exercice = decimal.Parse(rw["Achat_Exercice"].ToString());
                    decimal Eau_Electricite_MBrut = decimal.Parse(rw["Eau_Electricite_MBrut"].ToString()); decimal Eau_Electricite_Exercice = decimal.Parse(rw["Eau_Electricite_Exercice"].ToString());
                    decimal Loyer_MBrut = decimal.Parse(rw["Loyer_MBrut"].ToString()); decimal Loyer_Exercice = decimal.Parse(rw["Loyer_Exercice"].ToString());
                    decimal Entretien_Reparation_MBrut = decimal.Parse(rw["Entretien_Reparation_MBrut"].ToString()); decimal Entretien_Reparation_Exercice = decimal.Parse(rw["Entretien_Reparation_Exercice"].ToString());
                    decimal Prime_Assurance_MBrut = decimal.Parse(rw["Prime_Assurance_MBrut"].ToString()); decimal Prime_Assurance_Exercice = decimal.Parse(rw["Prime_Assurance_Exercice"].ToString());                 
                    //La sommes sur les achats et services ( les quatres lignes)                    
                    decimal Achat_Service_Exterieur_MBrut = Achat_MBrut + Eau_Electricite_MBrut +  Loyer_MBrut + Entretien_Reparation_MBrut + Prime_Assurance_MBrut; 
                    decimal Achat_Service_Exterieur_Exercice = Achat_Exercice + Eau_Electricite_Exercice + Loyer_Exercice + Entretien_Reparation_Exercice + Prime_Assurance_Exercice;
                    //------------------------Les autres service extérieurs---------------------------------------------
                    decimal Publicite_Relation_publique_MBrut = decimal.Parse(rw["Publicite_Relation_publique_MBrut"].ToString()); decimal Publicite_Relation_publique_Exercice = decimal.Parse(rw["Publicite_Relation_publique_Exercice"].ToString());
                    decimal Transport_deplacement_MBrut = decimal.Parse(rw["Transport_deplacement_MBrut"].ToString()); decimal Transport_deplacement_Exercice = decimal.Parse(rw["Transport_deplacement_Exercice"].ToString());
                    decimal Frais_Postaux_Telecommunication_MBrut = decimal.Parse(rw["Frais_Postaux_Telecommunication_MBrut"].ToString()); decimal Frais_Postaux_Telecommunication_Exercice = decimal.Parse(rw["Frais_Postaux_Telecommunication_Exercice"].ToString());
                    decimal Frais_Gestion_Credit_Entreco_MBrut = decimal.Parse(rw["Frais_Gestion_Credit_Entreco_MBrut"].ToString()); decimal Frais_Gestion_Credit_Entreco_Exercice = decimal.Parse(rw["Frais_Gestion_Credit_Entreco_Exercice"].ToString());
                    decimal Frais_Formation_MBrut = decimal.Parse(rw["Frais_Formation_MBrut"].ToString()); decimal Frais_Formation_Exercice = decimal.Parse(rw["Frais_Formation_Exercice"].ToString());
                    decimal Frais_Tenue_MBrut = decimal.Parse(rw["Frais_Tenue_MBrut"].ToString()); decimal Frais_Tenue_Exercice = decimal.Parse(rw["Frais_Tenue_Exercice"].ToString());
                    decimal Frais_Gardiennage_MBrut = decimal.Parse(rw["Frais_Gardiennage_MBrut"].ToString()); decimal Frais_Gardiennage_Exercice = decimal.Parse(rw["Frais_Gardiennage_Exercice"].ToString());
                    decimal Divers_MBrut = decimal.Parse(rw["Divers_MBrut"].ToString()); decimal Divers_Exercice = decimal.Parse(rw["Divers_Exercice"].ToString());
                    //La sommes sur les autres services ( les huits lignes)                                      
                    decimal Autre_Service_MBrut =Publicite_Relation_publique_MBrut + Transport_deplacement_MBrut + Frais_Postaux_Telecommunication_MBrut + Frais_Gestion_Credit_Entreco_MBrut + Frais_Formation_MBrut + Frais_Tenue_MBrut + Frais_Gardiennage_MBrut + Divers_MBrut;
                    decimal Autre_Service_Exercice = Publicite_Relation_publique_Exercice + Transport_deplacement_Exercice + Frais_Postaux_Telecommunication_Exercice + Frais_Gestion_Credit_Entreco_Exercice + Frais_Formation_Exercice + Frais_Tenue_Exercice + Frais_Gardiennage_Exercice + Divers_Exercice;
                    //------------------------Les charges des personnes-----------------------------------------------                   
                    decimal Charges_Salariales_MBrut = decimal.Parse(rw["Charges_Salariales_MBrut"].ToString()); decimal Charges_Salariales_Exercice = decimal.Parse(rw["Charges_Salariales_Exercice"].ToString());
                    decimal Charges_Sociale_MBrut = decimal.Parse(rw["Charges_Sociale_MBrut"].ToString()); decimal Charges_Sociale_Exercice = decimal.Parse(rw["Charges_Sociale_Exercice"].ToString());
                    decimal Frais_Remplacement_MBrut = decimal.Parse(rw["Frais_Remplacement_MBrut"].ToString()); decimal Frais_Remplacement_Exercice = decimal.Parse(rw["Frais_Remplacement_Exercice"].ToString());
                    decimal Assurance_Maladie_MBrut = decimal.Parse(rw["Assurance_Maladie_MBrut"].ToString()); decimal Assurance_Maladie_Exercice = decimal.Parse(rw["Assurance_Maladie_Exercice"].ToString());
                    decimal Bien_Etre_Employe_MBrut = decimal.Parse(rw["Bien_Etre_Employe_MBrut"].ToString()); decimal Bien_Etre_Employe_Exercice = decimal.Parse(rw["Bien_Etre_Employe_Exercice"].ToString());
                    //La sommes sur les charges de personnes ( les cinq lignes)                                                          
                    decimal Charge_Personne_MBrut = Charges_Salariales_MBrut +Charges_Sociale_MBrut +  Frais_Remplacement_MBrut + Assurance_Maladie_MBrut + Bien_Etre_Employe_MBrut;
                    decimal Charge_Personne_Exercice = Charges_Salariales_Exercice + Charges_Sociale_Exercice + Frais_Remplacement_Exercice + Assurance_Maladie_Exercice + Bien_Etre_Employe_Exercice;
              
                    decimal Autre_Charge_MBrut = decimal.Parse(rw["Autre_Charge_MBrut"].ToString()); decimal Autre_Charge_Exercice = decimal.Parse(rw["Autre_Charge_Exercice"].ToString());
                    decimal Dotation_Amortissement_MBrut = decimal.Parse(rw["Dotation_Amortissement_MBrut"].ToString()); decimal Dotation_Amortissement_Exercice = decimal.Parse(rw["Dotation_Amortissement_Exercice"].ToString());
                    decimal Charge_Exceptionnelle_MBrut = decimal.Parse(rw["Charge_Exceptionnelle_MBrut"].ToString()); decimal Charge_Exceptionnelle_Exercice = decimal.Parse(rw["Charge_Exceptionnelle_Exercice"].ToString());
                    decimal Impôt_Taxe_MBrut = decimal.Parse(rw["Impôt_Taxe_MBrut"].ToString()); decimal Impôt_Taxe_Exercice = decimal.Parse(rw["Impôt_Taxe_Exercice"].ToString());
                    //---------------------------------La somme total des charges---------------------------------------------------
                    decimal Total_Charge_MBrut = Charge_Financiere_MBrut + Achat_Service_Exterieur_MBrut + Autre_Service_MBrut + Charge_Personne_MBrut + Autre_Charge_MBrut + Dotation_Amortissement_MBrut + Charge_Exceptionnelle_MBrut + Impôt_Taxe_MBrut;
                    decimal Total_Charge_Exercice = Charge_Financiere_Exercice + Achat_Service_Exterieur_Exercice + Autre_Service_Exercice + Charge_Personne_Exercice + Autre_Charge_Exercice + Dotation_Amortissement_Exercice + Charge_Exceptionnelle_Exercice + Impôt_Taxe_Exercice;
                    db.AjoutEtatCharge(cbCaisse.SelectedValue.ToString(), DateTime.Parse(cbdate.Text), Charge_Financiere_MBrut, Charge_Financiere_Exercice, Interet_Verse_MBrut, Interet_Verse_Exercice,
           Autre_Charge_Financiere_MBrut, Autre_Charge_Financiere_Exercice, Achat_Service_Exterieur_MBrut, Achat_Service_Exterieur_Exercice, Achat_MBrut, Achat_Exercice,
           Eau_Electricite_MBrut, Eau_Electricite_Exercice, Loyer_MBrut, Loyer_Exercice, Entretien_Reparation_MBrut, Entretien_Reparation_Exercice, Prime_Assurance_MBrut, Prime_Assurance_Exercice,
           Autre_Service_MBrut, Autre_Service_Exercice, Publicite_Relation_publique_MBrut, Publicite_Relation_publique_Exercice, Transport_deplacement_MBrut, Transport_deplacement_Exercice, Frais_Postaux_Telecommunication_MBrut,
           Frais_Postaux_Telecommunication_Exercice, Frais_Gestion_Credit_Entreco_MBrut, Frais_Gestion_Credit_Entreco_Exercice, Frais_Formation_MBrut, Frais_Formation_Exercice,
           Frais_Tenue_MBrut, Frais_Tenue_Exercice, Frais_Gardiennage_MBrut, Frais_Gardiennage_Exercice, Divers_MBrut, Divers_Exercice, Charge_Personne_MBrut, Charge_Personne_Exercice, Charges_Salariales_MBrut, Charges_Salariales_Exercice,
           Charges_Sociale_MBrut, Charges_Sociale_Exercice, Frais_Remplacement_MBrut, Frais_Remplacement_Exercice, Assurance_Maladie_MBrut, Assurance_Maladie_Exercice, Bien_Etre_Employe_MBrut, Bien_Etre_Employe_Exercice, Autre_Charge_MBrut,
           Autre_Charge_Exercice, Dotation_Amortissement_MBrut, Dotation_Amortissement_Exercice, Charge_Exceptionnelle_MBrut, Charge_Exceptionnelle_Exercice, Impôt_Taxe_MBrut, Impôt_Taxe_Exercice,
           Total_Charge_MBrut, Total_Charge_Exercice);
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
