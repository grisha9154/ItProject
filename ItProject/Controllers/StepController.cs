using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Data;

namespace ItProject.Controllers
{
    public class StepController : Controller
    {
        private ApplicationDbContext db;

        public StepController(ApplicationDbContext application)
        {
            this.db = application;
            db.Articles.ToList();
            db.InitialDBComponent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}