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
using ItProject.Models.ArticleViewModels;

namespace ItProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController(ApplicationDbContext application)
        {
            this.db = application;
            db.InitialDBComponent();
        }
        public IActionResult Index()
        {
            var model = new MainArticleViewModel();
            model.ArticleMaxDate = db.Articles.OrderByDescending(a=>a.Date).ToList();
            model.ArticleMaxRating = db.Articles.OrderByDescending(a => a.Rating).ToList();
            model.TagCloud = db.Tags.OrderByDescending(a => a.Id).ToList();
            return View(model);
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
