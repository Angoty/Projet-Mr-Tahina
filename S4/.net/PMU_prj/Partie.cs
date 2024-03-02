namespace PMU;
using Npgsql;
public class Partie{
    int id ;
    string nom ;
    public void setId(int i){
        this.id = i ;
    }
    public void setNom(string n ){
        this.nom = n ;
    }
    public int getId(){
        return id;
    }
    public string getNom(){
        return nom;
    }
    public Partie(){}
    public Partie(int i,string n){
        setId(i);
        setNom(n);
    }
    public void insertPartie(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO partie (nom) VALUES (@nom)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@nom", this.getNom());
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0){
                    MessageBox.Show("Insertion reussi");
                }
            }
        }catch(Exception e){
            MessageBox.Show(e.Message);
        }finally{
            if (!valid && connection!=null){
                connection.Close();
            }
        }
        
    }
     public Partie getOnePartie(NpgsqlConnection connection,int id){
        bool valid = true ;
        Partie c = new Partie();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM partie WHERE id = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
             command.Parameters.AddWithValue("id", id);
            using (NpgsqlDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("id"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        c = new Partie(idP,nom);
                }
            }
        }
        }catch(Exception e){
            MessageBox.Show(e.Message);
        }finally{
            if (!valid && connection!=null){
                connection.Close();
            }
        }
        return c ;
    }
    public Partie getEndPartie(NpgsqlConnection connection){
        bool valid = true ;
        Partie c = new Partie();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM partie order by id desc limit 1";
        using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
            using (NpgsqlDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("id"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        c = new Partie(idP,nom);
                }
            }
        }
        }catch(Exception e){
            MessageBox.Show(e.Message);
        }finally{
            if (!valid && connection!=null){
                connection.Close();
            }
        }
        return c ;
    }
}