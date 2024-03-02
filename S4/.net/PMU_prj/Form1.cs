namespace PMU;

public partial class Form1 : Form
{
    Partie p ;
    public void setPartie(Partie p1){
        this.p = p1 ;
    }
    public Partie getPartie(){
        return this.p;
    }
    public Form1(Partie p1){
        setPartie(p1);
        InitializeComponent();
    }
}
