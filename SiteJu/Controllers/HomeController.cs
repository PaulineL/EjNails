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

namespace SiteJu.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> Contact(Contact contact, [FromServices] IMailSender _mailSender)
        {
            bool result = await _mailSender.SendMail(contact.Email, $"{contact.LastName} {contact.Name}", contact.Message);

            if (result)
            {
                ViewData["HasContactFormSend"] = true;

            }
            else
            {
                ViewData["HasMailError"] = true;
            }

            return View("Index");
   
        }

        [HttpGet("Prestations")]
        public IActionResult Prestations()
        {
            return View();
        }

        [HttpGet("Realisations")]
        public IActionResult Realisations()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
