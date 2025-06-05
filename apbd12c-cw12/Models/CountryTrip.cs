using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd12c_cw12.Models;

public class CountryTrip
{
    [ForeignKey(nameof(Country))]
    public int IdCountry { get; set; }
    
    [Required]
    public Country Country { get; set; } = null!;
    
    [ForeignKey(nameof(Trip))]
    public int IdTrip { get; set; }
    
    [Required]
    public Trip Trip { get; set; } = null!;
}