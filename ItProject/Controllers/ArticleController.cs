using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Models.Articles;
using ItProject.Models;
using ItProject.Data;

namespace ItProject.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db;

        public ArticleController (ApplicationDbContext application)
        {
            this.db = application;
            db.Articles.ToList();
            db.Users.ToList();
        }
        [Route("article/{id:int}")]
        public IActionResult ShowArticle(int id)
        {
            ViewBag.Articles = db.Articles.Find(id);
            return View("Article");
        }
    }
}