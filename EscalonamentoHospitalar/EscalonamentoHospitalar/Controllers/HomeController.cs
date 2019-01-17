using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Http;

namespace EscalonamentoHospitalar.Controllers
{
    public class HomeController : Controller
    {
        private HospitalDbContext _context;

        public HomeController(HospitalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int hour = DateTime.Now.Hour;

            string message;

            if (hour >= 7 && hour < 12)
            {
                message = "Bom dia";
            }
            else if (hour >= 12 && hour < 20)
            {
                message = "Boa tarde";
            }
            else
            {
                message = "Boa noite";
            }

            ViewBag.Message = message;

            return View(_context.userAccount.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Registar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registar(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                _context.userAccount.Add(user);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = user.Nome + " " + user.Email + " Registado com sucesso";
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (UserAccount user)
        {
            var account = _context.userAccount.Where(u => u.Codigo == user.Codigo && u.Password == user.Password).FirstOrDefault();
            if(account != null)
            {
                HttpContext.Session.SetString("UserId", account.UserId.ToString());
                HttpContext.Session.SetString("Codigo", account.Codigo);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "O código ou a password estão errados.");
            }


                return View();
        } 

        public ActionResult Welcome()
        {
            if(HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Codigo");
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
