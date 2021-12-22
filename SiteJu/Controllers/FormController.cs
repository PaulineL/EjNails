using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteJu.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EJ_nails.Controllers
{
    [Route("/users")]
    public class FormController : Controller
    {
        List<User> _users;
        public FormController(List<User> users)
        {
            _users = users;
        }

        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult Index()
        {
            return View(_users);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult AddUser(User user)
        {
            user.Id = _users.Count() + 1;
            _users.Add(user);
            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            User update = _users.FirstOrDefault(usr => usr.Id == id);
            return View(update);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, User user)
        {
            User update = _users.FirstOrDefault(usr => usr.Id == id);

            update.LastName = user.LastName;
            update.Name = user.Name;
            update.Role = user.Role;
            update.UserName = user.UserName;

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _users.RemoveAt(id - 1);
            return RedirectToAction("Index");
        }
    }
}
