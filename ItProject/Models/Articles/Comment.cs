﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.Articles
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticlesId { get; set; }
        public Article Articles { get; set; }
    }
}
