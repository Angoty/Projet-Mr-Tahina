namespace PMU;
using Npgsql;
public class PartieCheval{
    int idPartie ;
    int idCheval ;
    public void setIdPartie(int i){
        this.idPartie = i ;
    }
    public void setIdCheval(int i){
        this.idCheval = i ;
    }
    public int getIdPartie(){
        return idPartie;
    }
    public int getIdCheval(){
        return idCheval;
    }
    public PartieCheval(){}
    public PartieCheval(int i,int n){
        setIdPartie(i);
        setIdCheval(n);
    }
    public void InsertPartieCheval(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO partie_Cheval (idPartie,idCheval) VALUES (@idPartie,@idCheval)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@idPartie", this.getIdPartie());
                command.Parameters.AddWithValue("@idCheval", this.getIdCheval());
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
     public List<PartieCheval> getOnePartieCheval(NpgsqlConnection connection,int id){
        bool valid = true ;
        List<PartieCheval> c = new List<PartieCheval>();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM partieCheval WHERE idPartie = @id";
            using (NpgsqlCommand command = new NpgsqlCommand(request , connection)){
                command.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("idPartie"));
                        int idC= reader.GetInt32(reader.GetOrdinal("idCheval"));
                        c .Add(new PartieCheval(idP,idC));
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