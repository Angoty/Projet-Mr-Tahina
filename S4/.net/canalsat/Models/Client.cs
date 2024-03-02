using Microsoft.Data.SqlClient;

namespace canalsat.Models
{
    public class Client
    {
       public String idClient { get; set; }
        public String nomClient { get; set; }
        public String localisation { get; set; }
        public int signalRegion { get; set; }

        public Client()
        {

        }

        public Client(string idClient, string nomClient, String localisation, int signalRegion)
        {
            this.idClient = idClient;
            this.nomClient = nomClient;
            this.localisation = localisation;
            this.signalRegion = signalRegion;
        }

        public Client CurrentClient(SqlConnection co, String idClient)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Client client=null;
            SqlCommand command = new SqlCommand("SELECT * FROM v_clientRegion where idClient = '"+idClient+"' ", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idClient"];
                string nom = (string)reader["nomClient"];
                string location = (string)reader["nomRegion"];
                int signal = (int)reader["signal"];
                client = new Client(id,nom,location,signal);
            }
            reader.Close();
            return client;
        }

        public List<Client> ListClient(SqlConnection co)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
           List<Client> list = new List<Client>();
            SqlCommand command = new SqlCommand("SELECT * FROM v_clientRegion", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["idClient"];
                string nom = (string)reader["nomClient"];
                string location = (string)reader["nomRegion"];
                int signal = (int)reader["signal"];
                Client client = new Client(id, nom, location, signal);
                list.Add(client);
            }
            reader.Close();
            return list;
        }

    }
}
