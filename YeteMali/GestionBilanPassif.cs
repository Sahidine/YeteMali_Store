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
    public partial class GestionBilanPassif : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public GestionBilanPassif()
        {
            InitializeComponent();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        { 
            DataTable rest = db.TotalBilanPassiff(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr1 = rest.Rows[0];
            //--------Operation avec les membres-------------
            lb_Op_Avec_Membre_Mbrut.Text =  String.Format("{0:0,000}",dr1["total1"]);
            lb_1.Text =  String.Format("{0:0,000}",dr1["total2"]);
            //--------Epargne à vue-------------
            lb_Epargne_a_Vue_MBrut.Text = String.Format("{0:0,000}",dr1["total3"]);
            lb_2.Text = String.Format("{0:0,000}",dr1["total4"]);
            //--------Epargne à terme-------------
            lb_epargne_terme_brut.Text = String.Format("{0:0,000}",dr1["total5"]);
            lb_3.Text = String.Format("{0:0,000}",dr1["total6"]);
            //--------depot à terme-------------
            lb_depot_terme_brut.Text = String.Format("{0:0,000}",dr1["total7"]);
            lb_4.Text = String.Format("{0:0,000}",dr1["total8"]);
            //--------epargne stable -------------
            lb_epargne_stable_brut.Text = String.Format("{0:0,000}",dr1["total9"]);
            lb_5.Text = String.Format("{0:0,000}",dr1["total10"]);
            //--------epargne bloquée -------------
            lb_epargne_bloque_brut.Text = String.Format("{0:0,000}",dr1["total11"]);
            lb_6.Text = String.Format("{0:0,000}",dr1["total12"]);
            //--------dette rattachée -------------
            lb_dette_rattache_brut.Text = String.Format("{0:0,000}",dr1["total13"]);
            lb_7.Text = String.Format("{0:0,000}",dr1["total14"]);
            //--------Compte inactif -------------
            lb_compte_inactif_brut.Text = String.Format("{0:0,000}",dr1["total15"]);
            lb_8.Text = String.Format("{0:0,000}",dr1["total16"]);
            //--------Operation diverses -------------
            lb_operation_diverse_brut.Text = String.Format("{0:0,000}",dr1["total17"]);
            lb_9.Text = String.Format("{0:0,000}",dr1["total18"]);
            //--------crediteur diverss -------------
            lb_crediteur_diver_brut.Text = String.Format("{0:0,000}",dr1["total19"]);
            lb_10.Text = String.Format("{0:0,000}",dr1["total20"]);
            //-------- Autre crediteur diverss -------------
            lb_autre_crediteur_brut.Text = String.Format("{0:0,000}",dr1["total21"]);
            lb_11.Text = String.Format("{0:0,000}",dr1["total22"]);
            //-------- Compte regularisation passif -------------
            lb_compte_regularisation_brut.Text = String.Format("{0:0,000}",dr1["total23"]);
            lb_12.Text = String.Format("{0:0,000}",dr1["total24"]);
            //-------- Compte regularisation passif -------------
            lb_compte_passif_brut.Text = String.Format("{0:0,000}",dr1["total25"]);
            lb_13.Text = String.Format("{0:0,000}",dr1["total26"]);
            //-------- Source de fond extérieur -------------
            lb_source_fond_brut.Text = String.Format("{0:0,000}",dr1["total27"]);
            lb_14.Text = String.Format("{0:0,000}",dr1["total28"]);
            //--------  fond garantie -------------
            lb_fond_garantie_brut.Text = String.Format("{0:0,000}",dr1["total29"]);
            lb_15.Text = String.Format("{0:0,000}",dr1["total30"]);
            //--------  Ligne de credit -------------
            lb_ligne_credit_brut.Text = String.Format("{0:0,000}",dr1["total31"]);
            lb_16.Text = String.Format("{0:0,000}",dr1["total32"]);
            //-------- Emprunt -------------
            lb_emprunt_brut.Text = String.Format("{0:0,000}",dr1["total33"]);
            lb_17.Text = String.Format("{0:0,000}",dr1["total34"]);
            //-------- provision, fond propre assimilé -------------
            lb_provision_brut.Text = String.Format("{0:0,000}",dr1["total35"]);
            lb_18.Text = String.Format("{0:0,000}",dr1["total36"]);
            //-------- Subvention reçue -------------
            lb_subvention_brut.Text = String.Format("{0:0,000}",dr1["total37"]);
            lb_19.Text = String.Format("{0:0,000}",dr1["total38"]);
            //-------- Provision pour risque -------------
            lb_provision_risque.Text = String.Format("{0:0,000}",dr1["total39"]);
            lb_20.Text = String.Format("{0:0,000}",dr1["total40"]);
            //-------- Report à nouveau -------------
            lb_report_nouveau_brut.Text = String.Format("{0:0,000}",dr1["total41"]);
            lb_21.Text = String.Format("{0:0,000}",dr1["total42"]);
            //-------- Réserve -------------
            lb_reserve_brut.Text = String.Format("{0:0,000}",dr1["total43"]);
            lb_22.Text = String.Format("{0:0,000}",dr1["total44"]);
            //-------- parts sociales -------------
            lb_part_social_brut.Text = String.Format("{0:0,000}",dr1["total45"]);
            lb_23.Text = String.Format("{0:0,000}",dr1["total46"]);
            //-------- Resulat en attente d'affectation -------------
            lb_resultat_brut.Text = String.Format("{0:0,000}",dr1["total47"]);
            lb_24.Text = String.Format("{0:0,000}",dr1["total48"]);
            //--------  Excédent-------------
            lb_excedent_brut.Text = String.Format("{0:0,000}",dr1["total49"]);
            lb_25.Text = String.Format("{0:0,000}",dr1["total50"]);
            //--------  Total des bilans passifs-------------
            lb_total_passif.Text = String.Format("{0:0,000}",dr1["total51"]);
            lb_26.Text = String.Format("{0:0,000}",dr1["total52"]);
           
            
        }
    }
}
