﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteJu.Areas.Admin.Models;
using SiteJu.Data;
using SiteJu.Models;
using System;
using System.Linq;

namespace SiteJu.Areas.Admin.Controllers
{
    [Authorize("Admin")]
    [Route("/[area]")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ReservationContext _context;

        public HomeController(ReservationContext context)
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
            var prestations = _context.Prestations.Select(p => new PrestationViewModel
            {
                Id = p.ID,
                Duration = Convert.ToInt32(p.Duration.TotalMinutes),
                IsSelected = false,
                Name = p.Name,
                Price = p.Price,
                Options = p.OptionsAvailable.Select(po => new PrestationOptionViewModel
                {
                    Id = po.ID,
                    Duration = Convert.ToInt32(po.AdditionalTime.TotalMinutes),
                    IsSelected = false,
                    MaxQuantity = po.MaxAvailable,
                    Name = po.Name,
                    Price = po.AdditionalPrice,
                    Quantity = 0
                }).ToList()

            });
            return View(prestations);
        }

        [HttpGet("CreatePrestation")]
        public IActionResult CreatePrestation()
        {
            PrestationViewModel vm = new PrestationViewModel();
            vm.Options = _context.PrestationOptions.Select(p => new PrestationOptionViewModel
            {
                Id=p.ID,
                Duration = Convert.ToInt32(p.AdditionalTime.TotalMinutes),
                IsSelected=false,
                MaxQuantity= p.MaxAvailable, 
                Name= p.Name,
                Price = p.AdditionalPrice,
                Quantity= 0
            }).ToList();
            return View(vm);
        }

        [HttpPost("CreatePrestation")]
        public IActionResult CreatePrestation(PrestationViewModel prestationVM)
        {
            var selectedOptionIds = prestationVM.Options.Where(so => so.IsSelected).Select(so => so.Id).ToList();
            var options = _context.PrestationOptions.Where(po => selectedOptionIds.Contains(po.ID));

            var prestation = new Prestation
            {
                Name = prestationVM.Name,
                Price = prestationVM.Price,
                Duration = TimeSpan.FromMinutes(prestationVM.Duration),
                OptionsAvailable = options.ToList(),
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
        public IActionResult EditPrestation([FromQuery] int id)
        {
            var prestation = _context.Prestations.Include(p => p.OptionsAvailable).FirstOrDefault(p => p.ID == id);
            var allOptions = _context.PrestationOptions.ToList();
            var prestVm = new PrestationViewModel
            {
                Id = prestation.ID,
                Duration = Convert.ToInt32(prestation.Duration.TotalMinutes),
                Name = prestation.Name,
                Price = prestation.Price,
                Options = allOptions.Select(po => new PrestationOptionViewModel
                {
                    Id = po.ID,
                    Name = po.Name,
                    IsSelected = prestation.OptionsAvailable.Any(p => p.ID == po.ID),
                }).ToList(),
            };
            return View(prestVm);
        }

        [HttpPost("EditPrestation")]
        public IActionResult EditPrestation(PrestationViewModel prestationVM)
        {
            var selectedOptionIds = prestationVM.Options.Where(so => so.IsSelected).Select(so => so.Id).ToList();
            var options = _context.PrestationOptions.Where(po => selectedOptionIds.Contains(po.ID)).ToList();
            var presta = _context.Prestations.Include(p=> p.OptionsAvailable).FirstOrDefault(p => p.ID == prestationVM.Id);

            presta.Name = prestationVM.Name;
            presta.Price = prestationVM.Price;
            presta.Duration = TimeSpan.FromMinutes(prestationVM.Duration);
            foreach (var opt in prestationVM.Options)
            {
                //si l'option n'est pas sélectioné dans le formulaire mais est present dans la liste des options compatible, on la supprime
                if (!opt.IsSelected && presta.OptionsAvailable.Any(oa => oa.ID == opt.Id))
                {
                    presta.OptionsAvailable.Remove(presta.OptionsAvailable.First(oa => oa.ID == opt.Id));
                }
                //sinon si l'option est sélectioné dans le formulaire mais n'est pas present dans la liste des options compatible, on l'ajoute
                else if (opt.IsSelected && !presta.OptionsAvailable.Any(oa => oa.ID == opt.Id))
                {
                    presta.OptionsAvailable.Add(options.First(o => o.ID == opt.Id));
                }
            }

            _context.Prestations.Update(presta);
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
            var clients = _context.Clients.Select(client => new ClientViewModel
            {
                Email = client.Email,
                Firstname = client.Firstname,
                ID = client.ID,
                Lastname = client.Lastname,
                Telephone = client.Telephone,
            });
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
