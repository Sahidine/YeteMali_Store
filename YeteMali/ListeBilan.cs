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
    public partial class ListeBilan : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public ListeBilan()
        {
            InitializeComponent();
        }

        public void bilan()
        {
            dgListesuivi.DataSource = db.ListeBilan();
        }

        private void ListeBilan_Load(object sender, EventArgs e)
        {
            bilan();
        }

        private void ListeBilan_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MenuBilan b = new MenuBilan();
            b.Show();
        }
    }
}
