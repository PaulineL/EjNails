using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteJu.Areas.Admin.Models;
using SiteJu.Data;
using SiteJu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        [HttpGet("ValidClient")]
        public IActionResult ValidClient()
        {
            return RedirectToAction("Services");
        }

        [HttpGet("Services")]
        public IActionResult Services()
        {
            var prestations = _context.Prestations.Select(p => new PrestationViewModel
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = new PrestationCategoryViewModel { Name = p.Category.Name },
            }).ToList();


            return View("Services", prestations);
        }

        [HttpPost("ValidServices")]
        public IActionResult ValidServices(IEnumerable<PrestationViewModel> rdvForm)
        {

            // Id prestations selected by client
            var selectedServicesIds = rdvForm
                .Where(sp => sp.IsSelected)
                .Select(so => so.Id)
                .ToList();
            IQueryable<Prestation> prestations = _context.Prestations.Where(p => selectedServicesIds.Contains(p.ID));

            // Stocker prestations séléctionnées dans une session
            HttpContext.Session.SetString("Services", System.Text.Json.JsonSerializer.Serialize(selectedServicesIds));

            return RedirectToAction("Appointment");
        }


        /// <summary>
        ///     Get all available apppointments for a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("SearchAppointment")]
        public IActionResult SearchAppointment([FromQuery(Name = "date")]DateTime date)
        {

            var serviceIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("Services"));

            double durationMs = GetServiceDuration(serviceIds);

            List<DateTime> slotAvailable = GetAvailableSlots(date, durationMs);

            return Json(slotAvailable);
        }

        [HttpGet("Appointment")]
        public IActionResult Appointment()
        {
            return View();
        }

        // Get the date in form
        [HttpPost("ValidAppointment")]
        public IActionResult ValidAppointment(DateTime slotTime)
        {

            // Stocker la date séléctionné dans une session
            HttpContext.Session.SetString("Appointment", slotTime.ToString());

            return RedirectToAction("Summary");
        }

        [HttpGet("Summary")]
        public IActionResult Summary()
        {
            // Recupérer les données du cookie
            var sessionPrestationsString = HttpContext.Session.GetString("Services");
            var sessionAppointmentString = HttpContext.Session.GetString("Appointment");

            if (string.IsNullOrEmpty(sessionPrestationsString)|| string.IsNullOrEmpty(sessionAppointmentString))
            {
                throw new ArgumentException();
            }

            var prestationIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(sessionPrestationsString);
            var slotTime = DateTime.Parse(sessionAppointmentString);


            // Récupérer le nom de la presta
            var prestations = _context.Prestations.Where(p => prestationIds.Contains(p.ID)).ToList();

            // Créer un RDVViewModel
            
            // * Il faut que je m'occupe des clients également * //s

            var rdv  = new RDVViewModel
            {
                Prestation = prestations.Select(p => new PrestationViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Duration = Convert.ToInt32(p.Duration.TotalMinutes),
                }).ToList(),
                At = slotTime,
            };


            return View(rdv);
        }

        [HttpGet("ValidRDV")]
        public IActionResult ValidRDV()
        {
            return View();

        }

        [HttpPost("ValidRDV")]
        public IActionResult ValidRDV(RDVViewModel rdvForm)
        {
            // Avant d'enregistrer le RDV, verifier une derniere fois que le creneau est toujours disponible,
            // pour gerer les accès concurrent au créneau
            var serviceIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("Services"));

            double durationMs = GetServiceDuration(serviceIds);

            var availableSlots = GetAvailableSlots(rdvForm.At.Date, durationMs);

            foreach(var slot in availableSlots)
            {

                
            }

            /////////////////////////////////////////
            /////////////////////////////////////////
            ///
            ///   Verifier si le creneau est toujours dispo ici
            /// 
            /////////////////////////////////////////
            /////////////////////////////////////////




            // Recupérer les données du cookie
            var sessionPrestationsString = HttpContext.Session.GetString("Services");
            var sessionAppointmentString = HttpContext.Session.GetString("Appointment");

            if (string.IsNullOrEmpty(sessionPrestationsString) || string.IsNullOrEmpty(sessionAppointmentString))
            {
                throw new ArgumentException();
            }

            var prestationIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(sessionPrestationsString);
            var slotTime = DateTime.Parse(sessionAppointmentString);


            // Récupérer le nom de la presta
            var prestations = _context.Prestations.Where(p => prestationIds.Contains(p.ID)).ToList();

            // Attention : ne pas oublier les clients
            var appointment = new RDV
            {
                Prestations = prestations.ToList(),
                At = slotTime,

            };

            _context.RDVS.Add(appointment);
            _context.SaveChanges();

            if (appointment.Id != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(appointment);
            }

        }

        private double GetServiceDuration(int[] serviceIds)
        {
            // Search duration of the selected service in BDD
            var durationMs = _context.Prestations
                .Where(d => serviceIds.Contains(d.ID)) // Filter by Id
                .Select(duration => duration.Duration.TotalMilliseconds).Sum();
            return durationMs;
        }

        private List<DateTime> GetAvailableSlots(DateTime date, double durationMs) //remplacer en TimeSpan 'durationMs'
        {

            // Search all appointments on the selected date
            var rdvAtDate = _context.RDVS
                .Where(r => r.At.Date == date.Date) // Appointments filter by selected date
                .Select(rdv => new
                {
                    At = rdv.At,
                    duration = rdv.Prestations.Select(presta => presta.Duration.TotalMilliseconds),
                }).ToList();

            // initalizing values
            var duration = TimeSpan.FromMilliseconds(durationMs);
            int startWorkHour = 8, endWorkHour = 18, timeSlotDuration = 15;
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

            return slotAvailable;
        }


    }
}

