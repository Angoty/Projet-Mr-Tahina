namespace PMU ;
using Npgsql;
public class ChevalPersonne {
    int idPartie ;
    int idPersonne ;
    int idCheval;

    public void setIdPartie(int p){
        this.idPartie = p ;
    }
    public void setIdPersonne(int o){
        this.idPersonne = o ;
    }
    public void setIdCheval(int c){
        this.idCheval = c ;
    }
    public int getIdPartie(){
        return this.idPartie;
    }
    public int getIdCheval(){
        return this.idCheval;
    }
    public int getIdPersonne(){
        return this.idPersonne;
    }
    public ChevalPersonne(){}
    public ChevalPersonne(int idP,int idC,int idPers){
        setIdPartie(idP);
        setIdCheval(idC);
        setIdPersonne(idPers);
    }
    public void InsertchevalPersonne(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO chevalPersonne (idPersonne,idCheval,idPartie) VALUES (@idPersonne,@idCheval,@idPartie)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@idPersonne", this.getIdPersonne());
                command.Parameters.AddWithValue("@idCheval", this.getIdCheval());
                command.Parameters.AddWithValue("@idPartie", this.getIdPartie());
                
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
    public ChevalPersonne getOneChevalPersonne(NpgsqlConnection connection,int id,int idC){
        bool valid = true ;
        ChevalPersonne c = new ChevalPersonne();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM chevalPersonne WHERE idPartie = @idPartie and idCheval = @idCheval";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("id", idC);
                using (NpgsqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("idPartie"));
                        int idPers = reader.GetInt32(reader.GetOrdinal("idPersonne"));
                        int idCheval = reader.GetInt32(reader.GetOrdinal("idCheval"));
                        c =new ChevalPersonne(idP,idCheval,idPers);
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