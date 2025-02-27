using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace MyFirstAPI.Infraestructure
{
    public class PersitenceContext : DbContext
    {

        public PersitenceContext(DbContextOptions<PersitenceContext> options) : base(options)
        {

        }

        public PersitenceContext()
        {
        }

        //Mapers
        public DbSet<Example> examples { get; set; }

        //Create entity in DB
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Example>().ToTable("examples");
        }

    }
}
