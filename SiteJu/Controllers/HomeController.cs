using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SiteJu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Infrastructure.Mail;
using SiteJu.Configuration;
using SiteJu.Data;
using SiteJu.Areas.Admin.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace SiteJu.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ReservationContext _context;


        public HomeController(ReservationContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index([FromServices]IOptions<Web> options)
        {
            RouteValueDictionary routeValues = this.HttpContext.Request.RouteValues;

            if (HttpContext.Request.Query.ContainsKey("Sent"))
            {
                ViewData["HasContactFormSend"] = Convert.ToBoolean(HttpContext.Request.Query["Sent"].First());
            }

            HomeViewModel VM = new()
            {
                ProfilPicture = options.Value.ProfilPicture,
                Prestations = _context.Prestations.Select(p => new PrestationViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    // Je veux juste afficher la catégorie
                    Category = new PrestationCategoryViewModel { Name = p.Category.Name },
                }).ToList()

            };

            return View(VM);
        }


        [HttpPost("Contact")]
        public async Task<IActionResult> Contact(HomeViewModel vm, [FromServices] IMailSender _mailSender)
        {
            bool result = await _mailSender.SendMail(vm.Contact.Email, $"{vm.Contact.LastName} {vm.Contact.Name}", vm.Contact.Message, $"[Web] Prise de contact : {vm.Contact.LastName} {vm.Contact.Name}");

            return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Home", Action = "Index", Sent=result}));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}

