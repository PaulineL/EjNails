using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteJu.Data;
using SiteJu.Models;

namespace SiteJu.Controllers
{
    [Route("/admin")]
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly ReservationContext _context;

        public AdminController(ReservationContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Prestations")]
        public IActionResult Prestations()
        {
            var prestations = _context.Prestations.ToList();
            return View(prestations);          
        }

        [HttpGet("CreatePrestation")]
        public IActionResult CreatePrestation()
        {
            return View();
        }

        [HttpPost("CreatePrestation")]
        public IActionResult CreatePrestation(PrestationViewModel prestationVM)
        {
            var prestation = new Prestation
            {
                Name = prestationVM.Name,
                Price = prestationVM.Price,
                Duration = TimeSpan.FromMinutes(prestationVM.Duration)
            };

            _context.Prestations.Add(prestation);
            _context.SaveChanges();
            if (prestation.ID != 0)
            {
                return Redirect("Prestations");
            }
            else
            {
                return View(prestation);
            }
        }



        [HttpGet("EditPrestation")]
        public IActionResult EditPrestation([FromQuery]int id)
        {
            var prestation = _context.Prestations.Find(id);
            var prestVm = new PrestationViewModel
            {
                Id = prestation.ID,
                Duration = Convert.ToInt32(prestation.Duration.TotalMinutes),
                Name = prestation.Name,
                Price = prestation.Price
            };
            return View(prestVm);
        }

        [HttpPost("EditPrestation")]
        public IActionResult EditPrestation(PrestationViewModel prestationVM)
        {
            var prestation = new Prestation
            {
                ID = prestationVM.Id,
                Name = prestationVM.Name,
                Price = prestationVM.Price,
                Duration = TimeSpan.FromMinutes(prestationVM.Duration)
            };

            _context.Prestations.Update(prestation);
            _context.SaveChanges();

            return Redirect("Prestations");
        }

        [HttpPost("DeletePrestation")]
        public IActionResult DeletePrestation(int id)
        {
            _context.Prestations.Remove(new Prestation { ID = id });
            _context.SaveChanges();

            return Redirect("Prestations");

        }

        [HttpGet("Clients")]
        public IActionResult Clients()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }
        [HttpGet("CreateClient")]
        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost("CreateClient")]
        public IActionResult CreateClient(ClientViewModel clientVM)
        {
            var client = new Client
            {
                Firstname = clientVM.Firstname,
                Lastname = clientVM.Lastname,
                Telephone = clientVM.Telephone,
                Email = clientVM.Email,
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
            if (client.ID != 0)
            {
                return Redirect("Clients");
            }
            else
            {
                return View(client);
            }
        }

        [HttpGet("EditClient")]
        public IActionResult EditClient([FromQuery] int id)
        {
            var client = _context.Clients.Find(id);
            var clientVm = new ClientViewModel
            {
                ID = client.ID,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Telephone = client.Telephone,
                Email = client.Email,
            };
            return View(clientVm);
        }

        [HttpPost("EditClient")]
        public IActionResult EditClient(ClientViewModel clientVM)
        {
            var client = new Client
            {
                ID = clientVM.ID,
                Firstname = clientVM.Firstname,
                Lastname = clientVM.Lastname,
                Telephone = clientVM.Telephone,
                Email = clientVM.Email,
            };

            _context.Clients.Update(client);
            _context.SaveChanges();

            return Redirect("Clients");
        }

        [HttpPost("DeleteClient")]
        public IActionResult DeleteClient(int id)
        {
            _context.Clients.Remove(new Client { ID = id });
            _context.SaveChanges();

            return Redirect("Clients");

        }

        [HttpGet("RDV")]
        public IActionResult GetRDV([FromQuery(Name = "start")] DateTime start, [FromQuery(Name = "end")] DateTime end)
        {
            var filteredRendezVous = _context.RDVS.Include(rdv => rdv.Client).Where(rdv => start <= rdv.At && rdv.At <= end);
            var result = filteredRendezVous.Select(rdv => new
            {
                start = rdv.At,
                duration = rdv.Prestations.Select(presta => presta.Duration),
                title = rdv.Client.Firstname + " " + rdv.Client.Lastname,
            }).ToList();

            return Json(result.Select(rdv => new
            {
                start = rdv.start,
                end = rdv.start + new TimeSpan(rdv.duration.Sum(p => p.Ticks)),
                title = rdv.title
            }));
        }

        [HttpGet("CreateRDV")]
        public IActionResult CreateRDV()
        {
            var prestations = _context.Prestations.ToList();

            var res = new RDVViewModel();
            // Transforme les prestations de la BDD en PrestationViewModel
            res.Prestation = prestations.Select(prest => new PrestationViewModel
            {
                Id = prest.ID,
                Name = prest.Name
            }).ToList();

            return View(res);
        }
    }
}

