using Microsoft.EntityFrameworkCore;

namespace Chess_Backend.Domains;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    // public DbSet<Room> Rooms { get; set; }
}