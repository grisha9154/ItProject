using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.Articles
{
    public class Articles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Theme { get; set; }
      //  public List<string> Tags { get; set; }
        public List<Step> Steps { get; set; }
    }
}
