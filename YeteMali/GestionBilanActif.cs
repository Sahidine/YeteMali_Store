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
    public partial class GestionBilanActif : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public GestionBilanActif()
        {
            InitializeComponent();
            chargeCombo();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            DataTable rest = db.ListeBilanCaiss(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text),cbcaisse.Text);
            DataRow dr1 = rest.Rows[0];
            //--------Operation avec les institutions financières-------------
            lb_Operation_Institution1.Text = String.Format("{0:0,000}", dr1["total1"]);
            lb_Operation_Institution2.Text = String.Format("{0:0,000}", dr1["total2"]);
            lb_Operation_Institution3.Text = String.Format("{0:0,000}", dr1["total3"]);
            lb_Operation_Institution4.Text = String.Format("{0:0,000}", dr1["total4"]);
            //--------------------------Encaisse---------------------------
            lb_Encaisse1.Text = String.Format("{0:0,000}", dr1["total5"]);
            lb_Encaisse2.Text = String.Format("{0:0,000}", dr1["total6"]);
            lb_Encaisse3.Text = String.Format("{0:0,000}", dr1["total7"]);
            lb_Encaisse4.Text = String.Format("{0:0,000}", dr1["total8"]);
            //---------------------Etablissement-------------------------
            lb_etablissement1.Text = String.Format("{0:0,000}", dr1["total9"]);
            lb_etablissement2.Text = String.Format("{0:0,000}", dr1["total10"]);
            lb_etablissement3.Text = String.Format("{0:0,000}", dr1["total11"]);
            lb_etablissement4.Text = String.Format("{0:0,000}", dr1["total12"]);
            //---------------------Fond et garantie-------------------------
            lb_fond_garantie1.Text = String.Format("{0:0,000}", dr1["total13"]);
            lb_fond_garantie2.Text = String.Format("{0:0,000}", dr1["total14"]);
            lb_fond_garantie3.Text = String.Format("{0:0,000}", dr1["total15"]);
            lb_fond_garantie4.Text = String.Format("{0:0,000}", dr1["total16"]);
            //---------------------Operation avec les membres-------------------------
            lb_Opération_membre1.Text = String.Format("{0:0,000}", dr1["total17"]);
            lb_Opération_membre2.Text = String.Format("{0:0,000}", dr1["total18"]);
            lb_Opération_membre3.Text = String.Format("{0:0,000}", dr1["total19"]);
            lb_Opération_membre4.Text = String.Format("{0:0,000}", dr1["total20"]);
            //---------------------Credit sain-------------------------
            lb_Credit_sain1.Text = String.Format("{0:0,000}", dr1["total21"]);
            lb_Credit_sain2.Text = String.Format("{0:0,000}", dr1["total22"]);
            lb_Credit_sain3.Text = String.Format("{0:0,000}", dr1["total23"]);
            lb_Credit_sain4.Text = String.Format("{0:0,000}", dr1["total24"]);
            //---------------------Creance rattachée-------------------------
            lb_creance_rattaché1.Text = String.Format("{0:0,000}", dr1["total25"]);
            lb_creance_rattaché2.Text = String.Format("{0:0,000}", dr1["total26"]);
            lb_creance_rattaché3.Text = String.Format("{0:0,000}", dr1["total27"]);
            lb_creance_rattaché4.Text = String.Format("{0:0,000}", dr1["total28"]);
            //---------------------Creance en souffrance-------------------------
            lb_creance_souffrance1.Text = String.Format("{0:0,000}", dr1["total29"]);
            lb_creance_souffrance2.Text = String.Format("{0:0,000}", dr1["total30"]);
            lb_creance_souffrance3.Text = String.Format("{0:0,000}", dr1["total31"]);
            lb_creance_souffrance4.Text = String.Format("{0:0,000}", dr1["total32"]);
            //---------------------Operation diverses-------------------------
            lb_operation_diverse1.Text = String.Format("{0:0,000}", dr1["total33"]);
            lb_operation_diverse2.Text = String.Format("{0:0,000}", dr1["total34"]);
            lb_operation_diverse3.Text = String.Format("{0:0,000}", dr1["total35"]);
            lb_operation_diverse4.Text = String.Format("{0:0,000}", dr1["total36"]);
            //---------------------Stocks-------------------------
            lb_stock1.Text = String.Format("{0:0,000}", dr1["total37"]);
            lb_stock2.Text = String.Format("{0:0,000}", dr1["total38"]);
            lb_stock3.Text = String.Format("{0:0,000}", dr1["total39"]);
            lb_stock4.Text = String.Format("{0:0,000}", dr1["total40"]);
            //---------------------Debiteur divers-------------------------
            lb_debiteur1.Text = String.Format("{0:0,000}", dr1["total41"]);
            lb_debiteur2.Text = String.Format("{0:0,000}", dr1["total42"]);
            lb_debiteur3.Text = String.Format("{0:0,000}", dr1["total43"]);
            lb_debiteur4.Text = String.Format("{0:0,000}", dr1["total44"]);
            //---------------------Compte de regularisation actif-------------------------
            lb_compte_regularisation1.Text = String.Format("{0:0,000}", dr1["total45"]);
            lb_compte_regularisation2.Text = String.Format("{0:0,000}", dr1["total46"]);
            lb_compte_regularisation3.Text = String.Format("{0:0,000}", dr1["total47"]);
            lb_compte_regularisation4.Text = String.Format("{0:0,000}", dr1["total48"]);
            //---------------------Compte d'attente actif-------------------------
            lb_compte_attente1.Text = String.Format("{0:0,000}", dr1["total49"]);
            lb_compte_attente2.Text = String.Format("{0:0,000}", dr1["total50"]);
            lb_compte_attente3.Text = String.Format("{0:0,000}", dr1["total51"]);
            lb_compte_attente4.Text = String.Format("{0:0,000}", dr1["total52"]);
            //---------------------Depot de cautionnement-------------------------
            lb_depot1.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_MBrut"]);
            lb_depot2.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_Amort"]);
            lb_depot3.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_MontantNet"]);
            lb_depot4.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_Exercice"]);
            //---------------------Autre-------------------------
            lb_autre1.Text = String.Format("{0:0,000}", dr1["Autre_MBrut"]);
            lb_autre2.Text = String.Format("{0:0,000}", dr1["Autre_Amortis"]);
            lb_autre3.Text = String.Format("{0:0,000}", dr1["Autre_MontantNet"]);
            lb_autre4.Text = String.Format("{0:0,000}", dr1["Autre_Exercice"]);
            //---------------------Immobilisation-------------------------
            lb_immobilisation1.Text = String.Format("{0:0,000}", dr1["total53"]);
            lb_immobilisation2.Text = String.Format("{0:0,000}", dr1["total54"]);
            lb_immobilisation3.Text = String.Format("{0:0,000}", dr1["total55"]);
            lb_immobilisation4.Text = String.Format("{0:0,000}", dr1["total56"]);
            //----------------Titre de participation--------------------------
            lb_titre_participation1.Text = String.Format("{0:0,000}", dr1["total57"]);
            lb_titre_participation2.Text = String.Format("{0:0,000}", dr1["total58"]);
            lb_titre_participation3.Text = String.Format("{0:0,000}", dr1["total59"]);
            lb_titre_participation4.Text = String.Format("{0:0,000}", dr1["total60"]);
            //----------------Titre corporel--------------------------
            lb_immobolisation_corporelle1.Text = String.Format("{0:0,000}", dr1["total61"]);
            lb_immobolisation_corporelle2.Text = String.Format("{0:0,000}", dr1["total62"]);
            lb_immobolisation_corporelle3.Text = String.Format("{0:0,000}", dr1["total63"]);
            lb_immobolisation_corporelle4.Text = String.Format("{0:0,000}", dr1["total64"]);
            //----------------Immobilisation incorporelle--------------------------
            lb_immobolisation_incorporelle1.Text = String.Format("{0:0,000}", dr1["total65"]);
            lb_immobolisation_incorporelle2.Text = String.Format("{0:0,000}", dr1["total66"]);
            lb_immobolisation_incorporelle3.Text = String.Format("{0:0,000}", dr1["total67"]);
            lb_immobolisation_incorporelle4.Text = String.Format("{0:0,000}", dr1["total68"]);
            //----------------Immobilisation financiere--------------------------
            lb_immobilissation_financiere1.Text = String.Format("{0:0,000}", dr1["total69"]);
            lb_immobilissation_financiere2.Text = String.Format("{0:0,000}", dr1["total70"]);
            lb_immobilissation_financiere3.Text = String.Format("{0:0,000}", dr1["total71"]);
            lb_immobilissation_financiere4.Text = String.Format("{0:0,000}", dr1["total72"]);
            //-----------------------Total des bilans actifs----------------------
            lbAmor.Text = String.Format("{0:0,000}", dr1["Total_amortissement"]);
            lb_exercice.Text = String.Format("{0:0,000}", dr1["Total_Exercice"]);
            lbTotalMontaBrut1.Text = String.Format("{0:0,000}", dr1["total_brut"]);
            lb_Montantnet.Text = String.Format("{0:0,000}", dr1["Total_Montant_Net"]);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable rest = db.ParaTotalBilan(DateTime.Parse(d_du1.Text), DateTime.Parse(d_au1.Text));
            DataRow dr1 = rest.Rows[0];
            //--------Operation avec les institutions financières-------------
            lb_Operation_Institution1_.Text = String.Format("{0:0,000}", dr1["total1"]);
            lb_Operation_Institution2_.Text = String.Format("{0:0,000}", dr1["total2"]);
            lb_Operation_Institution3_.Text = String.Format("{0:0,000}", dr1["total3"]);
            lb_Operation_Institution4_.Text = String.Format("{0:0,000}", dr1["total4"]);
            //--------------------------Encaisse---------------------------
            lb_Encaisse1_.Text = String.Format("{0:0,000}", dr1["total5"]);
            lb_Encaisse2_.Text = String.Format("{0:0,000}", dr1["total6"]);
            lb_Encaisse3_.Text = String.Format("{0:0,000}", dr1["total7"]);
            lb_Encaisse4_.Text = String.Format("{0:0,000}", dr1["total8"]);
            //---------------------Etablissement-------------------------
            lb_etablissement1_.Text = String.Format("{0:0,000}", dr1["total9"]);
            lb_etablissement2_.Text = String.Format("{0:0,000}", dr1["total10"]);
            lb_etablissement3_.Text = String.Format("{0:0,000}", dr1["total11"]);
            lb_etablissement4_.Text = String.Format("{0:0,000}", dr1["total12"]);
           //---------------------Fond et garantie-------------------------
            lb_fond_garantie1_.Text = String.Format("{0:0,000}", dr1["total13"]);
            lb_fond_garantie2_.Text = String.Format("{0:0,000}", dr1["total14"]);
            lb_fond_garantie3_.Text = String.Format("{0:0,000}", dr1["total15"]);
            lb_fond_garantie4_.Text = String.Format("{0:0,000}", dr1["total16"]);
           //---------------------Operation avec les membres-------------------------
            lb_Opération_membre1_.Text = String.Format("{0:0,000}", dr1["total17"]);
            lb_Opération_membre2_.Text = String.Format("{0:0,000}", dr1["total18"]);
            lb_Opération_membre3_.Text = String.Format("{0:0,000}", dr1["total19"]);
            lb_Opération_membre4_.Text = String.Format("{0:0,000}", dr1["total20"]);
           //---------------------Credit sain-------------------------
            lb_Credit_sain1_.Text = String.Format("{0:0,000}", dr1["total21"]);
            lb_Credit_sain2_.Text = String.Format("{0:0,000}", dr1["total22"]);
            lb_Credit_sain3_.Text = String.Format("{0:0,000}", dr1["total23"]);
            lb_Credit_sain4_.Text = String.Format("{0:0,000}", dr1["total24"]);
              //---------------------Creance rattachée-------------------------
            lb_creance_rattaché1_.Text = String.Format("{0:0,000}", dr1["total25"]);
            lb_creance_rattaché2_.Text = String.Format("{0:0,000}", dr1["total26"]);
            lb_creance_rattaché3_.Text = String.Format("{0:0,000}", dr1["total27"]);
            lb_creance_rattaché4_.Text = String.Format("{0:0,000}", dr1["total28"]);
            //---------------------Creance en souffrance-------------------------
            lb_creance_souffrance1_.Text = String.Format("{0:0,000}", dr1["total29"]);
            lb_creance_souffrance2_.Text = String.Format("{0:0,000}", dr1["total30"]);
            lb_creance_souffrance3_.Text = String.Format("{0:0,000}", dr1["total31"]);
            lb_creance_souffrance4_.Text = String.Format("{0:0,000}", dr1["total32"]);
           //---------------------Operation diverses-------------------------
            lb_operation_diverse1_.Text = String.Format("{0:0,000}", dr1["total33"]);
            lb_operation_diverse2_.Text = String.Format("{0:0,000}", dr1["total34"]);
            lb_operation_diverse3_.Text = String.Format("{0:0,000}", dr1["total35"]);
            lb_operation_diverse4_.Text = String.Format("{0:0,000}", dr1["total36"]);
              //---------------------Stocks-------------------------
            lb_stock1_.Text = String.Format("{0:0,000}", dr1["total37"]);
            lb_stock2_.Text = String.Format("{0:0,000}", dr1["total38"]);
            lb_stock3_.Text = String.Format("{0:0,000}", dr1["total39"]);
            lb_stock4_.Text = String.Format("{0:0,000}", dr1["total40"]);
            //---------------------Debiteur divers-------------------------
            lb_debiteur1_.Text = String.Format("{0:0,000}", dr1["total41"]);
            lb_debiteur2_.Text = String.Format("{0:0,000}", dr1["total42"]);
            lb_debiteur3_.Text = String.Format("{0:0,000}", dr1["total43"]);
            lb_debiteur4_.Text = String.Format("{0:0,000}", dr1["total44"]);
           /* //---------------------Compte de regularisation actif-------------------------
            lb_compte_regularisation1_.Text = String.Format("{0:0,000}", dr1["total45"]);
            lb_compte_regularisation2_.Text = String.Format("{0:0,000}", dr1["total46"]);
            lb_compte_regularisation3_.Text = String.Format("{0:0,000}", dr1["total47"]);
            lb_compte_regularisation4_.Text = String.Format("{0:0,000}", dr1["total48"]);*/
            //---------------------Compte d'attente actif-------------------------
            lb_compte_attente1_.Text = String.Format("{0:0,000}", dr1["total49"]);
            lb_compte_attente2_.Text = String.Format("{0:0,000}", dr1["total50"]);
            lb_compte_attente3_.Text = String.Format("{0:0,000}", dr1["total51"]);
            lb_compte_attente4_.Text = String.Format("{0:0,000}", dr1["total52"]);
            //---------------------Depot de cautionnement-------------------------
            lb_depot1_.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_MBrut"]);
            lb_depot2_.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_Amort"]);
            lb_depot3_.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_MontantNet"]);
            lb_depot4_.Text = String.Format("{0:0,000}", dr1["Depot_Cautionnement_Exercice"]);
            //---------------------Autre-------------------------
            lb_autre1_.Text = String.Format("{0:0,000}", dr1["Autre_MBrut"]);
            lb_autre2_.Text = String.Format("{0:0,000}", dr1["Autre_Amortis"]);
            lb_autre3_.Text = String.Format("{0:0,000}", dr1["Autre_MontantNet"]);
            lb_autre4_.Text = String.Format("{0:0,000}", dr1["Autre_Exercice"]);
            //---------------------Immobilisation-------------------------
            lb_immobilisation1_.Text = String.Format("{0:0,000}", dr1["total53"]);
            lb_immobilisation2_.Text = String.Format("{0:0,000}", dr1["total54"]);
            lb_immobilisation3_.Text = String.Format("{0:0,000}", dr1["total55"]);
            lb_immobilisation4_.Text = String.Format("{0:0,000}", dr1["total56"]);
            //----------------Titre de participation--------------------------
            lb_titre_participation1_.Text = String.Format("{0:0,000}", dr1["total57"]);
            lb_titre_participation2_.Text = String.Format("{0:0,000}", dr1["total58"]);
            lb_titre_participation3_.Text = String.Format("{0:0,000}", dr1["total59"]);
            lb_titre_participation4_.Text = String.Format("{0:0,000}", dr1["total60"]);
            //----------------Titre corporel--------------------------
            lb_immobolisation_corporelle1_.Text = String.Format("{0:0,000}", dr1["total61"]);
            lb_immobolisation_corporelle2_.Text = String.Format("{0:0,000}", dr1["total62"]);
            lb_immobolisation_corporelle3_.Text = String.Format("{0:0,000}", dr1["total63"]);
            lb_immobolisation_corporelle4_.Text = String.Format("{0:0,000}", dr1["total64"]);
            //----------------Immobilisation incorporelle--------------------------
            lb_immobolisation_incorporelle1_.Text = String.Format("{0:0,000}", dr1["total65"]);
            lb_immobolisation_incorporelle2_.Text = String.Format("{0:0,000}", dr1["total66"]);
            lb_immobolisation_incorporelle3_.Text = String.Format("{0:0,000}", dr1["total67"]);
            lb_immobolisation_incorporelle4_.Text = String.Format("{0:0,000}", dr1["total68"]);
            //----------------Immobilisation financiere--------------------------
            lb_immobilissation_financiere1_.Text = String.Format("{0:0,000}", dr1["total69"]);
            lb_immobilissation_financiere2_.Text = String.Format("{0:0,000}", dr1["total70"]);
            lb_immobilissation_financiere3_.Text = String.Format("{0:0,000}", dr1["total71"]);
            lb_immobilissation_financiere4_.Text = String.Format("{0:0,000}", dr1["total72"]);
            //-----------------------Total des bilans actifs----------------------
            lbAmor_.Text = String.Format("{0:0,000}", dr1["Total_amortissement"]);
            lb_exercice_.Text = String.Format("{0:0,000}", dr1["Total_Exercice"]);
            lbTotalMontaBrut1_.Text = String.Format("{0:0,000}", dr1["total_brut"]);
            lb_Montantnet_.Text = String.Format("{0:0,000}", dr1["Total_Montant_Net"]);
            
        }
        public void chargeCombo()
        {
            cbcaisse.DataSource = db.ListeCaisse();
        }
        //----------------Methode pour la liste des bilans actifs----------------
        public void listeBilanActif()
        {
            //------------------Liste des bilans Actif
            dgListesuivi.DataSource = db.ListeBilanActif();
        }
        private void GestionBilanActif_Load(object sender, EventArgs e)
        {
            listeBilanActif();
            cbcaisse.Text = " ";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            tab_aperçu.Hide();

        }
    }
}
