using System.Text.Json.Serialization;

namespace BeerTap.Models;

public class Dispenser
{
    public int Id { get; set; }

    public string? BeerName { get; set; }

    public decimal FlowVolume { get; set; }

    public decimal Cost { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public decimal TotalAmount { get; set; }

    public int TimesUsed { get; set; } 

    public decimal TotalCost { get; set; }
    
}
