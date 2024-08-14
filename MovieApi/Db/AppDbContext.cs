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

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(x => new { x.IdGenre, x.IdMovie });
            modelBuilder.Entity<MovieActor>()
                .HasKey(x => new { x.IdActor, x.IdMovie });

            base.OnModelCreating(modelBuilder);
        }

    }
}
