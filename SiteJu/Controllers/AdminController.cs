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
        [HttpGet("")]
        public IActionResult Index([FromServices] SignInManager<IdentityUser> SignInManager, [FromServices] UserManager<IdentityUser> UserManager)
        {
            var auth = SignInManager.IsSignedIn(User);
            var usr = UserManager.GetUserName(User);
            return View();
        }

        [HttpGet("Prestations")]
        public IActionResult Prestations([FromServices] ReservationContext context)
        {
            var prestations = context.Prestations.ToList();
            return View(prestations);          
        }

        [HttpGet("CreatePrestation")]
        public IActionResult CreatePrestation()
        {
            return View();
        }

        [HttpPost("CreatePrestation")]
        public IActionResult CreatePrestation(PrestationViewModel prestationVM, [FromServices] ReservationContext context)
        {
            var prestation = new Prestation
            {
                Prestations = prestationVM.Name,
                Price = prestationVM.Price,
                Duration = TimeSpan.FromMinutes(prestationVM.Duration)
            };

            context.Prestations.Add(prestation);
            context.SaveChanges();
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
        public IActionResult EditPrestation([FromQuery]int id, [FromServices] ReservationContext context)
        {
            var prestation = context.Prestations.Find(id);
            var prestVm = new PrestationViewModel
            {
                Id = prestation.ID,
                Duration = Convert.ToInt32(prestation.Duration.TotalMinutes),
                Name = prestation.Prestations,
                Price = prestation.Price
            };
            return View(prestVm);
        }

        [HttpPost("EditPrestation")]
        public IActionResult EditPrestation(PrestationViewModel prestationVM, [FromServices] ReservationContext context)
        {
            var prestation = new Prestation
            {
                ID = prestationVM.Id,
                Prestations = prestationVM.Name,
                Price = prestationVM.Price,
                Duration = TimeSpan.FromMinutes(prestationVM.Duration)
            };

            context.Prestations.Update(prestation);
            context.SaveChanges();

            return Redirect("Prestations");
        }

        [HttpPost("DeletePrestation")]
        public IActionResult DeletePrestation(int id, [FromServices] ReservationContext context)
        {
            context.Prestations.Remove(new Prestation { ID = id });
            context.SaveChanges();

            return Redirect("Prestations");

        }

        [HttpGet("RDV")]
        public IActionResult GetRDV([FromQuery(Name = "start")] DateTime start, [FromQuery(Name = "end")] DateTime end, [FromServices] ReservationContext context)
        {
            var filteredRendezVous = context.RDVS.Include(rdv => rdv.Client).Where(rdv => start <= rdv.At && rdv.At <= end);
            var result = filteredRendezVous.Select(rdv => new
            {
                start = rdv.At,
                end = rdv.At + rdv.Prestation.Duration,
                title = rdv.Client.Firstname,

            });

            return Json(result);
        }

        //[HttpPost("Create")]
        //public IActionResult CreateRDV()
        //{

        //}

    }
}

