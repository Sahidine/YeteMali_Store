using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YeteMali
{
    class Service_SuiviCaisse
    {

        //connexion a la base de données
       // const string chaine = @"Server=(local); Database=SuiviCaisse; Trusted_Connection=yes;";
         string chaine = ConfigurationManager.ConnectionStrings["chaine1"].ConnectionString;
        int d;
        //--------------------------------Ajout de la caisse-----------------------------------
        public int AjoutCaisse(string caisse, string nomCaisse, string localite)
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSAjoutCaisse";
                cmd.Parameters.AddWithValue("@caisse", caisse);
                cmd.Parameters.AddWithValue("@NomCaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@localite", localite);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }
        //--------------------------------Liste de la caisse-----------------------------------
        public DataTable ListeCaisse()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSListeCaisse";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];
        }
        //--------------------------------Ajout Suivi caisse 1-----------------------------------
        public int AjoutSuiviCaisses(decimal encaisse, decimal reserve, decimal liquidite, 
            decimal nbre1, decimal montant1,/* decimal nbre2, decimal montant2, decimal nbre3, decimal montant3,
            decimal nbre4, decimal capital, decimal interet,*/ decimal nbre5, decimal montant4, decimal montant5, decimal nbre6,
            decimal volume, decimal montantG, decimal nbre7, decimal montant6, decimal nbre8, decimal montant7, decimal nbre9, decimal montant8,
            decimal nbre10, decimal montant9, decimal frais1, decimal nbre11, decimal montant10, 
            decimal nbre12, decimal frais2, decimal nbre13, decimal frais3, string idcaisse, DateTime datesuivi,double PAR)
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSAjoutSuivi";
                cmd.Parameters.AddWithValue("@encaisse", encaisse); cmd.Parameters.AddWithValue("@reserve", reserve);
                cmd.Parameters.AddWithValue("@liquidite", liquidite);
               cmd.Parameters.AddWithValue("@nbre1", nbre1); cmd.Parameters.AddWithValue("@montant1", montant1);
                 /*cmd.Parameters.AddWithValue("@nbre2", nbre2); cmd.Parameters.AddWithValue("@montant2", montant2);
                cmd.Parameters.AddWithValue("@nbre3", nbre3); cmd.Parameters.AddWithValue("@montant3", montant3);
                cmd.Parameters.AddWithValue("@nbre4", nbre4); cmd.Parameters.AddWithValue("@capital", capital);
                cmd.Parameters.AddWithValue("@interet", interet);*/ cmd.Parameters.AddWithValue("@nbre5", nbre5);
                cmd.Parameters.AddWithValue("@montant4", montant4); cmd.Parameters.AddWithValue("@montant5", montant5);
                cmd.Parameters.AddWithValue("@nbre6", nbre6); cmd.Parameters.AddWithValue("@volume", volume);
                cmd.Parameters.AddWithValue("@montantG", montantG); cmd.Parameters.AddWithValue("@nbre7", nbre7);
                cmd.Parameters.AddWithValue("@montant6", montant6); cmd.Parameters.AddWithValue("@nbre8", nbre8);
                cmd.Parameters.AddWithValue("@montant7", montant7); cmd.Parameters.AddWithValue("@nbre9", nbre9);
                cmd.Parameters.AddWithValue("@montant8", montant8); cmd.Parameters.AddWithValue("@nbre10", nbre10);
                cmd.Parameters.AddWithValue("@montant9", montant9); cmd.Parameters.AddWithValue("@frais1", frais1);
                cmd.Parameters.AddWithValue("@nbre11", nbre11); cmd.Parameters.AddWithValue("@montant10", montant10);

                cmd.Parameters.AddWithValue("@nbre12", nbre12); cmd.Parameters.AddWithValue("@frais2", frais2);
                cmd.Parameters.AddWithValue("@nbre13", nbre13); cmd.Parameters.AddWithValue("@frais3", frais3);
                cmd.Parameters.AddWithValue("@PAR", PAR);


                cmd.Parameters.AddWithValue("@id", idcaisse);
                cmd.Parameters.AddWithValue("@datesuivi", datesuivi);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }
        //--------------------------------Liste Suivi Caisse-----------------------------------
        public DataTable ListeSuiviCaisses()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSListeSuiviCaisses";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];
        }
        //Requete parametrée pour le nom de la caisse et la date de suivi--------------------------------------------

        public DataTable NomCaisse_DateSuivi(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSParamCaisse";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        // Requete sur le pret soldé--------------------------------------------

        public DataTable PretSolde(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PretSolde";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Requete sur le remboursement effectué--------------------------------------------

        public DataTable RemboursementEff(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RemboursementEffectue";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Requete sur le remboursement attendu--------------------------------------------

        public DataTable RemboursementAttendu(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RemboursementAttendu";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Requete sur le Credit Retard--------------------------------------------

        public DataTable CreditRetard(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CrediRetard";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Requete sur le Volume epargne--------------------------------------------

        public DataTable VolumeEpargne(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "VolumeEpargne";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        // Requete sur l'encour de credit--------------------------------------------

        public DataTable EncourCredit(string nomCaisse, DateTime dateSuivi)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EncourCredit";
                cmd.Parameters.AddWithValue("@Nomcaisse", nomCaisse);
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //Requete parametrée pour la date de suivi--------------------------------------------

        public DataTable DateSuivi(DateTime dateSuivi, DateTime dateSuivi1, string nomcaisse)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DateSuivi";
                cmd.Parameters.AddWithValue("@date", dateSuivi);
                cmd.Parameters.AddWithValue("@date1", dateSuivi1);
                cmd.Parameters.AddWithValue("@nomcaisse", @nomcaisse);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        //=============Nombre de caisses
        public DataTable NombreCaisse()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "NombreCaisse";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        //=============Nombre d'utilisateur
        public DataTable NombreUser()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "NombreUser";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        //=============Nombre d'importation
        public DataTable NombreImportation()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "NombreImportation";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //=================================== Methode de convertion d'image ===========================================

        public byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        public Image ByteArrayToImage(byte[] byteImg)
        {
            using (MemoryStream ms = new MemoryStream(byteImg))
            {
                return Image.FromStream(ms);
            }
        }

        //--------------------------------Ajout de l'utilisateur-----------------------------------
        public int AjoutUtilisateur(string nom, string prenom, string fonction, string pseudo, string password, int type, byte[] photo)
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AjoutUtilisateur";
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@fonction", fonction);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idtype", type);
                if (photo == null)
                {
                    cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@photo", photo);
                }
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }
        //--------------------------------Liste Type utilisateur-----------------------------------
        public DataTable ListeTypeUtilisateur()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeType";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];
        }
        //--------------------------------Liste Type utilisateur de la base-----------------------------------
        public DataTable ListeUtilisateur()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeUser";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];
        }
        //--------------------------------Connexion au menu general--------------------------

        public DataTable Connexion(string login, string password)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Utilisateur Where Pseudo=@login AND Password=@password", cnx);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
        //-------------------------Encryptage du mot de passe---------------------
        public string md5Cryptage(string valeur)
        {
            StringBuilder sbuilder = new StringBuilder();

            using (MD5 md5 = MD5.Create())
            {
                //Cryptage de la valeur apres sa conversion en tableau de byte
                byte[] encValue = md5.ComputeHash(Encoding.UTF8.GetBytes(valeur));
                //Creer un string builder pour ecrire les chaines de caractere formaté en hexa par bloc
                for (int i = 0; i < encValue.Length; i++)
                {
                    //On converti la valeur de chaque position du tableau
                    //en hexa decimal pour l'ajouter ds le string builder
                    sbuilder.Append(encValue[i].ToString("x2"));
                }
            }
            return sbuilder.ToString();
        }


        // Resultat Total liquidite--------------------------------------------

        public DataTable ResultatLiquidite(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalLiquidite";
                cmd.Parameters.AddWithValue("@Du", du);
                cmd.Parameters.AddWithValue("@Au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Resultat Total liquidite--------------------------------------------

        public DataTable ResultatPretRembours(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalPretRembourser";
                cmd.Parameters.AddWithValue("@Du", du);
                cmd.Parameters.AddWithValue("@Au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //----------------------------------------------------------PARTIE BILAN ET EVALUATION FINANCIERE-------------------------------------------
        //AJOUT BILAN ACTIF
        //--------------------------------Ajout Bilan actif-----------------------------------
        public int AjoutBilanActif(
       string idCaisse, DateTime DateBilan, decimal OpInstituFinanceMBrut, decimal Op_Institu_Finance_Amortis, decimal Op_Institu_Finance_Montant, decimal OpInstituFinanceExercice,
  decimal Encaisse_Montant_Brut, decimal Encaisse_Amortis, decimal Encaisse_MontantNet, decimal Encaisse_Exercice, decimal Etabli_financier_Brut, decimal Etabli_financier_Amortis,
   decimal Etabli_financier_MontantNet, decimal Etabli_financier_Exercice, decimal Fond_garantie_MBrut, decimal Fond_garantie_Amortis, decimal Fond_garantie_MontantNet, decimal Fond_garantie_Exercice,
    decimal Op_Avec_Membre_MBrut, decimal Op_Avec_Membre_Amortis, decimal Op_Avec_Membre_MontantNet, decimal Op_Avec_Membre_Exercice, decimal Credit_Sain_MBrut, decimal Credit_Sain_Amortis,
    decimal Credit_Sain_MontantNet, decimal Credit_Sain_Exercice, decimal Creance_Rattache_MBrut, decimal Creance_Rattache_Amortis, decimal Creance_Rattache_MontantNet, decimal Creance_Rattache_Exercice,
     decimal Créance_En_Souffrance_MBrut, decimal Créance_En_Souffrance_Amortis, decimal Créance_En_Souffrance_MontantNet, decimal Créance_En_Souffrance_Exercice, decimal Op_Diverse_MBrut, decimal Op_Diverse_Amortis,
    decimal Op_Diverse_MontantNet, decimal Op_Diverse_Exercice, decimal Stock_Montant_Brut, decimal Stock_Montant_Amortis, decimal Stock_Montant_MontantNet, decimal Stock_Montant_Exercice, decimal Debiteur_Diver_MBrut,
     decimal Debiteur_Diver_Amortis, decimal Debiteur_Diver_MontantNet, decimal Debiteur_Diver_Exercice, decimal Compte_Regulier_Actif_MBrut, decimal Compte_Regulier_Actif_Amortis, decimal Compte_Regulier_Actif_MontantNet,
    decimal Compte_Regulier_Actif_Exercice, decimal Compte_Attente_Actif_MBrut, decimal Compte_Attente_Actif_Amortis, decimal Compte_Attente_Actif_MontantNet, decimal Compte_Attente_Actif_Exercice,
	decimal Depot_Cautionnement_MBrut,decimal Depot_Cautionnement_Amort , decimal Depot_Cautionnement_MontantNet,decimal Depot_Cautionnement_Exercice, 
            decimal Autre_MBrut,decimal Autre_Amortis, decimal Autre_MontantNet,decimal Autre_Exercice, 
            decimal Compte_Liaison_MBrut, decimal Compte_Liaison_Amortis, decimal Compte_Liaison_MontantNet, decimal Compte_Liaison_Exercice,
    decimal Immobilisation_MBrut, decimal Immobilisation_Amortis, decimal Immobilisation_MontantNet, decimal Immobilisation_Exercice, decimal Titre_Participation_MontantBrut, decimal Titre_Participation_Amortis,
    decimal Titre_Participation_MontantNet, decimal Titre_Participation_Exercice, decimal Immobilisation_Corporelle_MBrut, decimal Immobilisation_Corporelle_Amortis, decimal Immobilisation_Corporelle_MontantNet,
    decimal Immobilisation_Corporelle_Exercice, decimal Immobilisation_Incorporelle_MBrut, decimal Immobilisation_Incorporelle_Amortis, decimal Immobilisation_Incorporelle_MontantNet, decimal Immobilisation_Incorporelle_Exercice,
    decimal Immobilisation_Financiere_MBrut, decimal Immobilisation_Financiere_Amortis, decimal Immobilisation_Financiere_MontantNet, decimal Immobilisation_Financiere_Exercice, decimal Total_MontantBrut,
    decimal Total_Amortissement, decimal Total_MontantNet, decimal Total_Exercice
            )
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AjoutBilans";
                cmd.Parameters.AddWithValue("@idCaisse", idCaisse); cmd.Parameters.AddWithValue("@DateBilan", DateBilan);
                cmd.Parameters.AddWithValue("@OpInstituFinanceMBrut", OpInstituFinanceMBrut);
                cmd.Parameters.AddWithValue("@Op_Institu_Finance_Amortis", Op_Institu_Finance_Amortis); cmd.Parameters.AddWithValue("@OpInstituFinanceExercice", OpInstituFinanceExercice);
                cmd.Parameters.AddWithValue("@Op_Institu_Finance_Montant", Op_Institu_Finance_Montant); cmd.Parameters.AddWithValue("@Encaisse_Montant_Brut", Encaisse_Montant_Brut);
                cmd.Parameters.AddWithValue("@Encaisse_Amortis", Encaisse_Amortis); cmd.Parameters.AddWithValue("@Encaisse_MontantNet", Encaisse_MontantNet);
                cmd.Parameters.AddWithValue("@Encaisse_Exercice", Encaisse_Exercice); cmd.Parameters.AddWithValue("@Etabli_financier_Brut", Etabli_financier_Brut);
                cmd.Parameters.AddWithValue("@Etabli_financier_Amortis", Etabli_financier_Amortis); cmd.Parameters.AddWithValue("@Etabli_financier_MontantNet", Etabli_financier_MontantNet);
                cmd.Parameters.AddWithValue("@Etabli_financier_Exercice", Etabli_financier_Exercice); cmd.Parameters.AddWithValue("@Fond_garantie_MBrut", Fond_garantie_MBrut);
                cmd.Parameters.AddWithValue("@Fond_garantie_Amortis", Fond_garantie_Amortis); cmd.Parameters.AddWithValue("@Fond_garantie_MontantNet", Fond_garantie_MontantNet);
                cmd.Parameters.AddWithValue("@Fond_garantie_Exercice", Fond_garantie_Exercice); cmd.Parameters.AddWithValue("@Op_Avec_Membre_MBrut", Op_Avec_Membre_MBrut);
                cmd.Parameters.AddWithValue("@Op_Avec_Membre_Amortis", Op_Avec_Membre_Amortis); cmd.Parameters.AddWithValue("@Op_Avec_Membre_MontantNet", Op_Avec_Membre_MontantNet);
                cmd.Parameters.AddWithValue("@Op_Avec_Membre_Exercice", Op_Avec_Membre_Exercice); cmd.Parameters.AddWithValue("@Credit_Sain_MBrut", Credit_Sain_MBrut);
                cmd.Parameters.AddWithValue("@Credit_Sain_Amortis", Credit_Sain_Amortis); cmd.Parameters.AddWithValue("@Creance_Rattache_MBrut", Creance_Rattache_MBrut);
                cmd.Parameters.AddWithValue("@Credit_Sain_MontantNet", Credit_Sain_MontantNet); cmd.Parameters.AddWithValue("@Credit_Sain_Exercice", Credit_Sain_Exercice);
                cmd.Parameters.AddWithValue("@Creance_Rattache_Amortis", Creance_Rattache_Amortis); cmd.Parameters.AddWithValue("@Creance_Rattache_MontantNet", Creance_Rattache_MontantNet);
                cmd.Parameters.AddWithValue("@Creance_Rattache_Exercice", Creance_Rattache_Exercice); cmd.Parameters.AddWithValue("@Créance_En_Souffrance_MBrut", Créance_En_Souffrance_MBrut);
                cmd.Parameters.AddWithValue("@Créance_En_Souffrance_Amortis", Créance_En_Souffrance_Amortis); cmd.Parameters.AddWithValue("@Créance_En_Souffrance_MontantNet", Créance_En_Souffrance_MontantNet);
                cmd.Parameters.AddWithValue("@Créance_En_Souffrance_Exercice", Créance_En_Souffrance_Exercice);
                cmd.Parameters.AddWithValue("@Op_Diverse_MBrut", Op_Diverse_MBrut);
                cmd.Parameters.AddWithValue("@Op_Diverse_Amortis", Op_Diverse_Amortis);
                cmd.Parameters.AddWithValue("@Op_Diverse_MontantNet", Op_Diverse_MontantNet); cmd.Parameters.AddWithValue("@Op_Diverse_Exercice", Op_Diverse_Exercice);
                cmd.Parameters.AddWithValue("@Stock_Montant_Brut", Stock_Montant_Brut); cmd.Parameters.AddWithValue("@Stock_Montant_Amortis", Stock_Montant_Amortis);
                cmd.Parameters.AddWithValue("@Stock_Montant_MontantNet", Stock_Montant_MontantNet); cmd.Parameters.AddWithValue("@Stock_Montant_Exercice", Stock_Montant_Exercice);
                cmd.Parameters.AddWithValue("@Debiteur_Diver_MBrut", Debiteur_Diver_MBrut); cmd.Parameters.AddWithValue("@Debiteur_Diver_Amortis", Debiteur_Diver_Amortis);
                cmd.Parameters.AddWithValue("@Debiteur_Diver_MontantNet", Debiteur_Diver_MontantNet); cmd.Parameters.AddWithValue("@Debiteur_Diver_Exercice", Debiteur_Diver_Exercice);
                cmd.Parameters.AddWithValue("@Compte_Regulier_Actif_MBrut", Compte_Regulier_Actif_MBrut); cmd.Parameters.AddWithValue("@Compte_Regulier_Actif_Amortis", Compte_Regulier_Actif_Amortis);
                cmd.Parameters.AddWithValue("@Compte_Regulier_Actif_MontantNet", Compte_Regulier_Actif_MontantNet); cmd.Parameters.AddWithValue("@Compte_Regulier_Actif_Exercice", Compte_Regulier_Actif_Exercice);
                cmd.Parameters.AddWithValue("@Compte_Attente_Actif_MBrut", Compte_Attente_Actif_MBrut); cmd.Parameters.AddWithValue("@Compte_Attente_Actif_Amortis", Compte_Attente_Actif_Amortis);
                cmd.Parameters.AddWithValue("@Compte_Attente_Actif_MontantNet", Compte_Attente_Actif_MontantNet); cmd.Parameters.AddWithValue("@Compte_Attente_Actif_Exercice", Compte_Attente_Actif_Exercice);

                cmd.Parameters.AddWithValue("@Depot_Cautionnement_MBrut", Depot_Cautionnement_MBrut); cmd.Parameters.AddWithValue("@Depot_Cautionnement_Amort", Depot_Cautionnement_Amort);
                cmd.Parameters.AddWithValue("@Depot_Cautionnement_MontantNet", Depot_Cautionnement_MontantNet); cmd.Parameters.AddWithValue("@Depot_Cautionnement_Exercice", Depot_Cautionnement_Exercice);
               
                cmd.Parameters.AddWithValue("@Autre_MBrut",Autre_MBrut ); cmd.Parameters.AddWithValue("@Autre_Amortis",Autre_Amortis );
                cmd.Parameters.AddWithValue("@Autre_MontantNet", Autre_MontantNet); cmd.Parameters.AddWithValue("@Autre_Exercice", Autre_Exercice);
                cmd.Parameters.AddWithValue("@Compte_Liaison_MBrut", Compte_Liaison_MBrut); cmd.Parameters.AddWithValue("@Compte_Liaison_Amortis", Compte_Liaison_Amortis);
                cmd.Parameters.AddWithValue("@Compte_Liaison_MontantNet", Compte_Liaison_MontantNet); cmd.Parameters.AddWithValue("@Compte_Liaison_Exercice", Compte_Liaison_Exercice);

                cmd.Parameters.AddWithValue("@Immobilisation_MBrut", Immobilisation_MBrut); cmd.Parameters.AddWithValue("@Immobilisation_Amortis", Immobilisation_Amortis);
                cmd.Parameters.AddWithValue("@Immobilisation_MontantNet", Immobilisation_MontantNet); cmd.Parameters.AddWithValue("@Immobilisation_Exercice", Immobilisation_Exercice);
                cmd.Parameters.AddWithValue("@Titre_Participation_MontantBrut", Titre_Participation_MontantBrut); cmd.Parameters.AddWithValue("@Titre_Participation_Amortis", Titre_Participation_Amortis);
                cmd.Parameters.AddWithValue("@Titre_Participation_MontantNet", Titre_Participation_MontantNet); cmd.Parameters.AddWithValue("@Titre_Participation_Exercice", Titre_Participation_Exercice);
                cmd.Parameters.AddWithValue("@Immobilisation_Corporelle_MBrut", Immobilisation_Corporelle_MBrut); cmd.Parameters.AddWithValue("@Immobilisation_Corporelle_Amortis", Immobilisation_Corporelle_Amortis);
                cmd.Parameters.AddWithValue("@Immobilisation_Corporelle_MontantNet", Immobilisation_Corporelle_MontantNet); cmd.Parameters.AddWithValue("@Immobilisation_Corporelle_Exercice", Immobilisation_Corporelle_Exercice);
                cmd.Parameters.AddWithValue("@Immobilisation_Incorporelle_MBrut", Immobilisation_Incorporelle_MBrut); cmd.Parameters.AddWithValue("@Immobilisation_Incorporelle_Amortis", Immobilisation_Incorporelle_Amortis);
                cmd.Parameters.AddWithValue("@Immobilisation_Incorporelle_MontantNet", Immobilisation_Incorporelle_MontantNet); cmd.Parameters.AddWithValue("@Immobilisation_Incorporelle_Exercice", Immobilisation_Incorporelle_Exercice);
                cmd.Parameters.AddWithValue("@Immobilisation_Financiere_MBrut", Immobilisation_Financiere_MBrut); cmd.Parameters.AddWithValue("@Immobilisation_Financiere_Amortis", Immobilisation_Financiere_Amortis);
                cmd.Parameters.AddWithValue("@Immobilisation_Financiere_MontantNet", Immobilisation_Financiere_MontantNet); cmd.Parameters.AddWithValue("@Immobilisation_Financiere_Exercice", Immobilisation_Financiere_Exercice);
                cmd.Parameters.AddWithValue("@Total_MontantBrut", Total_MontantBrut); cmd.Parameters.AddWithValue("@Total_Amortissement", Total_Amortissement);
                cmd.Parameters.AddWithValue("@Total_MontantNet", Total_MontantNet); cmd.Parameters.AddWithValue("@Total_Exercice", Total_Exercice);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }




        //AJOUT BILAN ACTIF
        //--------------------------------Ajout Suivi ACTIF-----------------------------------
        public int AjoutBilanPassif(
         string idCaisse, DateTime DateBilan_Passif, decimal Op_Avec_Membre_Mbrut, decimal Op_Avec_Membre_Exercice,
        decimal Epargne_a_Vue_MBrut, decimal Epargne_a_Vue_Exercice, decimal Epargne_a_Terme_Unique_MBrut, decimal Epargne_a_Terme_Unique_Exercice,
         decimal Depot_periodique_MBrut, decimal Depot_periodique_Exercice, decimal Epargne_Stable_MBrut, decimal Epargne_Stable_Exercice, decimal Epargne_bloque_Nantie_MBrut,
         decimal Epargne_bloque_Nantie_Exercice, decimal Dette_Rattache_MBrut, decimal Dette_Rattache_Exercice, decimal Compte_inactif_MBrut, decimal Compte_inactif_Exercice,
         decimal Op_Diverse_MBrut, decimal Op_Diverse_Exercice, decimal Crediteur_divers_MBrut, decimal Crediteur_divers_Exercice, decimal Autre_Crediteur_MBrut,
          decimal Autre_Crediteur_Exercice, decimal Compte_Regularisation_Passif_MBrut, decimal Compte_Regularisation_Passif_Exercice, decimal Compte_Attente_Passif_MBrut,
         decimal Compte_Attente_Passif_Exercice, decimal Source_Fond_Exterieur_MBrut, decimal Source_Fond_Exterieur_Exercice, decimal Fond_Garantie_MBrut, decimal Fond_Garantie_Exercice,
          decimal Ligne_Credit_MBrut, decimal Ligne_Credit_Exercice, decimal Emprunt_MBrut, decimal Emprunt_Exercice, decimal Provision_MBrut, decimal Provision_Exercice, decimal Subvention_Recu_MBrut,
          decimal Subvention_Recu_Exercice, decimal Provisions_Pour_Risque_MBrut, decimal Provisions_Pour_Risque_Exercice, decimal Report_a_Nouveau_MBrut, decimal Report_a_Nouveau_Exercice,
          decimal Reserve_MBrut, decimal Reserve_Exercice, decimal Parts_Sociale_MBrut, decimal Parts_Sociale_Exercice, decimal Resultat_Attente_Affectation_MBrut, decimal Resultat_Attente_Affectation_Exercice,
          decimal Excedent_MBrut, decimal Excedent_Exercice, decimal Total_MBrut, decimal Total_Exercice
            )
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AjoutBilanPassif";
                cmd.Parameters.AddWithValue("@idCaisse", idCaisse); cmd.Parameters.AddWithValue("@DateBilan_Passif", DateBilan_Passif);
                cmd.Parameters.AddWithValue("@Op_Avec_Membre_Mbrut", Op_Avec_Membre_Mbrut);
                cmd.Parameters.AddWithValue("@Op_Avec_Membre_Exercice", Op_Avec_Membre_Exercice); cmd.Parameters.AddWithValue("@Epargne_a_Vue_MBrut", Epargne_a_Vue_MBrut);
                cmd.Parameters.AddWithValue("@Epargne_a_Vue_Exercice", Epargne_a_Vue_Exercice); cmd.Parameters.AddWithValue("@Epargne_a_Terme_Unique_MBrut", Epargne_a_Terme_Unique_MBrut);
                cmd.Parameters.AddWithValue("@Epargne_a_Terme_Unique_Exercice", Epargne_a_Terme_Unique_Exercice); cmd.Parameters.AddWithValue("@Depot_periodique_MBrut", Depot_periodique_MBrut);
                cmd.Parameters.AddWithValue("@Depot_periodique_Exercice", Depot_periodique_Exercice); cmd.Parameters.AddWithValue("@Epargne_Stable_MBrut", Epargne_Stable_MBrut);
                cmd.Parameters.AddWithValue("@Epargne_Stable_Exercice", Epargne_Stable_Exercice); cmd.Parameters.AddWithValue("@Epargne_bloque_Nantie_MBrut", Epargne_bloque_Nantie_MBrut);
                cmd.Parameters.AddWithValue("@Epargne_bloque_Nantie_Exercice", Epargne_bloque_Nantie_Exercice); cmd.Parameters.AddWithValue("@Dette_Rattache_MBrut", Dette_Rattache_MBrut);
                cmd.Parameters.AddWithValue("@Dette_Rattache_Exercice", Dette_Rattache_Exercice); cmd.Parameters.AddWithValue("@Compte_inactif_MBrut", Compte_inactif_MBrut);
                cmd.Parameters.AddWithValue("@Compte_inactif_Exercice", Compte_inactif_Exercice); cmd.Parameters.AddWithValue("@Op_Diverse_MBrut", Op_Diverse_MBrut);
                cmd.Parameters.AddWithValue("@Op_Diverse_Exercice", Op_Diverse_Exercice); cmd.Parameters.AddWithValue("@Crediteur_divers_MBrut", Crediteur_divers_MBrut);
                cmd.Parameters.AddWithValue("@Source_Fond_Exterieur_Exercice", Source_Fond_Exterieur_Exercice);
                cmd.Parameters.AddWithValue("@Autre_Crediteur_MBrut", Autre_Crediteur_MBrut);
                cmd.Parameters.AddWithValue("@Crediteur_divers_Exercice", Crediteur_divers_Exercice); cmd.Parameters.AddWithValue("@Autre_Crediteur_Exercice", Autre_Crediteur_Exercice);
                cmd.Parameters.AddWithValue("@Compte_Regularisation_Passif_MBrut", Compte_Regularisation_Passif_MBrut); cmd.Parameters.AddWithValue("@Compte_Regularisation_Passif_Exercice", Compte_Regularisation_Passif_Exercice);
                cmd.Parameters.AddWithValue("@Compte_Attente_Passif_MBrut", Compte_Attente_Passif_MBrut); cmd.Parameters.AddWithValue("@Compte_Attente_Passif_Exercice", Compte_Attente_Passif_Exercice);
                cmd.Parameters.AddWithValue("@Source_Fond_Exterieur_MBrut", Source_Fond_Exterieur_MBrut); cmd.Parameters.AddWithValue("@Fond_Garantie_Exercice", Fond_Garantie_Exercice);
                cmd.Parameters.AddWithValue("@Fond_Garantie_MBrut", Fond_Garantie_MBrut); cmd.Parameters.AddWithValue("@Ligne_Credit_Exercice", Ligne_Credit_Exercice);
                cmd.Parameters.AddWithValue("@Ligne_Credit_MBrut", Ligne_Credit_MBrut);
                cmd.Parameters.AddWithValue("@Emprunt_MBrut", Emprunt_MBrut);
                cmd.Parameters.AddWithValue("@Emprunt_Exercice", Emprunt_Exercice);
                cmd.Parameters.AddWithValue("@Provision_MBrut", Provision_MBrut); cmd.Parameters.AddWithValue("@Provision_Exercice", Provision_Exercice);
                cmd.Parameters.AddWithValue("@Subvention_Recu_MBrut", Subvention_Recu_MBrut); cmd.Parameters.AddWithValue("@Subvention_Recu_Exercice", Subvention_Recu_Exercice);
                cmd.Parameters.AddWithValue("@Provisions_Pour_Risque_MBrut", Provisions_Pour_Risque_MBrut); cmd.Parameters.AddWithValue("@Provisions_Pour_Risque_Exercice", Provisions_Pour_Risque_Exercice);
                cmd.Parameters.AddWithValue("@Report_a_Nouveau_MBrut", Report_a_Nouveau_MBrut); cmd.Parameters.AddWithValue("@Report_a_Nouveau_Exercice", Report_a_Nouveau_Exercice);
                cmd.Parameters.AddWithValue("@Reserve_MBrut", Reserve_MBrut); cmd.Parameters.AddWithValue("@Reserve_Exercice", Reserve_Exercice);
                cmd.Parameters.AddWithValue("@Parts_Sociale_MBrut", Parts_Sociale_MBrut); cmd.Parameters.AddWithValue("@Parts_Sociale_Exercice", Parts_Sociale_Exercice);
                cmd.Parameters.AddWithValue("@Resultat_Attente_Affectation_MBrut", Resultat_Attente_Affectation_MBrut); cmd.Parameters.AddWithValue("@Resultat_Attente_Affectation_Exercice", Resultat_Attente_Affectation_Exercice);
                cmd.Parameters.AddWithValue("@Excedent_MBrut", Excedent_MBrut); cmd.Parameters.AddWithValue("@Excedent_Exercice", Excedent_Exercice);
                cmd.Parameters.AddWithValue("@Total_MBrut", Total_MBrut); cmd.Parameters.AddWithValue("@Total_Exercice", Total_Exercice);

                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }

        //AJOUT ETAT CHARGE
        //--------------------------------Ajout Etat Charge-----------------------------------
        public int AjoutEtatCharge(string idCaisse, DateTime DateEtatCharge, decimal Charge_Financiere_MBrut, decimal Charge_Financiere_Exercice, decimal Interet_Verse_MBrut, decimal Interet_Verse_Exercice,
         decimal Autre_Charge_Financiere_MBrut, decimal Autre_Charge_Financiere_Exercice, decimal Achat_Service_Exterieur_MBrut, decimal Achat_Service_Exterieur_Exercice, decimal Achat_MBrut, decimal Achat_Exercice,
         decimal Eau_Electricite_MBrut, decimal Eau_Electricite_Exercice, decimal Loyer_MBrut, decimal Loyer_Exercice, decimal Entretien_Reparation_MBrut, decimal Entretien_Reparation_Exercice, decimal Prime_Assurance_MBrut,
            decimal Prime_Assurance_Exercice,
         decimal Autre_Service_MBrut, decimal Autre_Service_Exercice, decimal Publicite_Relation_publique_MBrut, decimal Publicite_Relation_publique_Exercice, decimal Transport_deplacement_MBrut, decimal Transport_deplacement_Exercice,
            decimal Frais_Postaux_Telecommunication_MBrut,
         decimal Frais_Postaux_Telecommunication_Exercice, decimal Frais_Gestion_Credit_Entreco_MBrut, decimal Frais_Gestion_Credit_Entreco_Exercice, decimal Frais_Formation_MBrut, decimal Frais_Formation_Exercice,

            decimal Frais_Tenue_MBrut, decimal Frais_Tenue_Exercice, decimal Frais_Gardiennage_MBrut, decimal Frais_Gardiennage_Exercice, decimal Divers_MBrut, decimal Divers_Exercice, decimal Charge_Personne_MBrut,
            decimal Charge_Personne_Exercice, decimal Charges_Salariales_MBrut, decimal Charges_Salariales_Exercice,
         decimal Charges_Sociale_MBrut, decimal Charges_Sociale_Exercice, decimal Frais_Remplacement_MBrut, decimal Frais_Remplacement_Exercice, decimal Assurance_Maladie_MBrut, 
            decimal Assurance_Maladie_Exercice,
            decimal Bien_Etre_Employe_MBrut, decimal Bien_Etre_Employe_Exercice, decimal Autre_Charge_MBrut,
         decimal Autre_Charge_Exercice, decimal Dotation_Amortissement_MBrut, decimal Dotation_Amortissement_Exercice, decimal Charge_Exceptionnelle_MBrut, decimal Charge_Exceptionnelle_Exercice, decimal Impôt_Taxe_MBrut,
            decimal Impôt_Taxe_Exercice,
         decimal Total_Charge_MBrut, decimal Total_Charge_Exercice
            )
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AjoutEtatCharge";
                cmd.Parameters.AddWithValue("@idCaisse", idCaisse); cmd.Parameters.AddWithValue("@DateEtatCharge", DateEtatCharge);
                cmd.Parameters.AddWithValue("@Charge_Financiere_MBrut", Charge_Financiere_MBrut);
                cmd.Parameters.AddWithValue("@Charge_Financiere_Exercice", Charge_Financiere_Exercice); cmd.Parameters.AddWithValue("@Interet_Verse_MBrut", Interet_Verse_MBrut);
                cmd.Parameters.AddWithValue("@Interet_Verse_Exercice", Interet_Verse_Exercice); cmd.Parameters.AddWithValue("@Autre_Charge_Financiere_MBrut", Autre_Charge_Financiere_MBrut);
                cmd.Parameters.AddWithValue("@Autre_Charge_Financiere_Exercice", Autre_Charge_Financiere_Exercice); cmd.Parameters.AddWithValue("@Achat_Service_Exterieur_MBrut", Achat_Service_Exterieur_MBrut);
                cmd.Parameters.AddWithValue("@Achat_Service_Exterieur_Exercice", Achat_Service_Exterieur_Exercice); cmd.Parameters.AddWithValue("@Achat_MBrut", Achat_MBrut);
                cmd.Parameters.AddWithValue("@Achat_Exercice", Achat_Exercice); cmd.Parameters.AddWithValue("@Eau_Electricite_MBrut", Eau_Electricite_MBrut);
                cmd.Parameters.AddWithValue("@Eau_Electricite_Exercice", Eau_Electricite_Exercice); cmd.Parameters.AddWithValue("@Loyer_MBrut", Loyer_MBrut);
                cmd.Parameters.AddWithValue("@Loyer_Exercice", Loyer_Exercice); cmd.Parameters.AddWithValue("@Entretien_Reparation_MBrut", Entretien_Reparation_MBrut);
                cmd.Parameters.AddWithValue("@Entretien_Reparation_Exercice", Entretien_Reparation_Exercice); cmd.Parameters.AddWithValue("@Prime_Assurance_MBrut", Prime_Assurance_MBrut);
                cmd.Parameters.AddWithValue("@Prime_Assurance_Exercice", Prime_Assurance_Exercice); cmd.Parameters.AddWithValue("@Autre_Service_MBrut", Autre_Service_MBrut);
                cmd.Parameters.AddWithValue("@Autre_Service_Exercice", Autre_Service_Exercice); cmd.Parameters.AddWithValue("@Publicite_Relation_publique_MBrut", Publicite_Relation_publique_MBrut);
                cmd.Parameters.AddWithValue("@Publicite_Relation_publique_Exercice", Publicite_Relation_publique_Exercice); cmd.Parameters.AddWithValue("@Transport_deplacement_MBrut", Transport_deplacement_MBrut);
                cmd.Parameters.AddWithValue("@Transport_deplacement_Exercice", Transport_deplacement_Exercice); cmd.Parameters.AddWithValue("@Frais_Postaux_Telecommunication_MBrut", Frais_Postaux_Telecommunication_MBrut);
                cmd.Parameters.AddWithValue("@Frais_Postaux_Telecommunication_Exercice", Frais_Postaux_Telecommunication_Exercice); cmd.Parameters.AddWithValue("@Frais_Gestion_Credit_Entreco_MBrut", Frais_Gestion_Credit_Entreco_MBrut);
                cmd.Parameters.AddWithValue("@Frais_Gestion_Credit_Entreco_Exercice", Frais_Gestion_Credit_Entreco_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Formation_MBrut", Frais_Formation_MBrut); cmd.Parameters.AddWithValue("@Frais_Formation_Exercice", Frais_Formation_Exercice);
               
                cmd.Parameters.AddWithValue("@Frais_Tenue_MBrut", Frais_Tenue_MBrut); cmd.Parameters.AddWithValue("@Frais_Tenue_Exercice", Frais_Tenue_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Gardiennage_MBrut", Frais_Gardiennage_MBrut); cmd.Parameters.AddWithValue("@Frais_Gardiennage_Exercice", Frais_Gardiennage_Exercice);
                cmd.Parameters.AddWithValue("@Divers_MBrut", Divers_MBrut); cmd.Parameters.AddWithValue("@Divers_Exercice", Divers_Exercice);
                cmd.Parameters.AddWithValue("@Charge_Personne_MBrut", Charge_Personne_MBrut); cmd.Parameters.AddWithValue("@Charge_Personne_Exercice", Charge_Personne_Exercice);
                cmd.Parameters.AddWithValue("@Charges_Salariales_MBrut", Charges_Salariales_MBrut); cmd.Parameters.AddWithValue("@Charges_Salariales_Exercice", Charges_Salariales_Exercice);
                cmd.Parameters.AddWithValue("@Charges_Sociale_MBrut", Charges_Sociale_MBrut); cmd.Parameters.AddWithValue("@Charges_Sociale_Exercice", Charges_Sociale_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Remplacement_MBrut", Frais_Remplacement_MBrut); cmd.Parameters.AddWithValue("@Frais_Remplacement_Exercice", Frais_Remplacement_Exercice);
                cmd.Parameters.AddWithValue("@Assurance_Maladie_MBrut", Assurance_Maladie_MBrut); cmd.Parameters.AddWithValue("@Assurance_Maladie_Exercice", Assurance_Maladie_Exercice);
                cmd.Parameters.AddWithValue("@Bien_Etre_Employe_MBrut", Bien_Etre_Employe_MBrut); cmd.Parameters.AddWithValue("@Bien_Etre_Employe_Exercice", Bien_Etre_Employe_Exercice);
                cmd.Parameters.AddWithValue("@Autre_Charge_MBrut", Autre_Charge_MBrut); cmd.Parameters.AddWithValue("@Autre_Charge_Exercice", Autre_Charge_Exercice);
                cmd.Parameters.AddWithValue("@Dotation_Amortissement_MBrut", Dotation_Amortissement_MBrut); cmd.Parameters.AddWithValue("@Dotation_Amortissement_Exercice", Dotation_Amortissement_Exercice);
                cmd.Parameters.AddWithValue("@Charge_Exceptionnelle_MBrut", Charge_Exceptionnelle_MBrut); cmd.Parameters.AddWithValue("@Charge_Exceptionnelle_Exercice", Charge_Exceptionnelle_Exercice);
                cmd.Parameters.AddWithValue("@Impôt_Taxe_MBrut", Impôt_Taxe_MBrut); cmd.Parameters.AddWithValue("@Impôt_Taxe_Exercice", Impôt_Taxe_Exercice);
                cmd.Parameters.AddWithValue("@Total_Charge_MBrut", Total_Charge_MBrut); cmd.Parameters.AddWithValue("@Total_Charge_Exercice", Total_Charge_Exercice);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }

        //AJOUT ETAT CHARGE
        //--------------------------------Ajout Etat Charge-----------------------------------
        public int AjoutEtatProduit(
            string idCaisse, DateTime DateEtatProduit, decimal Produit_Financier_MBrut, decimal Produit_Financier_Exercice, decimal Interet_Recu_MBrut, decimal Interet_Recu_Exercice,
            decimal Interet_Recu_etablissement_MBrut, decimal Interet_Recu_etablissement_Exercice,
         decimal Autre_Produit_Financier_MBrut, decimal Autre_Produit_Financier_Exercice, decimal Revenu_Frais_Service_MBrut, decimal Revenu_Frais_Service_Exercice, decimal Surplus_Caisse_MBrut, decimal Surplus_Caisse_Exercice,
         decimal Frais_Adhésion_MBrut, decimal Frais_Adhésion_Exercice, decimal Frais_Service_MBrut, decimal Frais_Service_Exercice, decimal Frais_Remplacement_Carnet_MBrut,
            decimal Frais_Remplacement_Carnet_Exercice, decimal Pénalite_Retard_MBrut,
         decimal Pénalite_Retard_Exercice, decimal Frais_Tenue_Compte_MBrut, decimal Frais_Tenue_Compte_Exercice, decimal Frais_Divers_MBrut, decimal Frais_Divers_Exercice,
            decimal Autre_Revenus_MBrut, decimal Autre_Revenus_Exercice, decimal Autre_Revenu_Un_MBrut,decimal Autre_Revenu_Un_Exercice ,decimal Produits_divers_MBrut,
         decimal Produits_divers_Exercice, decimal Produit_Exceptionnel_MBrut, decimal Produit_Exceptionnel_Exercice, decimal Reprise_Amortissement_MBrut,
            decimal Reprise_Amortissement_Exercice, decimal Total_Produit_MBrut, decimal Total_Produit_Exercice
        
            )
        {
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AjoutEtatProduit";
                cmd.Parameters.AddWithValue("@idCaisse", idCaisse); cmd.Parameters.AddWithValue("@DateEtatProduit", DateEtatProduit);
                cmd.Parameters.AddWithValue("@Produit_Financier_MBrut", Produit_Financier_MBrut);
                cmd.Parameters.AddWithValue("@Produit_Financier_Exercice", Produit_Financier_Exercice);
                cmd.Parameters.AddWithValue("@Interet_Recu_MBrut", Interet_Recu_MBrut); cmd.Parameters.AddWithValue("@Interet_Recu_Exercice", Interet_Recu_Exercice);
                cmd.Parameters.AddWithValue("@Interet_Recu_etablissement_MBrut", Interet_Recu_etablissement_MBrut); cmd.Parameters.AddWithValue("@Interet_Recu_etablissement_Exercice", Interet_Recu_etablissement_Exercice);
                cmd.Parameters.AddWithValue("@Autre_Produit_Financier_MBrut", Autre_Produit_Financier_MBrut); cmd.Parameters.AddWithValue("@Autre_Produit_Financier_Exercice", Autre_Produit_Financier_Exercice);
                cmd.Parameters.AddWithValue("@Revenu_Frais_Service_MBrut", Revenu_Frais_Service_MBrut); cmd.Parameters.AddWithValue("@Revenu_Frais_Service_Exercice", Revenu_Frais_Service_Exercice);
                cmd.Parameters.AddWithValue("@Surplus_Caisse_MBrut", Surplus_Caisse_MBrut); cmd.Parameters.AddWithValue("@Surplus_Caisse_Exercice", Surplus_Caisse_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Adhésion_MBrut", Frais_Adhésion_MBrut); cmd.Parameters.AddWithValue("@Frais_Adhésion_Exercice", Frais_Adhésion_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Service_MBrut", Frais_Service_MBrut); cmd.Parameters.AddWithValue("@Frais_Service_Exercice", Frais_Service_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Remplacement_Carnet_MBrut", Frais_Remplacement_Carnet_MBrut); cmd.Parameters.AddWithValue("@Frais_Remplacement_Carnet_Exercice", Frais_Remplacement_Carnet_Exercice);
                cmd.Parameters.AddWithValue("@Pénalite_Retard_MBrut", Pénalite_Retard_MBrut); cmd.Parameters.AddWithValue("@Pénalite_Retard_Exercice", Pénalite_Retard_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Tenue_Compte_MBrut", Frais_Tenue_Compte_MBrut); cmd.Parameters.AddWithValue("@Frais_Tenue_Compte_Exercice", Frais_Tenue_Compte_Exercice);
                cmd.Parameters.AddWithValue("@Frais_Divers_MBrut", Frais_Divers_MBrut); cmd.Parameters.AddWithValue("@Frais_Divers_Exercice", Frais_Divers_Exercice);
                cmd.Parameters.AddWithValue("@Autre_Revenus_MBrut", Autre_Revenus_MBrut); cmd.Parameters.AddWithValue("@Autre_Revenus_Exercice", Autre_Revenus_Exercice);
                cmd.Parameters.AddWithValue("@Autre_Revenu_Un_MBrut", Autre_Revenu_Un_MBrut); cmd.Parameters.AddWithValue("@Autre_Revenu_Un_Exercice", Autre_Revenu_Un_Exercice);
                cmd.Parameters.AddWithValue("@Produits_divers_MBrut", Produits_divers_MBrut); cmd.Parameters.AddWithValue("@Produits_divers_Exercice", Produits_divers_Exercice);
                cmd.Parameters.AddWithValue("@Produit_Exceptionnel_MBrut", Produit_Exceptionnel_MBrut); cmd.Parameters.AddWithValue("@Produit_Exceptionnel_Exercice", Produit_Exceptionnel_Exercice);
                cmd.Parameters.AddWithValue("@Reprise_Amortissement_MBrut", Reprise_Amortissement_MBrut); cmd.Parameters.AddWithValue("@Reprise_Amortissement_Exercice", Reprise_Amortissement_Exercice);
                cmd.Parameters.AddWithValue("@Total_Produit_MBrut", Total_Produit_MBrut); cmd.Parameters.AddWithValue("@Total_Produit_Exercice", Total_Produit_Exercice);
                d = cmd.ExecuteNonQuery();
                cnx.Close();
            }
            return d;
        }
        // Liste des bilans--------------------------------------------

        public DataTable ListeBilan()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeBilan";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // Requete sur le total des bilan actifs--------------------------------------------

        public DataTable ParaTotalBilan(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ParaTotalBilan";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        // Requete sur le total des bilan passifs--------------------------------------------

        public DataTable ParaTotalBilanPassif(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalBilanPassif";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        // -------------Total des charges
        public DataTable TotalCharge(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Totalcharge";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // -------------Total des produits
        public DataTable TotalProduit(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalProduit";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // -------------Total des Bilan passif
        public DataTable TotalBilanPassiff(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalBilanPassiff";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // -------------Total des états de produits
        public DataTable TotalEtatproduits(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalProduitss";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // -------------Total des états de charges
        public DataTable TotalEtatCharges(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TotalEtatCharge";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        // -------------Liste des bilans actifs
        public DataTable ListeBilanActif()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeBilanActif";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Nombre de bilan actif---------------------
        public DataTable NombreBilanActif()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "NombreBilanActif";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Nombre de bilan passif---------------------
        public DataTable NombreBilanPassif()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "nbreBilanPassif";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Nombre de charge---------------------
        public DataTable nombreCharge()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "nbreCharge";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Nombre de produits---------------------
        public DataTable nombreProduit()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "nbreProduit";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        //-----------------------------------Liste bilan caisse---------------------
        public DataTable ListeBilanCaiss(DateTime du, DateTime au,string caiss)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListeBilanCaisse";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);
                cmd.Parameters.AddWithValue("@caisse", caiss);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }

        //-----------------------------------Le rang pour les caisses ---------------------
        public DataTable RangCaisse(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Rang";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Le rang pour les caisses ---------------------
        public DataTable RangCaissePAR(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RangPAR";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
        //-----------------------------------Appreciation sur le PAR ---------------------
        public DataTable AppreciationPar(DateTime du, DateTime au)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnx = new SqlConnection(chaine))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PS_Observation";
                cmd.Parameters.AddWithValue("@du", du);
                cmd.Parameters.AddWithValue("@au", au);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cnx.Close();
            }
            return ds.Tables[0];

        }
    }
}
