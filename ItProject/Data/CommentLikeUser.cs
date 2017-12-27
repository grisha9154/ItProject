using ItProject.Models;
using ItProject.Models.Articles;

namespace ItProject.Data
{
    public class CommentLikeUser
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}