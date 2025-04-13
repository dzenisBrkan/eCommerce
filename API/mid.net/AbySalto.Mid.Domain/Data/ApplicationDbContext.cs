using AbySalto.Mid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Domain.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
