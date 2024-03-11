using Microsoft.EntityFrameworkCore;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.LocalDatabase
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Diet> Diet { get; set; }
        public string DbPath { get; }

        public RecipeDbContext()
        {
            DbPath = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithMany()
                .UsingEntity(j => j.ToTable("RecipeIngredients"));

            _ = modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Diets)
                .WithMany()
                .UsingEntity(j => j.ToTable("RecipeDiets"));
            base.OnModelCreating(modelBuilder);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlite($"Data Source={DbPath}");
        }
    }

}
