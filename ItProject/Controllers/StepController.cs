using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItProject.Data;
using ItProject.Models.ArticleViewModels;

namespace ItProject.Controllers
{
    public class StepController : Controller
    {
        private ApplicationDbContext db;

        public StepController(ApplicationDbContext application)
        {
            this.db = application;
            db.InitialDBComponent();
        }

        [Route("step/{stepId:int}")]
        public IActionResult ShowStep(int stepId)
        {
            var stepViewModel = new StepViewModel();
            stepViewModel.SetAllParams(db, stepId);
            return View("Step", stepViewModel);
        }
    }
}