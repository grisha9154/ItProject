using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Data
{
    public class TagArticle
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ArticleId { get; set; }
        public ArticleModel Article { get; set; }
    }
}
