using System;
using Microsoft.EntityFrameworkCore;
using MyGrocery.WebApi.Models;

namespace MyGrocery.WebApi.Data
{
	public class AppDbContext:DbContext 
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{
		}

		public DbSet<Grocery> Groceries => Set<Grocery>();
	}
}

