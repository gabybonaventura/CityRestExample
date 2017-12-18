using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Entities
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            :base(options)
        {
            if (Database.EnsureCreated())
                System.Console.WriteLine("database true");
            else
                System.Console.WriteLine("database false");

        }

        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }

     
}
