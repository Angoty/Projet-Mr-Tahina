namespace PMU;
using Npgsql;
static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        NpgsqlConnection c = null;
        try{
            Partie p = new Partie(1,"P2");
            c = Connexion.getConnection();
            c.Open();
            p.insertPartie(c);
            Partie part = p.getEndPartie(c);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(part));
        }
        catch (System.Exception e){
            MessageBox.Show(e.Message);
        }finally{
            if (c!=null){
                c.Close();
            }
        }
    }    
}