using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.Articles
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        //  public List<string> Tags { get; set; }
        public int ApplicationUsersId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Step> Steps { get; set; }
        public float Rating { get; set; }
        public DateTime Date { get; set; }

        public Article()
        {
            Steps = new List<Step>();
            Comments = new List<Comment>();
        }

    }
}
