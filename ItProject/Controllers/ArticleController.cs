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

        [HttpPost]
        public IActionResult AddRating(ArticleModel article)
        {
            var rating = db.Articles.Find(article.Id).Rating;
            var currentUser = db.Users.Where(user => user.UserName == User.Identity.Name).Single();
            var count = db.ArticleUserRating.Where(articleUserRating => articleUserRating.Article==article).Count();
            var newRating = ((rating * count) + article.Rating) / (count + 1);
            db.Articles.Find(article.Id).Rating = newRating;
            db.ArticleUserRating.Add(new ArticleUserRating(article,currentUser));
            db.SaveChanges();
            return ShowArticle(article.Id);
        }

        private void Like(ApplicationUser user, CommentModel comment)
        {
            if (FiendUser(user,comment))
            {
                var commentLikeUserEntity = db.CommentLikeUser.Find(comment.Id, user.Id);
                db.CommentLikeUser.Remove(commentLikeUserEntity);
                db.SaveChanges();
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
            return View("Article", db.Articles.Find(id));
        } 
    }
}