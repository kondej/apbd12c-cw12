using System.ComponentModel.DataAnnotations;

namespace apbd12c_cw12.Models;

public class Trip
{
    [Key]
    public int IdTrip { get; set; }
    [Required] 
    [StringLength(120)] 
    public string Name { get; set; } = string.Empty;
    [Required] 
    [StringLength(220)] 
    public string Description { get; set; } = string.Empty;
    [Required] 
    public DateTime DateFrom { get; set; }
    [Required] 
    public DateTime DateTo { get; set; }
    [Required]
    public int MaxPeople { get; set; }
    
    public ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
    public ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}