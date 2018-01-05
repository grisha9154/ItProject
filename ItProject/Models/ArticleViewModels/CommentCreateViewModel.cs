using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class CommentCreateViewModel
    {
        public string Body { get; set; }
        public int ArticleId { get; set; }
    }
}
