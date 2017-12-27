﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Models.Articles;
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
            db.Users.ToList();
            db.Comments.ToList();
            db.Steps.ToList();
            db.CommentLikeUser.ToList();
        }
        [Route("article/{id:int}")]
        public IActionResult ShowArticle(int id)
        {
            ViewBag.Articles = db.Articles.Find(id);
            ViewBag.AllComments = db.Comments.Where(c => c.ArticlesId == id).ToList();
            return View("Article");
        }
    }
}