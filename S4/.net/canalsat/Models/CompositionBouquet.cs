using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace canalsat.Models
{
    public class CompositionBouquet
    {
        public String idBouquet { get; set; }
        public String nomBouquet { get; set; }
        public int remise { get; set; }
        public String idChaine { get; set; }
        public String nomChaine { get; set; }
        public double prix { get; set; }
        public int signal { get; set; }

        public CompositionBouquet(String idBouquet, string nomBouquet, int remise, String idChaine, string nomChaine, double prix, int signal)
        {
            this.idBouquet = idBouquet;
            this.nomBouquet = nomBouquet;
            this.remise = remise;
            this.idChaine = idChaine;
            this.nomChaine = nomChaine;
            this.prix = prix;
            this.signal = signal;
        }

        public CompositionBouquet() { 
        
        }

        public List<CompositionBouquet> getCompositionBouquet(SqlConnection co)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<CompositionBouquet> list = new List<CompositionBouquet>();
            SqlCommand command = new SqlCommand("SELECT * FROM v_compBouquet", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idBouquet"];
                string nomB = (string)reader["nomBouquet"];
                int remise = (int)reader["remise"];
                string idChaine = (string)reader["idChaine"];
                string nomC = (string)reader["nomChaine"];
                decimal p = (decimal)reader["prix"];
                double price = (double)p;
                int sign = (int)reader["signal"];
                CompositionBouquet comp = new CompositionBouquet(id, nomB, remise, idChaine, nomC, price, sign);
                list.Add(comp);
            }
            reader.Close();
            //co.Close();
            return list;
        }


        public double prixBouquet(SqlConnection co, String idBouquet)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            double prix = 0;
            SqlCommand command = new SqlCommand("SELECT sum(prix) price FROM v_compBouquet where idBouquet= '"+idBouquet+"'", co);
            SqlDataReader reader = command.ExecuteReader();
            int remise = 0;
            while (reader.Read())
            {
                decimal p = (decimal)reader["price"];
                 prix = (double)p;
            }
            reader.Close();
            return prix;
        }

        public List<Bouquet> BouquetDispo(SqlConnection co, int signal)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<String> index = this.IndiceBouquetDispo(co, signal);
            List<Bouquet> list = new List<Bouquet>();
            for (int i = 0; i < index.Count; i++)
            {
                double prix= this.prixBouquet(co, index[i]);
                String querry = "SELECT * FROM Bouquet where idBouquet= '" + index[i] + "' ";
                Console.WriteLine(querry);
                SqlCommand command = new SqlCommand(querry, co);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = (string)reader["idBouquet"];
                    string nomB = (string)reader["nomBouquet"];
                    int remise = (int)reader["remise"];
                    Bouquet bouquet = new Bouquet(id, nomB, remise);
                    bouquet.prix = prix - ((prix * remise) / 100);
                    list.Add(bouquet);
                }
                reader.Close();
            }
            //co.Close();
            return list;
        }

        public List<CompositionBouquet> CompBouquetByID(SqlConnection co, String idBouquet)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<CompositionBouquet> list = new List<CompositionBouquet>();
            SqlCommand command = new SqlCommand("SELECT * FROM v_compBouquet where idBouquet='"+idBouquet+"'", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idBouquet"];
                string nomB = (string)reader["nomBouquet"];
                int remise = (int)reader["remise"];
                string idChaine = (string)reader["idChaine"];
                string nomC = (string)reader["nomChaine"];
                decimal p = (decimal)reader["prix"];
                double price = (double)p;
                int sign = (int)reader["signal"];
                CompositionBouquet comp = new CompositionBouquet(id, nomB, remise, idChaine, nomC, price, sign);
                list.Add(comp);
            }
            reader.Close();
            //co.Close();
            return list;
        }

        public List<String> IndiceBouquetDispo(SqlConnection co, int signal)
        {
            Bouquet b = new Bouquet();
            List<Bouquet> bouquet = b.getBouquet(co);
            List<String>index= new List<String>();
            for(int i=0; i<bouquet.Count; i++)
            {
                List<CompositionBouquet> comp= this.CompBouquetByID(co,bouquet[i].idBouquet);
                int ind = comp.Count;
                for(int j=0; j<comp.Count; j++)
                {
                    if (comp[j].signal > signal)
                    {
                        ind--;
                    }
                    
                }
                if (ind == comp.Count)
                {
                    index.Add(bouquet[i].idBouquet);
                }
            }
            return index;
        }
    }
}
