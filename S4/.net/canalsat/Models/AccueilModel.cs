namespace canalsat.Models
{
    public class AccueilModel
    {
        public List<Chaine> chaines { get; set; }
        public Client client { get; set; }

        public List<Bouquet> bouquets { get; set; }

        public Abonnement abonnement { get; set; }
            public AccueilModel(List<Chaine> chaines, Client client, List<Bouquet> bouquets, Abonnement abonnement)
            {
                this.chaines = chaines;
                this.client = client;
                this.bouquets = bouquets;
                this.abonnement = abonnement;    
            }
    }
}
