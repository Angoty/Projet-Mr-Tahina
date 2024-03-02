namespace PMU;
using Npgsql;
public class TempsVola{
    int idPartie ;
    double vola ;
    double temps;
    public void setIdPartie(int i){
        this.idPartie = i ;
    }
    public void setVola(double v){
        this.vola = v ;
    }
    public void setTemps(double t){
        this.temps = t ;
    }
    public int getIdPartie(){
        return idPartie;
    }
    public double getVola(){
        return this.vola;
    }
    public double getTemps(){
        return this.temps;
    }
    public TempsVola(){}
    public TempsVola(int p , double v , double t){
        setIdPartie(p);
        setVola(v);
        setTemps(t);
    }
    public void InsertTempsVola(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO tempsVola (idPartie,vola,temps) VALUES (@idPartie,@vola,@temps)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@idPartie", this.getIdPartie());
                command.Parameters.AddWithValue("@vola", this.getVola());
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
     public TempsVola getOneTempsVola(NpgsqlConnection connection,int id){
        bool valid = true ;
        TempsVola c = new TempsVola();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM tempsVola WHERE idPartie = @id";
            using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
                command.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("idPartie"));
                        double temps = reader.GetDouble(reader.GetOrdinal("temps"));
                        double vola= reader.GetDouble(reader.GetOrdinal("vola"));
                        c =new TempsVola(idP,vola,temps);
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
    public double elanelanaTemps(double t1 , double t2){
        MessageBox.Show((t2-t1).ToString());
        return t2-t1;
    }
}