using ItProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.Articles
{
    public class Comment
    {
        public int Id { get; set; }
        public int Like { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<CommentLikeUser> CommentLikeUser { get; set; }
        public int ArticlesId { get; set; }
        public Article Articles { get; set; }
    }
}
