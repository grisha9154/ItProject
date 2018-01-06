using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Models.ArticleModels;
using ItProject.Models;
using ItProject.Data;
using Microsoft.EntityFrameworkCore;
using ItProject.Models.ArticleViewModels;

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

        [HttpPost]
        public IActionResult CreateComment(CommentCreateViewModel comment)
        {
            var currentUser = db.Users.Where(user => user.UserName == User.Identity.Name).Single();
            db.Comments.Add(new CommentModel(comment.Body,currentUser,comment.ArticleId));
            db.SaveChanges();
            return ShowArticle(comment.ArticleId);
        }

        private void Like(ApplicationUser user, CommentModel comment)
        {
            if (FiendUser(user,comment))
            {

            }
            else
            {
                var com = new CommentLikeUser(comment, user);
                db.CommentLikeUser.Add(com);
                db.SaveChanges();
            }
        }

        private bool FiendUser(ApplicationUser user, CommentModel comment)
        {
            var result = false;
            var likeUser  = db.CommentLikeUser.Find(comment.Id, user.Id);
            if(likeUser != null)
            {
                result = true;
            }
            return result;
        }

        [HttpGet]
        public IActionResult AddLike(int commentId)
        {
            var currentUser = db.Users.Where(user => user.UserName == User.Identity.Name).Single();
            Like(currentUser, db.Comments.Find(commentId));
            return ShowArticle(db.Comments.Find(commentId).ArticlesId);
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