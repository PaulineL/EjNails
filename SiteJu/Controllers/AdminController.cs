using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteJu.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiteJu.Controllers
{
    [Route("/admin")]
    //[Authorize]
    public class AdminController : Controller
    {
        [HttpGet("")]
        public IActionResult Index([FromServices] ReservationContext context)
        {
            return View();
        }

        [HttpGet("Office")]
        public IActionResult Office()
        {
            return View();
        }

        [HttpGet("RDV")]
        public IActionResult GetRDV([FromQuery(Name = "start")] DateTime start, [FromQuery(Name = "end")] DateTime end, [FromServices] ReservationContext context)
        {
            var filteredRendezVous = context.RDVS.Include(rdv => rdv.Client).Where(rdv => start  <= rdv.At && rdv.At <= end);
            return Json(filteredRendezVous);
        }
    }
}

