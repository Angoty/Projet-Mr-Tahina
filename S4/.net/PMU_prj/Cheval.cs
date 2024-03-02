namespace PMU;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
public class Cheval {
    int id ;
    string? nom ;
    int vitesse ;
    double endurence;
    int x ;
    int y ;
    int width;
    int height;
    Color color ;
    PictureBox pic ;
    double angle =0;
    int tour ;
    double temps = 0;
    public void setTemps(double t){
        this.temps = t ;
    }
    public double getTemps(){
        return this.temps;
    }

    public void setTour(int t){
        this.tour = t ;
    }
    public int getTour(){
        return this.tour;
    }

    public void setAngle(double a){
        this.angle = a ;
    }
    public double getAngle(){
        return this.angle;
    }
    public void setId(int id){
        this.id = id ; 
    }
    public void setNom(string nom){
        this.nom = nom ; 
    }
    public void setVitesse(int v){
        this.vitesse = v ; 
    }
    public void setEndurence(double e){
        this.endurence = e ; 
    }
    public void setX(int x){
        this.x = x ; 
    }
    public void setY(int y){
        this.y = y ;
    }
    public void setWidth(int x){
        this.width = x ; 
    }
    public void setHeight(int y){
        this.height = y ;
    }
    public void setColor(Color c ){
        this.color=c;
    }
    public int getId(){
        return this.id;
    }
    public string? getNom(){
        return this.nom;
    }
    public int getVitesse(){
        return this.vitesse;
    }
    public double getEndurence(){
        return this.endurence;
    }
    public int getX(){
        return this.x ;
    }
    public int getY(){
        return this.y;
    }
    public int getWidth(){
        return this.width;
    }
    public int getHeight(){
        return this.height;
    }
    public Color getColor(){
        return this.color;
    }
    public void setPic(PictureBox p){
        this.pic = p ;
    }
    public PictureBox getPic(){
        return this.pic;
    }
    public Cheval(){}
    public Cheval(int id , string nom , int v , double e ,int x , int y , int w , int h,Color c ){
        setId(id);
        setNom(nom);
        setVitesse(v);
        setEndurence(e);
        setWidth(w);
        setX(x);
        setY(y);
        setWidth(w);
        setHeight(h);
        setColor(c);
        PictureBox p = new PictureBox();
        p.Location = new Point(this.getX(),this.getY());
        p.Size = new Size(this.getWidth(),this.getHeight());
        p.BackColor = this.getColor();
        this.setPic(p);
    }
    public Cheval( int x , int y , int w , int h,Color c ){
        setWidth(w);
        setX(x);
        setY(y);
        setHeight(h);
        setColor(c);
         PictureBox p = new PictureBox();
        p.Location = new Point(this.getX(),this.getY());
        p.Size = new Size(this.getWidth(),this.getHeight());
        p.BackColor = this.getColor();
        this.setPic(p);
    }
    public Cheval(int id , string nom , int v , double e){
        setId(id);
        setNom(nom);
        setVitesse(v);
        setEndurence(e);
        PictureBox p = new PictureBox();
        p.Location = new Point(this.getX(),this.getY());
        p.Size = new Size(this.getWidth(),this.getHeight());
        p.BackColor = this.getColor();
        this.setPic(p);
    }
    public void InsertCheval(NpgsqlConnection connection){
        bool valid = true;
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string query = "INSERT INTO cheval (nom,vitesse,endurence) VALUES (@nom,@vitesse,@endurence)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection)){
                command.Parameters.AddWithValue("@nom", this.getNom());
                command.Parameters.AddWithValue("@vitesse", this.getVitesse());
                command.Parameters.AddWithValue("@endurence", this.getEndurence());
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(query);
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
    public Cheval getOneHourse(NpgsqlConnection connection,int id){
        bool valid = true ;
        Cheval c = new Cheval();
        try{
            if (connection==null){
                connection = Connexion.getConnection();
                valid = false;
                connection.Open();
            }
            string request = "SELECT * FROM cheval WHERE id = @id";
        using (NpgsqlCommand command = new NpgsqlCommand(request, connection)){
             command.Parameters.AddWithValue("id", id);
            using (NpgsqlDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                        int idC = reader.GetInt32(reader.GetOrdinal("id"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        int vitesse = reader.GetInt32(reader.GetOrdinal("vitesse"));
                        double endurence = reader.GetDouble(reader.GetOrdinal("endurence"));
                        c = new Cheval(idC,nom,vitesse,endurence);
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
    public void setTour(){
        int v = 1 ;
        if (this.getAngle()>=360){
            this.setTour(v);
            this.setAngle(0);
            v+=1;
        }
    }
}
