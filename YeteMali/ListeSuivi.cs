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
    public partial class ListeSuivi : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ListeSuivi()
        {
            InitializeComponent();
        }

        //Charge la grille
        public void chargeliste()
        {
            dgListesuivi.DataSource = db.ListeSuiviCaisses();

        }
        private void ListeSuivi_Load(object sender, EventArgs e)
        {
            chargeliste();
        }

        private void dgListesuivi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
