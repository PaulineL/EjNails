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
            var selectedPrestaIds = rdvForm
                .Where(sp => sp.IsSelected)
                .Select(so => so.Id)
                .ToList();
            IQueryable<Prestation> prestations = _context.Prestations.Where(p => selectedPrestaIds.Contains(p.ID));

            // Stocker prestations séléctionnées dans une session
            HttpContext.Session.SetString("Prestations", System.Text.Json.JsonSerializer.Serialize(selectedPrestaIds));

            return RedirectToAction("Appointment");
        }


        // Get all apppointments
        [HttpGet("SearchAppointment")]
        public IActionResult SearchAppointment([FromQuery(Name = "date")]DateTime date)
        {

            // Services selected for calcul the duration
            var prestationIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("Prestations"));

            // Search all appointments on the selected date
            var rdvAtDate = _context.RDVS
                .Where(r => r.At.Date == date.Date) // Appointments filter by selected date
                .Select(rdv => new
                {
                    At = rdv.At,
                    duration = rdv.Prestations.Select(presta => presta.Duration.TotalMilliseconds),
                }).ToList();

            // Sum of duration service
            var durationA = TimeSpan.FromMilliseconds(rdvAtDate.Sum(p => p.duration.Sum()));


            // Search duration of the selected service in BDD
            var durationMs = _context.Prestations
                .Where(d => prestationIds.Contains(d.ID)) // Filter by Id
                .Select(duration => duration.Duration.TotalMilliseconds).ToList();

            // initalizing values
            var duration = TimeSpan.FromMilliseconds(durationMs.Sum());
            int startWorkHour = 8, endWorkHour = 18, timeSlotDuration = 30;
            var minutesWorkDay = (endWorkHour - startWorkHour) * 60;
            var timeSlotCountDay = minutesWorkDay / timeSlotDuration;


            // ---- Découper les (endWorkHour - startWorkHour) en créneaux de timeSlot minutes
            var slotOccupied = rdvAtDate.Select(p => new
            {
                slotStartIdx = p.At.AddHours(-startWorkHour).TimeOfDay.TotalMinutes / timeSlotDuration,
                slotCount = (int)TimeSpan.FromMilliseconds(p.duration.Sum()).TotalMinutes / timeSlotDuration
            });


            // Trouver un créneau dans RDV At Date de la durée calculée précédemment
            var slotAvailable = new List<DateTime>(timeSlotCountDay);
            for (int i = 0; i < timeSlotCountDay; i++)
            {
                var appointment = slotOccupied.FirstOrDefault(p => p.slotStartIdx == i);
                if (appointment == null)
                {
                    var start = i * (timeSlotDuration / 60d);
                    slotAvailable.Add(
                        new DateTime(
                            date.Year,
                            date.Month,
                            date.Day,
                            (int)Math.Truncate(start) + startWorkHour,
                            (int)Math.Abs(Math.Min(Math.Truncate(start) - start, 0) * 60),
                            0
                        )
                    );
                }
                else
                {
                    i += appointment.slotCount;
                }
            }

            return Json(slotAvailable);
        }

        [HttpGet("Appointment")]
        public IActionResult Appointment()
        {
            return View();
        }

        // Get the date in form
        [HttpPost("ValidAppointment")]
        public IActionResult ValidAppointment(DateTime app)
        {


            // Stocker la date séléctionné dans une session
            HttpContext.Session.SetString("Appointment", app.ToString());

            return RedirectToAction("Summary");
        }

        [HttpGet("Summary")]
        public IActionResult Summary()
        {
            // Recupérer les données du cookie
            //var sessionPrestationsString = HttpContext.Session.GetString("Prestations");
            //var sessionAppointmentString = HttpContext.Session.GetString("Appointment");

            //var sessionPrestations = System.Text.Json.JsonSerializer.Deserialize<int[]>(sessionPrestationsString);
            //var sessionAppointment = System.Text.Json.JsonSerializer.Deserialize<DateTime>(sessionAppointmentString);

            //// Récupérer le nom de la presta
            //var prestations = _context.Prestations.Where(p => sessionPrestations.Contains(p.ID)).ToList();

            //// Créer un RDVViewModel
            //var rdv  = new RDVViewModel
            //{
            //    Prestation = prestations.Select(p => new PrestationViewModel
            //    {
            //        Name = p.Name,
            //        Price = p.Price,

            //    }).ToList(),
            //    At = sessionAppointment,

            //};

            return View();
        }


    }
}

