namespace apbd12c_cw12.DTOs;

public class TripDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDto> Countries { get; set; } = new();
    public List<ClientDto> Clients { get; set; } = new();
}