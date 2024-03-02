namespace PMU;
using Npgsql;
public class Personne {
    int id ;
    string? nom ;

    public void setId(int id){
        this.id = id ; 
    }
    public void setNom(string nom){
        this.nom = nom ; 
    }
    
    public int getId(){
        return this.id;
    }
    public string? getNom(){
        return this.nom;
    }
    public Personne(){}
    public Personne(int id , string nom){
        setId(id);
        setNom(nom);
    }
     public Personne getOnePerson(NpgsqlConnection connection,int id){
        bool valid = true ;
        Personne c = new Personne();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM personne WHERE id = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
             command.Parameters.AddWithValue("id", id);
            using (NpgsqlDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("id"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        c = new Personne(idP,nom);
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
    public void Insertpersonne(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO personne (nom) VALUES (@nom)";

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

}