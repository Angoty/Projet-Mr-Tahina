namespace PMU;
using System.Drawing;
using System.Windows.Forms;
public class Terrain {
    int x ;
    int y ;
    int width;
    int height;
    Color couleur;
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
    public Terrain(){}
    public Terrain(int x , int y , int w , int h,Color c ){
        setWidth(w);
        setX(x);
        setY(y);
        setHeight(h);
        setCouleur(c);
    }
    
}