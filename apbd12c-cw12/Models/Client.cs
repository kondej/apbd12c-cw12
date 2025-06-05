using System.ComponentModel.DataAnnotations;

namespace apbd12c_cw12.Models;

public class Client
{
    [Key] 
    public int IdClient { get; set; }
    [Required] 
    [StringLength(120)] 
    public string FirstName { get; set; } = string.Empty;
    [Required] 
    [StringLength(120)] 
    public string LastName { get; set; } = string.Empty;
    [Required] 
    [StringLength(120)] 
    public string Email { get; set; } = string.Empty;
    [Required] 
    [StringLength(120)] 
    public string Telephone { get; set; } = string.Empty;
    [Required] 
    [StringLength(120)] 
    public string Pesel { get; set; } = string.Empty;
    
    public ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}