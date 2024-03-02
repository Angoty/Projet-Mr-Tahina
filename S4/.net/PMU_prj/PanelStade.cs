namespace PMU;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using  Npgsql ; 
public class PanelStade:Panel {
    System.Windows.Forms.Timer tempsMatch = new System.Windows.Forms.Timer();
    Form1 forms ;
    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
    int x ;
    int y ;
    int width;
    int height;
    Color couleur;
    Terrain? terrain ;
    List<Cheval> chevals;
    Point points;
    double temps =0;
    int i = 0 ;
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
    public void setCouleur(Color c ){
        this.couleur = c ;
    }
    public void setTerrain(Terrain t){
        this.terrain = t ;
    }
    public void setListCheval(List<Cheval> lc ){
        this.chevals = lc;
    }
    public void setPoint(Point p){
        this.points = p ;
    }
    public void setTempsMtach(Timer t){
        this.tempsMatch = t ;
    }
    public void setTemps(double t){
        this.temps = t ;
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
    public Color getCouleur(){
        return this.couleur;
    }
    public Point getPoints(){
        return this.points;
    }
    public Terrain? getTerrain(){
        return this.terrain;
    }
    public List<Cheval> getChevals(){
        return this.chevals;
    }
    public Timer getTempsMatch(){
        return this.tempsMatch;
    }
    public double getTemps(){
        return this.temps;
    }
    public PanelStade(){}
    public PanelStade(int x , int y , int w , int h,Color c,Terrain t , List<Cheval> lc,Point p , Timer tt,Form1 f){
        setWidth(w);
        setX(x);
        setY(y);
        setHeight(h);
        setCouleur(c);
        setTerrain(t);
        setListCheval(lc);
        setPoint(p);
        setTempsMtach(tt);
        forms = f;
        this.Location = new Point(this.getX(),this.getY());
        this.Size = new Size(this.getWidth(),this.getHeight());
        this.BackColor = this.getCouleur();
        DoubleBuffered=true;
        getTempsMatch().Tick += new EventHandler(moveHourse);
        getTempsMatch().Interval = 200;
        timer.Tick+= new EventHandler(tiempo);
        timer.Interval = 1000;
    }
    protected override void OnPaint(PaintEventArgs p){
        base.OnPaint(p);
        Graphics g = p.Graphics;
        GraphicsPath gr= new GraphicsPath(); 
        gr.AddEllipse(ClientRectangle);
        Region=new Region(gr);
        
        // dessin cheval et dessin terrain
        int x = 0 ; 
        int y = 0 ; 
        Font f = new Font("cursive",15);
        SolidBrush s = new SolidBrush(Color.Gray);
        g.DrawString(i+" ",f,s,100,100);
        SolidBrush ss = new SolidBrush(Color.Red);
        g.FillRectangle(ss,this.getPoints().X,this.getPoints().Y,180,20);
        if (this.getChevals()!=null){
            foreach(Cheval c in this.getChevals()){
                Rectangle r = new Rectangle();
                r.Width = ClientRectangle.Width-x;
                r.Height = ClientRectangle.Height-x;
                r.X = ClientRectangle.X-x;
                r.Y = ClientRectangle.Y-x;
                Controls.Add(c.getPic());
                x +=150;
                y+=100;
            }
        }
        SolidBrush br = new SolidBrush(Color.Green);
        g.FillEllipse(br,this.getTerrain().getY(),this.getTerrain().getY(),this.getTerrain().getWidth(),this.getTerrain().getHeight());
    }
    public void startTimer(object sender, KeyEventArgs e){
        if (e.KeyCode == Keys.G){
            this.getTempsMatch().Start();
            timer.Start();
        }
    }
    public void moveHourse(object sender,EventArgs e){
        int xh = 20;
        int yh = 20;
        foreach(Cheval c in this.getChevals()){
            c.setAngle(c.getAngle()+c.getVitesse());
            int centreX = this.getWidth()/2;
            int centreY = this.getHeight()/2;
            int radX=this.getWidth()/2;
            int radY=this.getHeight()/2;

            double radians = c.getAngle()*Math.PI/180;

            double offX = Math.Cos(radians) * (radX - xh);
            double offY = Math.Sin(radians) * (radY - yh);

            int x = (int)((centreX+offX) - c.getWidth()/2);
            int y = (int)((centreY+offY) - c.getHeight()/2);
            c.getPic().Location = new Point(x,y);
            c.setTour();
            if (c.getTour()==1 && c.getAngle()==0){
                c.setVitesse(0);
            }
            if (c.getTemps()== 0 && c.getVitesse()==0){
                c.setTemps(i);
                ChevalTiempo t = new ChevalTiempo(forms.getPartie().getId(),c.getId(),i);
                t.insertChevalTiempo(null);
            }
            if(c.getAngle()==252){
                c.setVitesse(4);
                MessageBox.Show(c.getVitesse().ToString());
                // MessageBox.Show("Misy changement");
            }
            xh+=20;
            yh+=20;
        }
        this.stopTimer();
        Invalidate();
    }
    public void tiempo(object sender , EventArgs e){
        i+=1;
    }
    public double perimetre(){
        int a = this.getWidth()/2;
        int b = this.getHeight()/2;
        return Math.PI * ((3*(a+b))-Math.Sqrt((3*a+b)*(a+3*b)));
    }
    public double getPerimetre(){
        int a = this.getWidth()/2;
        int b = this.getHeight()/2;
        return 2*Math.PI*Math.Sqrt(((Math.Pow(a,2))+(Math.Pow(b,2)))/2);
    }
    public void stopTimer(){
        int v = 0 ;
        foreach (Cheval p in this.getChevals()){
            if (p.getVitesse()==0){
                v+=1;
            }
        }
        if (v==this.getChevals().Count){
            timer.Stop();
        }
    }
    
    
}