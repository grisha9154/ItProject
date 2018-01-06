using ItProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleModels
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagArticle> Articles { get; set; }

        public Tag()
        {
            Articles = new List<TagArticle>();
        }

        public Tag(string name)
        {
            Name = name;
            Articles = new List<TagArticle>();
        }
    }
}
