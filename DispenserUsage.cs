using System.Text.Json.Serialization;

namespace BeerTap.Models;

public class DispenserUsage
{
    public int Id { get; set; }

    public int DispenserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Amount { get; set; }

    public decimal Cost { get; set; }

}
