using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd12c_cw12.Models;

public class ClientTrip
{
    [ForeignKey(nameof(Client))]
    public int IdClient { get; set; }
    
    [Required]
    public Client Client { get; set; } = null!;
    
    [ForeignKey(nameof(Trip))]
    public int IdTrip { get; set; }
    
    [Required]
    public Trip Trip { get; set; } = null!;
    
    [Required]
    public int RegisteredAt { get; set; }
    
    public int? PaymentDate { get; set; }
}