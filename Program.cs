using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ef_core_fk_model
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var appDbContext = new AppDbContext())
            {
                appDbContext.Database.EnsureDeleted();
                appDbContext.Database.EnsureCreated();
                Console.WriteLine("The database has been reset. Check SSMS to see what the model is like.");
            }
        }
    }
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Trip> Trips { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server=(localdb)\{nameof(ef_core_fk_model)};Database={nameof(ef_core_fk_model)};");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }

    public class Trip
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public int DistanceInKilometers { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}
