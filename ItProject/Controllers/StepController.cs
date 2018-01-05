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
        
        public IActionResult ShowStep(int id)
        {
            var stepViewModel = new StepViewModel();
            stepViewModel.SetAllParams(db, id);
            return View("Step", stepViewModel);
        }
    }
}