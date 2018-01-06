using ItProject.Models;
using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Data
{
    public class ArticleUserRating
    {
        public int ArticleId { get; set; }
        public ArticleModel Article { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public ArticleUserRating()
        {

        }
        public ArticleUserRating(ArticleModel article, ApplicationUser user)
        {
            ArticleId = article.Id;
            Article = article;
            UserId = user.Id;
            User = user;
        }
    }
}
