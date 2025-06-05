using System.ComponentModel.DataAnnotations;

namespace apbd12c_cw12.DTOs;

public class AddClientToTripDto
{
    [Required]
    [StringLength(120)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(120)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(120)]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    [StringLength(120)]
    public string Telephone { get; set; } = string.Empty;
        
    [Required]
    [StringLength(120)]
    public string Pesel { get; set; } = string.Empty;
        
    [Required]
    public int IdTrip { get; set; }
        
    [Required]
    public string TripName { get; set; } = string.Empty;
        
    public int? PaymentDate { get; set; }
}