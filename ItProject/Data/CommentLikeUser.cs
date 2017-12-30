using ItProject.Models;
using ItProject.Models.ArticleModels;

namespace ItProject.Data
{
    public class CommentLikeUser
    {
        public int CommentId { get; set; }
        public CommentModel Comment { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}