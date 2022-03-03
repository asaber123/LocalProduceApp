using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LocalProduceApp.Models;


namespace LocalProduceApp.Data;

public class LocalProduceAppDbContext : IdentityDbContext
{
    public LocalProduceAppDbContext(DbContextOptions<LocalProduceAppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Produce> Produce { get; set; }
    public DbSet<Producer> Producer { get; set; }
}
