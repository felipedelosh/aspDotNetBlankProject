using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace MyFirstAPI.Infraestructure
{
    public class PersitenceContext : DbContext
    {

        public PersitenceContext(DbContextOptions<PersitenceContext> options) : base(options)
        {

        }

        //Mapers
        public DbSet<Example> Persons { get; set; }

        //Create entity in DB
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Example>().ToTable("examples");
        }

    }
}
