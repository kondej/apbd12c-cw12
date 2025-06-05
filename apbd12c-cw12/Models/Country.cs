using System.ComponentModel.DataAnnotations;

namespace apbd12c_cw12.Models;

public class Country
{
    [Key] 
    public int IdCountry { get; set; }
    [Required] 
    [StringLength(120)] 
    public string Name { get; set; } = string.Empty;
    
    public ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}