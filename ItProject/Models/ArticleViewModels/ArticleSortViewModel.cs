using ItProject.Models.ArticleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ArticleViewModels
{
    public class ArticleSortViewModel
    {
        public string Name { get; set; }
        public string SortType { get; set; }
        public ArticleSortViewModel(string name, string sort)
        {
            Name = name;
            SortType = sort;
        }
        public ArticleSortViewModel()
        {

        }
    }
}
