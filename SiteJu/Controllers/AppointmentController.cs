using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteJu.Areas.Admin.Models;
using SiteJu.Data;
using SiteJu.Models;
using Microsoft.AspNetCore.Http;


namespace SiteJu.Controllers
{
    [Route("/Appointment")]

    public class AppointmentController : Controller
    {
        private readonly ReservationContext _context;

        public AppointmentController(ReservationContext context)
        {
            // BDD
            _context = context;

        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var prestations = _context.Prestations.Select(p => new PrestationViewModel
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = new PrestationCategoryViewModel { Name = p.Category.Name },
            }).ToList();


            return View(prestations);
        }

        [HttpPost("ValidPresta")]
        public IActionResult ValidPresta(IEnumerable<PrestationViewModel> rdvForm)
        {

            // Id prestations selected by client
            var selectedPrestaIds = rdvForm.Where(sp => sp.IsSelected).Select(so => so.Id).ToList();
            IQueryable<Prestation> prestations = _context.Prestations.Where(p => selectedPrestaIds.Contains(p.ID));

            // Stocker prestations séléctionnées dans une session
            HttpContext.Session.SetString("Prestations", System.Text.Json.JsonSerializer.Serialize(selectedPrestaIds));

            return RedirectToAction("Appointment");
        }


        // Je récupére les rdvs qui sont déja pris
        [HttpGet("SearchAppointment")]
        public IActionResult SearchAppointment(DateTime date)
        {
            var rdvAtDate = _context.RDVS.Where(r => r.At.Date == date.Date).Select(rdv => new
            {
                rdv.At,
            }).ToList();

            // Recuperation des prestas selectionnées pour calculer la durée du rdv
            var prestations = HttpContext.Session.GetString("Prestations");


            // Trouver un créneau dans RDV At Date de la durée calculer précédemment

            return Json(new {});
        }

        [HttpGet("Appointment")]
        public IActionResult Appointment()
        {

            return View();
        }


        [HttpPost("ValidAppointment")]
        public IActionResult ValidAppointment(DateTime dateForm)
        {

            // Stocker la date séléctionné dans une session
            // HttpContext.Session.SetString("Date", System.Text.Json.JsonSerializer.Serialize(date));


            return RedirectToAction("Summary");
        }

        [HttpGet("Summary")]
        public IActionResult Summary()
        {
            // Recupérer les données du cookie
            var sessionPrestationsString = HttpContext.Session.GetString("Prestations");
            //var sessionDate = DateTime HttpContext.Session.GetString("Date");

            var sessionPrestations = System.Text.Json.JsonSerializer.Deserialize<int[]>(sessionPrestationsString);

            // Récupérer le nom de la presta
            var prestations = _context.Prestations.Where(p => sessionPrestations.Contains(p.ID)).ToList();

            // Créer un RDVViewModel
            var rdv  = new RDVViewModel
            {
                Prestation = prestations.Select(p => new PrestationViewModel
                {
                    Name = p.Name,
                    Price = p.Price,

                }).ToList(),
                //At = date,

            };

            return View(rdv);
        }


    }
}

