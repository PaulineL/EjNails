using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SiteJu.Areas.Admin.Models;
using SiteJu.Data;
using SiteJu.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiteJu.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(SignInManager<Client> signinManager, ILogger<UserController> logger, UserManager<Client> userManager)
        {
            _signInManager = signinManager;
            _userManager = userManager;
            _logger = logger;
        }


        [Route("/")]
        public IActionResult Index()
        {
            return RedirectToActionPermanent("login");
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> LoginPost(ClientViewModel vm)
        {
            var returnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true@
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Route("/register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterPost(ClientViewModel user)
        {
            //Il faut verifier que toute les infos sont bien ici
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.Email == null)
            {
                return RedirectToAction("register");
            }

            var exist = await _userManager.FindByEmailAsync(user.Email) != null;
            if (!exist)
            {
                Client identityUser = new Client
                {
                    Email = user.Email,
                    UserName = user.Lastname + "_" + user.Firstname,
                    PhoneNumber = user.Telephone,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    EmailConfirmed = true,  //On dit que le compte est activé pour le moment, on fera la validation des comptes dans un second temps
                };
                var creationResult = await _userManager.CreateAsync(identityUser, user.Password);

                if (creationResult.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(identityUser, user.Password, true, false);
                    return View("RegisterSuccessful"); // Le création du compte a fonctionnée, rediriger vers une page qui indique le compte est bien creer
                }
                else
                {
                    return View("Error", new ErrorViewModel() { Message = String.Join("; ", creationResult.Errors.Select(e => e.Description)) }); // on affiche une page d'erreur 
                }
            }

            return View("Error", "L'utilisateur existe déjà");
        }

        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //allah redirection, on redirige vers la page de login
            return RedirectToAction("login");
        }
    }
}

