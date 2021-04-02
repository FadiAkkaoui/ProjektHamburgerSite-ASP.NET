using System;
using Microsoft.EntityFrameworkCore;
namespace ProjektHB.Models
{
    //Skapar en Databas
    public class BurgerModel : DbContext
    {
        public BurgerModel()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source = BurgerDB.db");
        }
        public DbSet<Burger> Burgers {get; set;}
    }
}
