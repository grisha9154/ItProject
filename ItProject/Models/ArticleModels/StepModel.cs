using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleModels
{
    public class StepModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int ArticleId { get; set; }
        public ArticleModel Article { get; set; } 
        public DateTime Date { get; set; }
    }
}
