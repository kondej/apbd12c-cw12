using apbd12c_cw12.DTOs;

namespace apbd12c_cw12.Services;

public interface IDbService
{
    Task<TripListDto> GetTripsPagedAsync(int page = 1, int pageSize = 10);
    Task DeleteClientAsync(int idClient);
    Task AddClientToTripAsync(int idTrip, AddClientToTripDto dto);
}