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
    public partial class GestionEtatProduit : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public GestionEtatProduit()
        {
            InitializeComponent();
        }

        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            DataTable rest = db.TotalEtatproduits(DateTime.Parse(d_du.Text), DateTime.Parse(d_au.Text));
            DataRow dr1 = rest.Rows[0];
            //--------Produit financier-------------
            lb_111.Text = String.Format("{0:0,000}",dr1["total1"]);
            lb_22.Text = String.Format("{0:0,000}",dr1["total2"]);
            //--------Intéret reçu sur prêt-------------
            lb_33.Text = String.Format("{0:0,000}",dr1["total3"]);
            lb_44.Text = String.Format("{0:0,000}",dr1["total4"]);
            //--------Intéret reçu des établissements financiers-------------
            lb_55.Text = String.Format("{0:0,000}",dr1["total5"]);
            lb_66.Text = String.Format("{0:0,000}",dr1["total6"]);
            //--------Autres produits financiers-------------
            lb_77.Text = String.Format("{0:0,000}",dr1["total7"]);
            lb_88.Text = String.Format("{0:0,000}",dr1["total8"]);
            //--------Revenu de frais de service-------------
            lb_99.Text = String.Format("{0:0,000}",dr1["total9"]);
            lb_1010.Text = String.Format("{0:0,000}",dr1["total10"]);
            //--------Surplus de caisse-------------
            lb_1111.Text = String.Format("{0:0,000}",dr1["total11"]);
            lb_1212.Text = String.Format("{0:0,000}",dr1["total12"]);
            //--------Frais d'adhésion-------------
            lb_1313.Text = String.Format("{0:0,000}",dr1["total13"]);
            lb_1414.Text = String.Format("{0:0,000}",dr1["total14"]);
            //--------Frais service depot salaire-------------
            lb_1515.Text = String.Format("{0:0,000}",dr1["total15"]);
            lb_1616.Text = String.Format("{0:0,000}",dr1["total16"]);
            //--------Frais remplacement carnet-------------
            lb_1717.Text = String.Format("{0:0,000}",dr1["total17"]);
            lb_1818.Text = String.Format("{0:0,000}",dr1["total18"]);
            //--------Pénalité sur retard-------------
            lb_1919.Text = String.Format("{0:0,000}",dr1["total19"]);
            lb_2020.Text = String.Format("{0:0,000}",dr1["total20"]);
            
            //--------Frais de tenu de compte-------------
            lb_2121.Text = String.Format("{0:0,000}",dr1["total21"]);
            lb_2222.Text = String.Format("{0:0,000}",dr1["total22"]);
            //--------Frais divers-------------
            lb_2323.Text = String.Format("{0:0,000}",dr1["total23"]);
            lb_2424.Text = String.Format("{0:0,000}",dr1["total24"]);
            //--------Autre revenu de service-------------
            lb_2525.Text = String.Format("{0:0,000}",dr1["total25"]);
            lb_2626.Text = String.Format("{0:0,000}",dr1["total26"]);
            
            //--------Produits divers-------------
            lb_2727.Text = String.Format("{0:0,000}",dr1["total29"]);
            lb_2828.Text = String.Format("{0:0,000}",dr1["total30"]);
            //--------Produits exceptionnels-------------
            lb_2929.Text = String.Format("{0:0,000}",dr1["total31"]);
            lb_3030.Text = String.Format("{0:0,000}",dr1["total32"]);
            //--------Reprise sur amortissement et provision-------------
            lb_3131.Text = String.Format("{0:0,000}",dr1["total33"]);
            lb_3232.Text = String.Format("{0:0,000}",dr1["total34"]);
            //-------Total des produits-------------
            lb_3333.Text = String.Format("{0:0,000}",dr1["total35"]);
            lb_3434.Text = String.Format("{0:0,000}",dr1["total36"]);
            /*-------Excédent------------
            lb_3535.Text = dr1["total35"].ToString();
            lb_3636.Text = dr1["total36"].ToString();*/
        }
    }
}
