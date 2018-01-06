using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ItProject.Models;
using ItProject.Models.ArticleModels;

namespace ItProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<StepModel> Steps { get; set; }
        public DbSet<Tag>Tags { get; set; }
        public DbSet<CommentLikeUser> CommentLikeUser { get; set; }
        public DbSet<TagArticle> TagArticle { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private IEnumerable<IEnumerable<Object>> FieldToList()
        {
            return new List<IEnumerable<Object>>() { Articles, Comments, Steps, CommentLikeUser,Users,Tags };
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


            builder.Entity<TagArticle>().HasKey(t => new { t.ArticleId, t.TagId });

            builder.Entity<TagArticle>()
                .HasOne(sc => sc.Article)
                .WithMany(s => s.Tags)
                .HasForeignKey(sc => sc.ArticleId);

            builder.Entity<TagArticle>()
                .HasOne(sc => sc.Tag)
                .WithMany(s => s.Articles)
                .HasForeignKey(sc => sc.TagId);
        }
    }
}
