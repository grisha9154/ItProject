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

        public CommentLikeUser()
        {
            
        }

        public CommentLikeUser(CommentModel comment, ApplicationUser applicationUser)
        {
            CommentId = comment.Id;
            Comment = comment;
            ApplicationUser = applicationUser;
            ApplicationUserId = applicationUser.Id;
        }
    }
}