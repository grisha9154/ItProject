using ItProject.Data;
using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class StepViewModel
    {
        public StepModel Step { get; set; }
        public List<StepModel> ListOfStep {
            get
            {
                return Step.Article.Steps;
            }
        }
        public List<CommentModel> ListOfComment {
            get
            {
                return Step.Article.Comments;
            }
        }

        public void SetAllParams(ApplicationDbContext db, int stepId)
        {
            Step = db.Steps.Find(stepId);
        }
    }  
}