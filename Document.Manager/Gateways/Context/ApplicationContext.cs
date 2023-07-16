using Document.Manager.Models.Entities;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Document.Manager.Gateways.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<AttachedDocument> AttachedDocuments { get; set; }
}
