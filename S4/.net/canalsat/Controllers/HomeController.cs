using canalsat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace canalsat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Accueil()
        {
            Client c = new Client();
            CompositionBouquet cb = new CompositionBouquet();
            Abonnement abonnement = new Abonnement();
            SqlConnection co = null;
            var id= Request.Form["username"];
            var client = c.CurrentClient(co, id);
            Chaine chaine= new Chaine();
            var chaines= chaine.ChaineDispo(co, client.signalRegion);
            var bouquets= cb.BouquetDispo(co, client.signalRegion);
            var historique= abonnement.HistoriqueAbonnement(co, id);
            Abonnement offre_actuel = null;
            if (historique.Count!=0)
            {
                offre_actuel = historique[0];
            }
            else
            {
                offre_actuel = null;
            }
            AccueilModel am = new AccueilModel(chaines, client, bouquets,offre_actuel);
            if (co != null)
            {
                co.Close();
            }
            return View(am);
        }

        public IActionResult ListeChaine()
        {
            Chaine c = new Chaine();
            SqlConnection co = null;
            var list = c.All_channel(co);
            if (co != null)
            {
                co.Close();
            }
            return View(list);
        }

        public IActionResult Specifique()
        {
            SqlConnection co = null;
            String idClient = Request.Form["idClient"];
            String[] selectedValues = Request.Form["perso"];
            Abonnement abonnement = new Abonnement();
            String offre = abonnement.getOffreSpecifique(selectedValues);
            abonnement.BouquetSpecifique(co, idClient, offre);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmAbonnement()
        {
            SqlConnection co = null;
            String idClient = Request.Query["idClient"];
            String idBouquet= Request.Query["idBouquet"];
            Client c = new Client();
            Chaine chaine = new Chaine();
            var client = c.CurrentClient(co, idClient);
            var chaines = chaine.chaineProposee(co, idBouquet, client.signalRegion);
            ViewBag.idClient = idClient;
            ViewBag.idBouquet = idBouquet;
            if (co != null)
            {
                co.Close();
            }
            return View(chaines);
        }

        public IActionResult Abonnement()
        {
            SqlConnection co = null;
            String[] chaines= Request.Form["perso"];
            String idClient = Request.Form["idClient"];
            String idBouquet = Request.Form["idBouquet"];
            Abonnement abonnement = new Abonnement();
            string offre = abonnement.getOffre(chaines, idBouquet);
            abonnement.insertionAbonnement(co, idClient, offre);
            if (co != null)
            {
                co.Close();
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Reabonnement()
        {
            SqlConnection co = null;
            String idClient = Request.Form["idClient"];
            Abonnement abonnement = new Abonnement();
            abonnement.Reabonnement(co, idClient);
            if (co != null)
            {
                co.Close();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ListClients()
        {
            SqlConnection co = null;
            Client c = new Client();
            List<Client> list = c.ListClient(co);
            if (co != null)
            {
                co.Close();
            }
            return View(list);
        }

        public IActionResult Historique()
        {
            SqlConnection co = null;
            Abonnement a= new Abonnement();
            String idClient = Request.Query["idClient"];
            List<Abonnement> list = a.HistoriqueAbonnement(co, idClient);
            Client c = new Client();
            var client = c.CurrentClient(co,idClient);
            ViewBag.idClient = idClient;
            ViewBag.nomClient = client.nomClient;
            if (co != null)
            {
                co.Close();
            }
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}