using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SiteJu.Configuration;
using SiteJu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SiteJu.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IOptions<Mail> _mailOptions { get; }
        public ISendGridClient _sendgridClient { get; }

        public HomeController(IOptions<Mail> mailOptions, ISendGridClient sendgridClient)
        {
            _mailOptions = mailOptions;
            _sendgridClient = sendgridClient;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.HasContactFormSend = true;
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> EnvoyerContact(Contact contact)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress("paulinelopez31650@gmail.com", contact.Name + contact.LastName),
                PlainTextContent = contact.Message,
                Subject = "Prise de contact : EJ Nails",
                ReplyTo = new EmailAddress(contact.Email, contact.Name + " " + contact.LastName)
            };
            msg.AddTo(new EmailAddress(_mailOptions.Value.Contact));

            var response = await _sendgridClient.SendEmailAsync(msg);
            var bodyresp = response.Body.ReadAsStringAsync().Result;

            return RedirectToAction("Index");

   
        }


        [HttpGet("Data")]
        public IActionResult Data()
        {
            var viewModel = new DataViewModel
            {
                Name = "Powline",
                LastName = "LowPez",
                BirthDate = DateTime.Parse("05/12/1991"),
                Passion = new List<string> { "Bouffe", "Manger", "Dévorer", "Nouriture" }
            };
            return View(viewModel);
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
