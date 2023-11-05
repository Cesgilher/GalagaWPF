using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using GalagaWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DBContext : DbContext
{
  
    public DbSet<User> Users;
    public DbSet<Ship> Ships;
    public DbSet<Background> Backgrounds;
    public DbSet<Score> Scores;
    public DbSet<Projectile> Projectiles;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configura la cadena de conexión a tu base de datos
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Galaga;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    

    
}