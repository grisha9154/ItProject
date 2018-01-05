using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class ArticleUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
    }
}
