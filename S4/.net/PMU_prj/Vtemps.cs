namespace PMU;
using Npgsql;
public class VTemps{
    Partie? partie ;
    ChevalTiempo? chT;
    Personne? personne ;
    Cheval? cheval;
    public void setPersonne(Personne p){
        this.personne = p ;
    }
    public Personne? getPersonne(){
        return this.personne;
    }
    public void setPartie(Partie i){
        this.partie = i ;
    }
    public void setTemps(ChevalTiempo t){
        this.chT = t ;
    }
    public Partie? getPartie(){
        return partie;
    }
    public ChevalTiempo? getTemps(){
        return this.chT;
    }
    public void setCheval(Cheval c){
        this.setCheval(c);
    }
    public Cheval? getCheval(){
        return this.cheval;
    }
    public VTemps(){}
    public VTemps(Partie p , ChevalTiempo ch ,Personne pers , Cheval c){
        setPartie(p);
        setTemps(ch);
        setPersonne(pers);
        setCheval(c);
    }
    public List<VTemps> getAllChevalTemps(NpgsqlConnection connection,int id){
        bool valid = true ;
        List<VTemps> c = new List<VTemps>();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM v_relationPCPT WHERE idPartie = @id";
            using (NpgsqlCommand command = new NpgsqlCommand(request , connection)){
                command.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idP = reader.GetInt32(reader.GetOrdinal("idPartie"));
                        int idC= reader.GetInt32(reader.GetOrdinal("idCheval"));
                        // c .Add(new VTemps(idP,idC));
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