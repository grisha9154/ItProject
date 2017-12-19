﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.Articles
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int ArticleId { get; set; }
        public Articles Team { get; set; }
        public string Text { get; set; }
    }
}
