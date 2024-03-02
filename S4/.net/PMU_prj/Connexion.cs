namespace PMU;
using Npgsql;
public class Connexion{

public static NpgsqlConnection getConnection(){
    string connectionString = "Host=localHost;Port=5432;Database=pmu;Username=postgres;Password=mdpprom15";
    NpgsqlConnection connection = new NpgsqlConnection(connectionString);
    return connection;
}

}