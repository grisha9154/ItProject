using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class ArticleCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Tags { get; set; }
    }
}
