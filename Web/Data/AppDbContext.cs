using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ArticleTag> ArticleTags { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Social> Socials { get; set; }
		public DbSet<TeamSocial> TeamSocials { get; set; }
		public DbSet<Position> Positions { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
        {
			base.OnModelCreating(builder);
			builder.Entity<User>().ToTable("Users");
			builder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}

