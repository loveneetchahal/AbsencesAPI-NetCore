using AbsencesAPI.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbsencesAPI.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Absence> Absences { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Management> Managements { get; set; }
    public DbSet<Stats> Stats { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AbsencesDB;Trusted_Connection=True;");
        //optionsBuilder.UseSqlite("Filename=Absences.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Absence>().HasKey(e => e.Id);
        builder.Entity<Employee>().HasKey(e => e.Id);
        builder.Entity<Management>().HasKey(e => e.Id);
        builder.Entity<Stats>().HasKey(e => e.Id);

        builder.Entity<Employee>().HasOne(e => e.Manager);
        builder.Entity<Employee>().HasMany(e => e.Absences).WithMany(e => e.Employees);
        builder.Entity<Absence>().HasOne(e => e.Statistic);

        base.OnModelCreating(builder);
    }
}