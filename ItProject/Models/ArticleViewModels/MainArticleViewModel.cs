using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class MainArticleViewModel
    {
        public List<ArticleModel> ArticleMaxDate { get; set; }
        public List<ArticleModel> ArticleMaxRating { get; set; }
        public List<Tag> TagCloud { get; set; }
    }
}
