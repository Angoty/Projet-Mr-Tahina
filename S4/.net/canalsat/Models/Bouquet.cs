using Microsoft.Data.SqlClient;

namespace canalsat.Models
{
    public class Bouquet
    {
        public String idBouquet { get; set; }
        public String nomBouquet { get; set; }
        public int remise { get; set; }
        public double prix { get; set; }

        public Bouquet() { 
        
        }
        public Bouquet(String idBouquet, string nomBouquet, int remise)
        {
            this.idBouquet = idBouquet;
            this.nomBouquet = nomBouquet;
            this.remise = remise;
        }

        public List<Bouquet> getBouquet(SqlConnection co)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<Bouquet> list = new List<Bouquet>();
            SqlCommand command = new SqlCommand("SELECT * FROM Bouquet", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idBouquet"];
                string nomB = (string)reader["nomBouquet"];
                int remise = (int)reader["remise"];
                Bouquet bouquet= new Bouquet(id, nomB, remise);
                list.Add(bouquet);
            }
            reader.Close();
            //co.Close();
            return list;
        }

        public Bouquet BouquetById(SqlConnection co,String idBouquet)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Bouquet bouquet = null;
            SqlCommand command = new SqlCommand("SELECT * FROM Bouquet where idBouquet='"+idBouquet+"'", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idBouquet"];
                string nomB = (string)reader["nomBouquet"];
                int remise = (int)reader["remise"];
                bouquet = new Bouquet(id, nomB, remise);
            }
            reader.Close();
            //co.Close();
            return bouquet;
        }
    }
}
