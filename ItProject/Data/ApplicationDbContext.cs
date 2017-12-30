using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ItProject.Models;
using ItProject.Models.Articles;

namespace ItProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<CommentLikeUser> CommentLikeUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private IEnumerable<IEnumerable<Object>> FieldToList()
        {
            return new List<IEnumerable<Object>>() { Articles, Comments, Steps, CommentLikeUser };
        }

        public void InitialDBComponent()
        {
            foreach(var list in FieldToList())
            {
                list.ToList();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CommentLikeUser>().HasKey(t => new { t.CommentId, t.ApplicationUserId });

            builder.Entity<CommentLikeUser>()
            .HasOne(sc => sc.Comment)
            .WithMany(s => s.CommentLikeUser)
            .HasForeignKey(sc => sc.CommentId);

            builder.Entity<CommentLikeUser>()
            .HasOne(sc => sc.ApplicationUser)
            .WithMany(c => c.CommentLikeUser)
            .HasForeignKey(sc => sc.ApplicationUserId);
        }
    }
}
