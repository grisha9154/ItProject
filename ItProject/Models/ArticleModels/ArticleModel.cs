using ItProject.Data;
using ItProject.Models.ArticleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleModels
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public List<TagArticle> Tags { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<StepModel> Steps { get; set; }
        public List<ArticleUserRating> ArticleUserRating { get; set; }
        public float Rating { get; set; }
        public DateTime Date { get; set; }

        public ArticleModel()
        {
            Steps = new List<StepModel>();
            Comments = new List<CommentModel>();
            ArticleUserRating = new List<Data.ArticleUserRating>();
        }
        public ArticleModel (ArticleCreateViewModel article,ApplicationUser user)
        {
            Name = article.Name;
            Description = article.Description;
            Theme = article.Theme;
            ApplicationUser = user;
            Date = DateTime.Now;
            Steps = new List<StepModel>();
            Comments = new List<CommentModel>();
            ArticleUserRating = new List<Data.ArticleUserRating>();
        }
    }
}
