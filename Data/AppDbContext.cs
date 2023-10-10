using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;

namespace MovieCatalog.Data;
public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Movie> Movies { get; set; }

}