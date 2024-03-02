using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading.Channels;

namespace canalsat.Models
{
    public class Abonnement
    {
        public String idClient { get; set; }

        public String offre { get; set; }

        public DateTime dateDebut { get; set; }

        public DateTime dateFin { get; set; }

        public String prix { get; set; }
        public Abonnement()
        {

        }

        public Abonnement(string idClient, string offre, DateTime dateDebut, DateTime dateFin, string prix)
        {
            this.idClient = idClient;
            this.offre = offre;
            this.dateDebut = dateDebut;
            this.dateFin = dateFin;
            this.prix = prix;
        }

        public String getOffre(String[] chaines, String bouquet)
        {
            String result = bouquet;
            for(int i=0; i<chaines.Length; i++)
            {
                result += ","+ chaines[i];
            }
            return result;
        }

        public String getOffreSpecifique(String[] chaines)
        {
            String result = chaines[0];
            if (chaines.Length>1)
            {
                for (int i = 1; i < chaines.Length; i++)
                {
                    result += "," + chaines[i];
                }
            }
            return result;
        }


        public String getPrix(SqlConnection co,String offre)
        {
            CompositionBouquet comp = new CompositionBouquet();
            Bouquet b = new Bouquet();
            Bouquet bouquet = null;
            String price = null;
            double prix = 0;
            if (offre.Contains(","))
            {
                String[] chaines = offre.Split(',');
                if (chaines[0].Contains("B"))
                {
                    /*bouquet = b.BouquetById(co, chaines[0]); //prix Bouquet, prixChaine1, prixChaine2, ...
                    prix = comp.prixBouquet(co, chaines[0]);
                    prix = prix - ((prix * bouquet.remise) / 100);
                    price = prix.ToString();
                    for (int i = 1; i < chaines.Length; i++)
                    {
                        Chaine c = new Chaine();
                        c = c.chaineById(co, chaines[i]);
                        price += "," + c.prix.ToString();
                    }*/

                    double prixChaine = 0; //prix Bouquet+Chaine
                    double remise = 0;
                    bouquet = b.BouquetById(co, chaines[0]); 
                    prix = comp.prixBouquet(co, chaines[0]);
                    prix = prix - ((prix * bouquet.remise) / 100);
                    for (int i = 1; i < chaines.Length; i++)
                    {
                        Chaine c = new Chaine();
                        c = c.chaineById(co, chaines[i]);
                        prixChaine += c.prix;
                    }
                    prixChaine = prixChaine - ((prixChaine * remise) / 100);
                    prix = prix + prixChaine;
                    price = prix.ToString();
                }
                else
                {
                    /*Chaine c = new Chaine();
                    c = c.chaineById(co, chaines[0]);
                    price = c.prix.ToString();
                    for (int i = 1; i < chaines.Length; i++)
                    {
                        c = c.chaineById(co, chaines[i]);
                        price += "," + c.prix.ToString();
                    }*/
                    Chaine c = new Chaine();
                    double remise = 0;
                    double prixSpecifique = 0;
                    for (int i = 0; i < chaines.Length; i++)
                    {
                        c = c.chaineById(co, chaines[i]);
                        prixSpecifique += c.prix;
                    }
                    prixSpecifique = prixSpecifique - ((prixSpecifique * remise) / 100);
                    price = prixSpecifique.ToString();
                }
               
            }
            else
            {
                bouquet = b.BouquetById(co, offre);
                prix = comp.prixBouquet(co, offre);
                prix = prix - ((prix * bouquet.remise) / 100);
                price = prix.ToString();
            }
            return price;
        }

        public Boolean insertionAbonnement(SqlConnection co, String idClient, String offre)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Boolean result = false;
            DateTime dateDebut = new DateTime();
            Abonnement abonnement = this.lastAbonnement(co, idClient);
            String lastBouquet = null;
            double prixLastAbonnement = 0;
            double prixActuel = 0;
            String priceToInsert = this.getPrix(co, offre);
            if (abonnement != null)
            {
                lastBouquet = abonnement.offre;
                /*  if(lastBouquet.Contains("B")) // Prix Ancien Bouquet
                  {
                      String lastPrice = this.getPrix(co, lastBouquet);
                      if (lastPrice.Contains(","))
                      {
                          string[] price = lastPrice.Split(",");
                          prixLastAbonnement = Double.Parse(price[0]);
                      }
                      else
                      {
                          prixLastAbonnement = Double.Parse(lastPrice);
                      }
                  }*/

                /* String lastPrice = this.getPrix(co, lastBouquet); //Prix derniere offre bouquet+chaine
                 if (lastPrice.Contains(","))
                 {
                     string[] price = lastPrice.Split(",");
                     for (int i = 0; i < price.Length; i++)
                     {
                         prixLastAbonnement += Double.Parse(price[i]);
                     }
                 }*/

                /*String lastPrice = this.getPrix(co, lastBouquet); //dernier offre total
                prixLastAbonnement += Double.Parse(lastPrice);*/

                if (lastBouquet.Contains(",")) //maka prix dernier bouquet
                {
                    if (lastBouquet.Contains("B"))
                    {
                        CompositionBouquet comp = new CompositionBouquet();
                        Bouquet bouquet = new Bouquet();
                        string[] last = lastBouquet.Split(",");
                        bouquet = bouquet.BouquetById(co, last[0]);
                        prixLastAbonnement = comp.prixBouquet(co, last[0]);
                        prixLastAbonnement = prixLastAbonnement - ((prixLastAbonnement * bouquet.remise) / 100);
                    }
                }
                else
                {
                    String lastPrice = this.getPrix(co, lastBouquet);
                    prixLastAbonnement += Double.Parse(lastPrice);
                }
            
                if (DateTime.Now < abonnement.dateFin)
                {
                    dateDebut = abonnement.dateFin;
                }
                else
                {
                    dateDebut = DateTime.Now;
                }
            }
            else
            {
                dateDebut = DateTime.Now;
            }
            /*       if (offre.Contains("B")) // prix actuel Bouquet 
                   {
                       string actualPrice= this.getPrix(co, offre);
                       if (actualPrice.Contains(","))
                       {
                           string[] price = actualPrice.Split(",");
                           prixActuel= Double.Parse(price[0]);
                       }
                       else
                       {
                           prixActuel= Double.Parse(actualPrice);
                       }
                   }*/

            /*      if (priceToInsert.Contains(",")) // prix actuel Bouquet+chaine
                  {
                      string[] actualPrice = priceToInsert.Split(",");
                      for(int i = 0; i< actualPrice.Length; i++) 
                      { 
                          prixActuel += Double.Parse(actualPrice[i]);
                      }
                  }*/

            if (offre.Contains(",")) //prix bouquet actuel
            {
                if (offre.Contains("B"))
                {
                    CompositionBouquet comp = new CompositionBouquet();
                    Bouquet bouquet = new Bouquet();
                    string[] actual = offre.Split(",");
                    bouquet = bouquet.BouquetById(co, actual[0]);
                    prixActuel = comp.prixBouquet(co, actual[0]);
                    prixActuel = prixActuel - ((prixActuel * bouquet.remise) / 100);
                }
            }
            else
            {
                prixActuel = Double.Parse(priceToInsert);
            }

            //prixActuel= Double.Parse(priceToInsert); prix total actuel
            DateTime dateFin = dateDebut.AddDays(30);
            Console.WriteLine("Dernier abo "+ prixLastAbonnement);
            Console.WriteLine("Actuel abo "+ prixActuel);
            if (prixActuel >= prixLastAbonnement)
            {
                String querry = "insert into abonnement values ('" + idClient + "','" + offre + "','" + dateDebut + "','" + dateFin + "','" + priceToInsert + "')";
                Console.WriteLine(querry);
                SqlCommand command = new SqlCommand(querry, co);
                command.ExecuteNonQuery();
                result = true;
            }
            return result;
        }
        
        public int getRemise(SqlConnection co)
        {
            int rem = 0;
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM remise", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rem = (int) reader["remise"];
            }
            reader.Close();
            return rem;
        }
        public Boolean BouquetSpecifique(SqlConnection co, String idClient, String offre)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Boolean result = false;
            DateTime dateDebut = new DateTime();
            Abonnement abonnement = this.lastAbonnement(co, idClient);
            String lastBouquet = null;
            String priceToInsert = this.getPrix(co, offre);
            double prixLastAbonnement = 0;
            double prixActuel = 0;
            int nombreChaines = 0;
            int remiseFinal = 0;
            if (offre.Contains(","))
            {
                string[] offres = offre.Split(",");
                nombreChaines = offres.Length;
            }
            if (nombreChaines > 5)
            {
                //int rem = this.getRemise(co);
                remiseFinal = nombreChaines - 5;
                double prixRem = Double.Parse(priceToInsert) ;
                prixRem= prixRem - ((prixRem * remiseFinal) / 100);
                priceToInsert = prixRem.ToString();
            }
            if (abonnement != null)
            {
                lastBouquet = abonnement.offre;
                String lastPrice = this.getPrix(co, lastBouquet);
                prixLastAbonnement += Double.Parse(lastPrice);
                if (DateTime.Now < abonnement.dateFin)
                {
                    dateDebut = abonnement.dateFin;
                }
                else
                {
                    dateDebut = DateTime.Now;
                }
            }
            else
            {
                dateDebut = DateTime.Now;
            }
            prixActuel = Double.Parse(priceToInsert);
            DateTime dateFin = dateDebut.AddDays(30);
          /*  if(prixActuel>= prixLastAbonnement)
            {*/
                String querry = "insert into abonnement values ('" + idClient + "','" + offre + "','" + dateDebut + "','" + dateFin + "','" + priceToInsert + "')";
                Console.WriteLine(querry);
                SqlCommand command = new SqlCommand(querry, co);
                command.ExecuteNonQuery();
                result = true;
           // }
            return result;
        }

        public void Reabonnement(SqlConnection co, String idClient)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Abonnement abonnement = this.lastAbonnement(co, idClient);
            DateTime dateDebut = new DateTime();
            DateTime dateFin = abonnement.dateFin;
            if(DateTime.Now < dateFin)
            {
                DateTime newdateFin = dateFin.AddDays(30);
                String querry = "update abonnement set datefin='"+newdateFin+"' where datefin='"+dateFin+"' and idClient='"+idClient+"'";
                Console.WriteLine(querry);
                SqlCommand command = new SqlCommand(querry, co);
                command.ExecuteNonQuery();
            }
            else
            {
                dateDebut= DateTime.Now;
                DateTime newdateFin = dateDebut.AddDays(30);
                String querry = "insert into abonnement values ('" + idClient + "','" + abonnement.offre + "','" + dateDebut + "','" + newdateFin + "','" + abonnement.prix + "')";
                Console.WriteLine(querry);
                SqlCommand command = new SqlCommand(querry, co);
                command.ExecuteNonQuery();
            }
        }

        public Abonnement lastAbonnement(SqlConnection co, String idClient)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Abonnement abonnement = null;
            String querry = "select top 1 idClient,abonnement,datedebut,datefin,prix from abonnement where idClient='" + idClient + "' order by datedebut desc";
            Console.WriteLine(querry);
            SqlCommand command = new SqlCommand(querry, co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idClient"];
                string offre= (string)reader["abonnement"];
                DateTime debut = reader.GetDateTime(2);
                DateTime fin= reader.GetDateTime(3);
                string prix = (string)reader["prix"];
                abonnement = new Abonnement(id, offre, debut, fin, prix);
            }
            reader.Close();
            return abonnement;
        }

        public String nomOffre(SqlConnection co, String offre)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            String nomOffre = null;
            Bouquet b = new Bouquet();
            if (offre.Contains(","))
            {
                String[] split_offre = offre.Split(",");
                if (split_offre[0].Contains("B"))
                {
                    b = b.BouquetById(co, split_offre[0]);
                    nomOffre = b.nomBouquet;
                    for (int i = 1; i < split_offre.Length; i++)
                    {
                        Chaine c = new Chaine();
                        c = c.chaineById(co, split_offre[i]);
                        nomOffre += "," + c.nomChaine;
                    }
                }
                else
                {
                    Chaine c = new Chaine();
                    c = c.chaineById(co, split_offre[0]);
                    nomOffre = c.nomChaine;
                    for (int i = 1; i < split_offre.Length; i++)
                    {
                        c = c.chaineById(co, split_offre[i]);
                        nomOffre += "," + c.nomChaine;
                    }
                }
            }
            else
            {
                b = b.BouquetById(co, offre);
                nomOffre = b.nomBouquet;
            }
            return nomOffre;
        }

        public List<Abonnement> HistoriqueAbonnement(SqlConnection co, String idClient)
        {
            if (co == null) 
            { 
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<Abonnement> list= new List<Abonnement>();
            String querry = "select a.idClient,a.abonnement,a.datedebut,a.datefin,a.prix,c.nomClient from abonnement a join client c on a.idClient=c.idClient where a.idClient='" + idClient + "' order by datedebut desc";
            Console.WriteLine(querry);
            SqlCommand command = new SqlCommand(querry, co);
            SqlDataReader reader = command.ExecuteReader();
            SqlConnection connection = null;
            while (reader.Read())
            {
                double price = 0;
                string offre = (string)reader["abonnement"];
                string id = (string)reader["idClient"];
                DateTime dateDebut = reader.GetDateTime(2);
                DateTime dateFin = reader.GetDateTime(3);
                string prix = (string)reader["prix"];
                Console.WriteLine(prix);
 /*               if (prix.Contains(","))
                {
                    string[] vola = prix.Split(",");
                    for(int i = 0; i<vola.Length; i++) {
                        price += Double.Parse(vola[i]);
                    }
                    prix= price.ToString();
                }*/
                string nom= (string)reader["nomClient"];
                Abonnement abonnement = new Abonnement(id,nom,dateDebut,dateFin,prix);
                abonnement.offre = this.nomOffre(connection, offre);
                list.Add(abonnement);
            }
            reader.Close();
            return list;
        }
    }
}

