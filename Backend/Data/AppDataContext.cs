using System;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.DataContext;

public class AppDataContext(DbContextOptions options) :IdentityDbContext<AppUser>(options) 
{
    public DbSet<Product> Products{get;set;}
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
}
