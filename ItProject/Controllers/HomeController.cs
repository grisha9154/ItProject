using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Models;
using ItProject.Models.ArticleModels;
using ItProject.Data;
using Microsoft.EntityFrameworkCore;

namespace ItProject.Controllers
{
    public class HomeController : Controller
    {
        List<ArticleModel> a = new List<ArticleModel>
        {
            new ArticleModel
            {
                Id = 1,
                Name = "Работа с WPF",
                Rating = 5,
                Description = "Короткий, курс описывающий как парвильно создавать WPF приложжения.",
                Theme = "С#",
                Steps = new List<StepModel>{new StepModel { Id =0, Name="qwe",Body = "qwe", ArticleId = 0}},
                ApplicationUser = new ApplicationUser{Id ="2", UserName = "Петров"},
                Date = DateTime.Now                
            }
        };

        private ApplicationDbContext db;
        public HomeController(ApplicationDbContext application)
        {
            this.db = application;
        }
        public async Task<IActionResult> Index()
        {
            db.Users.ToList();
            ViewBag.Articles = await db.Articles.ToListAsync();
            return View();
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
