using apbd12c_cw12.Data;
using apbd12c_cw12.DTOs;
using apbd12c_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd12c_cw12.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<TripListDto> GetTripsPagedAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        
        var totalTrips = await _context.Trip.CountAsync();
        var allPages = (int) Math.Ceiling(totalTrips / (double) pageSize);
        
        var trips = _context.Trip
            .Include(t => t.CountryTrips)
            .ThenInclude(ct => ct.Country)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.CountryTrips.Select(ct => new CountryDto
            {
                Name = ct.Country.Name
            }).ToList(),
            Clients = t.ClientTrips.Select(ct => new ClientDto
            {
                FirstName = ct.Client.FirstName,
                LastName = ct.Client.LastName
            }).ToList()
        }).ToList();
            
        return new TripListDto
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = allPages,
            Trips = trips
        };
    }

    public async Task DeleteClientAsync(int idClient)
    {
        var client = await _context.Client
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);
        
        if (client == null)
            throw new KeyNotFoundException($"Klient o id {idClient} nie istnieje!");
        
        if (client.ClientTrips.Any())
            throw new InvalidOperationException($"Klient o id {idClient} posiada przypisane wycieczki!");
        
        _context.Client.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task AddClientToTripAsync(int idTrip, AddClientToTripDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Telephone = dto.Telephone,
                    Pesel = dto.Pesel
                };
                _context.Client.Add(client);
                await _context.SaveChangesAsync();
            }

            var trip = await _context.Trip
                .Include(t => t.ClientTrips)
                .FirstOrDefaultAsync(t => t.IdTrip == idTrip);

            if (trip == null)
                throw new KeyNotFoundException($"Wycieczka o id {idTrip} nie istnieje!");

            if (trip.DateFrom <= DateTime.Now)
                throw new InvalidOperationException("Nie można zapisać się na wycieczkę, która już się odbyła!");
            
            if (trip.Name != dto.TripName)
                throw new InvalidOperationException("Nazwa wycieczki nie zgodna z id wycieczki!");

            if (await _context.Client_Trip.AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == dto.IdTrip))
                throw new InvalidOperationException(
                    $"Klient o id {client.IdClient} jest już zapisany na wycieczkę o id {dto.IdTrip}");

            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = Convert.ToInt32(DateTime.Now.ToString("ddMMyyyy")),
                PaymentDate = dto.PaymentDate
            };

            _context.Client_Trip.Add(clientTrip);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}