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
    public partial class MenuSuiviQuotidien : Form
    {
        Service_SuiviCaisse db = new Service_SuiviCaisse();
        public MenuSuiviQuotidien()
        {
            InitializeComponent();
        }

        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ImportationSuivi im = new ImportationSuivi();
            im.Show();
        }

        private void MenuSuiviQuotidien_Load(object sender, EventArgs e)
        {
            DataTable nombreCaisse = db.NombreCaisse();
            DataRow dr = nombreCaisse.Rows[0];
            NbreCaisse.Text = dr["Nombre"].ToString();

            //--------------------------
            DataTable nombreImportation = db.NombreImportation();
            DataRow dh = nombreImportation.Rows[0];
            lbImporation.Text = dh["NombreImportation"].ToString();
            //--------------------------
            DataTable nombreUtilisateur = db.NombreUser();
            DataRow dre = nombreUtilisateur.Rows[0];
            lbUser.Text = dre["NombreUser"].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SuiviQuotidien s = new SuiviQuotidien();
            s.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int imageNumber = 1;
        private void loadNextImage()
                {
                    if (imageNumber == 4)
                    {
                        imageNumber = 1;
                    }
                   // pbxImage.ImageLocation = string.Format(@"Images\{0}.jpg",imageNumber);
                    imageNumber++;
                }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadNextImage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatistiqueSuivi s = new StatistiqueSuivi();
            s.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Caisse c = new Caisse();
            c.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PapierTerrain p = new PapierTerrain();
            p.Show();
        }
    }
}
