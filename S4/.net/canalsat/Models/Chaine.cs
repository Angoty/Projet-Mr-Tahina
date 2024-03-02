using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace canalsat.Models
{
    public class Chaine
    {
        public String idChaine { get; set; }
        public String nomChaine { get; set; }
        public double prix { get; set; }
        public int signal { get; set; }

        public Chaine(String idChaine, string nomChaine, double prix, int signal)
        {
            this.idChaine = idChaine;
            this.nomChaine = nomChaine;
            this.prix = prix;
            this.signal = signal;
        }

        public Chaine()
        {

        }

        public List<Chaine> ChaineDispo(SqlConnection co, int signal)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<Chaine> list= new List<Chaine>();
                SqlCommand command = new SqlCommand("SELECT * FROM chaine where signal <= "+signal+" ", co);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = (string)reader["idChaine"];
                    string nom = (string)reader["nomChaine"];
                    decimal p = (decimal)reader["prix"];
                    double price = (double)p;
                    int sign = (int)reader["signal"];
                    Chaine c = new Chaine(id, nom, price, sign);
                    list.Add(c);
            }
                reader.Close();
            return list;
        }

        public List<Chaine> All_channel(SqlConnection co)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
                List<Chaine> list = new List<Chaine>();
                SqlCommand command = new SqlCommand("SELECT * FROM chaine", co);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = (string)reader["idChaine"];
                    string nom = (string)reader["nomChaine"];
                    decimal p = (decimal)reader["prix"];
                    double price = (double)p;
                    int sign = (int)reader["signal"];
                    Chaine c = new Chaine(id, nom, price, sign);
                    list.Add(c);
                }
                reader.Close();
                co.Close();
            return list;
        }

        public List<Chaine> chaineProposee(SqlConnection co,String idBouquet,int signal)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<Chaine> list = new List<Chaine>();
            SqlCommand command = new SqlCommand("select * from chaine where idchaine not in (select idChaine from v_compBouquet where idBouquet='"+idBouquet+"') and signal<"+signal+"", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idChaine"];
                string nom = (string)reader["nomChaine"];
                decimal p = (decimal)reader["prix"];
                double price = (double)p;
                int sign = (int)reader["signal"];
                Chaine c = new Chaine(id, nom, price, sign);
                list.Add(c);
            }
            reader.Close();
            return list;
        }

        public Chaine chaineById(SqlConnection co, String idChaine)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Chaine chaine= new Chaine();
            SqlCommand command = new SqlCommand("select * from chaine where idchaine='"+idChaine+"'", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idChaine"];
                string nom = (string)reader["nomChaine"];
                decimal p = (decimal)reader["prix"];
                double price = (double)p;
                int sign = (int)reader["signal"];
                chaine = new Chaine(id, nom, price, sign);
            }
            reader.Close();
            return chaine;
        }
    }
}
