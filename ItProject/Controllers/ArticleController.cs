using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Models.ArticleModels;
using ItProject.Models;
using ItProject.Data;
using Microsoft.EntityFrameworkCore;

namespace ItProject.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db;

        public ArticleController (ApplicationDbContext application)
        {
            this.db = application;
            db.Articles.ToList();
            db.InitialDBComponent();
        }

        [Route("article/{id:int}")]
        public IActionResult ShowArticle(int id)
        {
            ViewBag.Articles = db.Articles.Find(id);
            db.Users.ToList();
            ViewBag.AllComments = db.Comments.Where(c => c.ArticlesId == id).ToList();
            ViewBag.AllSteps = db.Steps.Where(s => s.ArticleId == id).ToList();
            return View("Article");
        } 
    }
}