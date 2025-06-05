using apbd12c_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd12c_cw12.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Country> Country { get; set; }
    public DbSet<Trip> Trip { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<CountryTrip> Country_Trip { get; set; }
    public DbSet<ClientTrip> Client_Trip { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
        });
        
        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.DateFrom).IsRequired();
            entity.Property(e => e.DateTo).IsRequired();
            entity.Property(e => e.MaxPeople).IsRequired();
        });
        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(120);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Telephone).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Pesel).IsRequired().HasMaxLength(120);
            entity.HasIndex(e => e.Pesel).IsUnique();
        });
        
        modelBuilder.Entity<CountryTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdCountry, e.IdTrip });
            
            entity.HasOne(e => e.Country)
                .WithMany(c => c.CountryTrips)
                .HasForeignKey(e => e.IdCountry)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Trip)
                .WithMany(t => t.CountryTrips)
                .HasForeignKey(e => e.IdTrip)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip });
            
            entity.HasOne(e => e.Client)
                .WithMany(c => c.ClientTrips)
                .HasForeignKey(e => e.IdClient)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Trip)
                .WithMany(t => t.ClientTrips)
                .HasForeignKey(e => e.IdTrip)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(e => e.RegisteredAt).IsRequired();
            entity.Property(e => e.PaymentDate);
        });

        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                IdCountry = 1,
                Name = "Polska",
            },
            new Country
            {
                IdCountry = 2,
                Name = "Niemcy",
            },
            new Country
            {
                IdCountry = 3,
                Name = "Słowacja",
            }
        );

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                IdTrip = 1,
                Name = "Berlin",
                Description = "Wycieczka kulturoznawcza po Berlinie",
                DateFrom = new DateTime(2025, 6, 15),
                DateTo = new DateTime(2025, 6, 22),
                MaxPeople = 16,
            },
            new Trip
            {
                IdTrip = 2,
                Name = "Gdańsk",
                Description = "Wycieczka plażoznawcza po Gdańsku",
                DateFrom = new DateTime(2025, 7, 1),
                DateTo = new DateTime(2025, 7, 8),
                MaxPeople = 20,
            },
            new Trip
            {
                IdTrip = 3,
                Name = "Bratysława",
                Description = "Wycieczka zamkoznawcza po Bratysławie",
                DateFrom = new DateTime(2025, 8, 1),
                DateTo = new DateTime(2025, 8, 8),
                MaxPeople = 12,
            }
        );

        modelBuilder.Entity<Client>().HasData(
            new Client()
            {
                IdClient = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@gmail.com",
                Telephone = "123456789",
                Pesel = "12345678910",
            },
            new Client()
            {
                IdClient = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna.nowak@gmail.com",
                Telephone = "111222333",
                Pesel = "11111222333",
            }
        );

        modelBuilder.Entity<CountryTrip>().HasData(
            new CountryTrip()
            {
                IdCountry = 2,
                IdTrip = 1,
            },
            new CountryTrip()
            {
                IdCountry = 1,
                IdTrip = 2,
            },
            new CountryTrip()
            {
                IdCountry = 3,
                IdTrip = 3,
            }
        );

        modelBuilder.Entity<ClientTrip>().HasData(
            new ClientTrip()
            {
                IdClient = 1,
                IdTrip = 1,
                PaymentDate = null,
                RegisteredAt = 20250601
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}