using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalonamentoHospitalar.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EscalonamentoHospitalar.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        // Get all roles
        public IActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        // Create a new role
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}