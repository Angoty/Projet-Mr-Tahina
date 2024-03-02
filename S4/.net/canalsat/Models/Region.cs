namespace canalsat.Models
{
    public class Region
    {
        int idRegion;
        String nomRegion;
        int signal;

        public Region(int idRegion, string nomRegion, int signal)
        {
            this.idRegion = idRegion;
            this.nomRegion = nomRegion;
            this.signal = signal;
        }
    }
}
