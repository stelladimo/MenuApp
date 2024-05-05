using Microsoft.EntityFrameworkCore;
using MenuApp.Models;
namespace MenuApp.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });
            modelBuilder.Entity<DishIngredient>().HasOne(d=>d.Dish).WithMany(di => di.DishIngredients).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(d => d.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Margaritta", Price = 7.50, ImageUrl = "https://images.app.goo.gl/xiCeAybCa2cU1kr9A" }
                ) ;
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomato Sauce" },
                new Ingredient { Id = 2, Name = "Mozarella" },
                new Ingredient { Id = 3, Name = "Regato" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 }
                 );




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<DishIngredient> DishIngredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Menu_App; Integrated Security = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }
    }
}
