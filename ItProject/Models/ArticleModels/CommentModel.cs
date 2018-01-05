using ItProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleModels
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int Like { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<CommentLikeUser> CommentLikeUser { get; set; }
        public int ArticlesId { get; set; }
        public ArticleModel Articles { get; set; }
        
        public CommentModel()
        {
            CommentLikeUser = new List<Data.CommentLikeUser>();
        }

        public CommentModel(string body, ApplicationUser user,int articleId)
        {
            Like = 0;
            Body = body;
            Date = DateTime.Now;
            ApplicationUser = user;
            ArticlesId = articleId;
            CommentLikeUser = new List<Data.CommentLikeUser>();
        }
    }
}
