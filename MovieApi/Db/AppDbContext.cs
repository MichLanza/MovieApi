using Microsoft.EntityFrameworkCore;
using MovieApi.Entities;

namespace MovieApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }
    }
}
