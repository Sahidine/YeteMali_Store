using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeteMali
{
    class Service_PapierTerrain
    {
        string chaine = ConfigurationManager.ConnectionStrings["chaine1"].ConnectionString;
        int d;
        //--------------------------------Ajout de la caisse-----------------------------------
        public int AjoutPapierTerrain(string caisse, string nomPrenom, decimal MontantDebourse ,DateTime datepapier,float taux ,decimal fraisPreleve,decimal fraisNotaire,string Garantie, string reference )
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ajout_DossierTerrain";
                cmd.Parameters.AddWithValue("@caisse", caisse);
                cmd.Parameters.AddWithValue("@nomPrenom", nomPrenom);
                cmd.Parameters.AddWithValue("@MontantDebourse", MontantDebourse );
                cmd.Parameters.AddWithValue("@datepapier", datepapier);
                cmd.Parameters.AddWithValue("@Taux", taux);
                cmd.Parameters.AddWithValue("@FraisPreleve", fraisPreleve);
                cmd.Parameters.AddWithValue("@FraisNotaire", fraisNotaire);
                cmd.Parameters.AddWithValue("@Garantie", Garantie);
                cmd.Parameters.AddWithValue("@Reference", reference);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }
        //--------------------------------Liste de la caisse-----------------------------------
        public DataTable ListePapierTerrain()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListePapierTerrain";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];
        }
    }
}
