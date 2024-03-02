namespace PMU;
using Npgsql;
public class ChevalTiempo{
    int idCheval ; 
    int idPartie ;
    double temps;
    public void setIdCheval(int idC){
        this.idCheval = idC;
    }
    public void setIdPartie(int idP){
        this.idPartie=idP;
    }
    public void setTemps(double t){
        this.temps = t ;
    }
    public int getIdCheval(){
        return this.idCheval;
    }
    public int getIdPartie(){
        return this.idPartie;
    }
    public double getTemps(){
        return this.temps ; 
    }
    public ChevalTiempo(){}
    public ChevalTiempo(int idP , int idC , double temps){
        setIdCheval(idC);
        setIdPartie(idP);
        setTemps(temps);
    }
    public void insertChevalTiempo(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO chevalTemps(idPartie,idCheval,temps) VALUES (@idPartie,@idCheval,@temps)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@idPartie", this.getIdPartie());
                command.Parameters.AddWithValue("@idCheval", this.getIdCheval());
                command.Parameters.AddWithValue("@temps", this.getTemps());
                
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
    public ChevalTiempo getOneChevalTiempo(NpgsqlConnection connection,int idC,int idP){
        bool valid = true ;
        ChevalTiempo c = new ChevalTiempo();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM chevalTemps WHERE idPartie = @idPartie and idCheval = @idCheval";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
                command.Parameters.AddWithValue("id", idP);
                command.Parameters.AddWithValue("id", idC);
                using (NpgsqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int id = reader.GetInt32(reader.GetOrdinal("idPartie"));
                        int idPers = reader.GetInt32(reader.GetOrdinal("temps"));
                        int idCheval = reader.GetInt32(reader.GetOrdinal("idCheval"));
                        c =new ChevalTiempo(id,idCheval,idPers);
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