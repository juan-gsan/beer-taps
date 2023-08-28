using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeerTap.Models;

public partial class BeerTapsContext : DbContext
{
    
    public DbSet<Dispenser> Dispenser { get; set; }
    public DbSet<DispenserUsage> DispenserUsage { get; set; }

    public BeerTapsContext()
    {
        Dispenser = Set<Dispenser>();
        DispenserUsage = Set<DispenserUsage>();
    }

    public BeerTapsContext(DbContextOptions<BeerTapsContext> options)
        : base(options)
    {
        Dispenser = Set<Dispenser>();
        DispenserUsage = Set<DispenserUsage>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
