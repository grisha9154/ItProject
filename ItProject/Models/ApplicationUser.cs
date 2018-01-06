using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ItProject.Data;
using ItProject.Models.ArticleModels;

namespace ItProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<ArticleModel> Articles { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<CommentLikeUser> CommentLikeUser { get; set; }
        public List<ArticleUserRating> ArticleUserRating { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        
        public ApplicationUser()
        {
            Articles = new List<ArticleModel>();
            Comments = new List<CommentModel>();
            CommentLikeUser = new List<Data.CommentLikeUser>();
        }
    }
}
