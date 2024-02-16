using Microsoft.EntityFrameworkCore;
using WinFormsInfoApp.Models;
using System;
using System.Collections.Generic;

namespace WinFormsInfoApp
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Ingredient> Ingreidient { get; set; }

        public string DbPath { get; }

        public RecipeDbContext()
        {
            DbPath = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

}
