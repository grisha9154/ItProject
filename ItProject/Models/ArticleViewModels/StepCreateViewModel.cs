using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class StepCreateViewModel
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public int ArticleId {get; set; }
    }
}
