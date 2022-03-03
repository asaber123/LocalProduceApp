using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LocalProduceApp.Models;


namespace LocalProduceApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Produce> Produce { get; set; }
    public DbSet<Producer> Producer { get; set; }
}
