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

namespace SiteJu.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index([FromServices]IOptions<Web> options)
        {
            HomeViewModel VM = new()
            {
                ProfilPicture = options.Value.ProfilPicture
            };

            return View(VM);
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> Contact(HomeViewModel vm, [FromServices] IMailSender _mailSender)
        {
            bool result = await _mailSender.SendMail(vm.Contact.Email, $"{vm.Contact.LastName} {vm.Contact.Name}", vm.Contact.Message, $"[Web] Prise de contact : {vm.Contact.LastName} {vm.Contact.Name}");

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

