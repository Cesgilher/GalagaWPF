using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using GalagaWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DBContext : DbContext
{
  
    public DbSet<User> Users { get; set; }
    //public DbSet<Ship> Ships { get; set; }
    //public DbSet<Background> Backgrounds { get; set; }
    public DbSet<Score> Scores { get; set; }
    //public DbSet<Projectile> Projectiles { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // Configura la cadena de conexión a tu base de datos
    //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\cesgi\\Source\\Repos\\Cesgilher\\GalagaWPF\\GalagaWPF\\DB\\Galaga.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Obtiene el directorio del proyecto (asegúrate de ajustar esto según tu estructura de proyectos)
        string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));

        // Construye la ruta relativa al proyecto
        string relativePath = Path.Combine(projectDirectory, "DB", "Galaga.mdf");

        // Configura la cadena de conexión a tu base de datos
        optionsBuilder.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .LogTo(Console.WriteLine, LogLevel.Information);
    }





}