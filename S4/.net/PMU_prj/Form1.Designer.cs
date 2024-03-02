namespace PMU;
using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        NpgsqlConnection con = null;
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000,800);
        this.Text = "PMU";

        Personne pers = new Personne();
        Personne pers1 = pers.getOnePerson(con,1);
        Personne pers2 = pers.getOnePerson(con,2);
        List<Personne> per = new List<Personne>();
        per.Add(pers1);
        per.Add(pers2);

        Terrain ter = new Terrain(510,120,550,250,Color.White);
        List<Cheval> lc = new List<Cheval>();
        Cheval c = new Cheval();
        Cheval c1 = c.getOneHourse(con,1);

        c1.setX(680);
        c1.setY(220);
        c1.setWidth(30);
        c1.setHeight(30);
        c1.setColor(Color.Blue);
        PictureBox pic1 = new PictureBox();
        pic1.Location = new Point(c1.getX(),c1.getY());
        pic1.Size = new Size(c1.getWidth(),c1.getHeight());
        pic1.BackColor = c1.getColor();
        c1.setPic(pic1);

        Cheval c2 = c.getOneHourse(con,2);
        c2.setX(700);
        c2.setY(220);
        c2.setWidth(30);
        c2.setHeight(30);
        c2.setColor(Color.Black);
        PictureBox pic2 = new PictureBox();
        pic2.Location = new Point(c2.getX(),c2.getY());
        pic2.Size = new Size(c2.getWidth(),c2.getHeight());
        pic2.BackColor = c2.getColor();
        c2.setPic(pic2);

        lc.Add(c1);
        lc.Add(c2);
        int i = 0 ;
        TempsVola temps = new TempsVola(this.getPartie().getId(),100,10);
        temps.InsertTempsVola(con);
        foreach (Cheval cheval in lc){
            PartieCheval pc = new PartieCheval(this.getPartie().getId(),cheval.getId());
            ChevalPersonne cp = new ChevalPersonne(this.getPartie().getId(),cheval.getId(),per[i].getId());
            cp.InsertchevalPersonne(con);
            i+=1;
            pc.InsertPartieCheval(con);
        }
        Point point = new Point(665,250);
        System.Windows.Forms.Timer tempsMatch = new System.Windows.Forms.Timer();
        PanelStade p = new PanelStade(100,100,800,500,Color.Brown,ter,lc,point,tempsMatch,this);
        this.Controls.Add(p);
        this.KeyDown +=p.startTimer;
    }

    #endregion
}
