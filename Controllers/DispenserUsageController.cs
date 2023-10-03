using Microsoft.AspNetCore.Mvc;
using BeerTap.Models;
namespace BeerTap.Controllers;

[ApiController]
[Route("[controller]")]

public class UsageController : ControllerBase
{
  private readonly BeerTapsContext _DBContext;

  public UsageController(BeerTapsContext dbContext)
  {
    this._DBContext = dbContext;
  }

  [HttpGet("")]
  
  public IActionResult GetAll()
  {
    var dispenserUsages = this._DBContext.DispenserUsage.ToList();
    return Ok(dispenserUsages);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var dispenserUsage = this._DBContext.DispenserUsage.FirstOrDefault(element => element.Id == id);
    return Ok(dispenserUsage);
  }

  [HttpPost("{id}")]

  public IActionResult Open(int id)
  {
    var dispenser = this._DBContext.Dispenser.FirstOrDefault(element => element.Id == id);
    if(dispenser == null) {
      return NotFound("Dispenser not found");
    }

    var newUsage = new DispenserUsage {
      DispenserId = dispenser.Id,
      StartTime = DateTime.Now,
      EndTime = DateTime.Now,
      Amount = 0.0M,
      Cost = 0.0M
    };

    this._DBContext.DispenserUsage.Add(newUsage);
    this._DBContext.SaveChanges();
    return Ok(newUsage);
  }

  [HttpPatch("{id}")]

  public IActionResult Update(int id)
  {
    var dispenserUsage = this._DBContext.DispenserUsage.FirstOrDefault(usage => usage.Id == id);
    var dispenser = this._DBContext.Dispenser.FirstOrDefault(element => element.Id == dispenserUsage!.DispenserId);

    if (dispenserUsage != null && dispenser != null) {

      dispenserUsage.EndTime = DateTime.Now;
      TimeSpan timeDifference = DateTime.Now - dispenserUsage.StartTime;
      decimal amountDispensed = (decimal)timeDifference.TotalSeconds * dispenser.FlowVolume;
      decimal cost = amountDispensed * dispenser.Cost;
      dispenserUsage.Amount = amountDispensed;
      dispenserUsage.Cost = cost;

      dispenser.TotalAmount += amountDispensed; 
      dispenser.TotalCost += cost;

      this._DBContext.SaveChanges();
      return Ok(dispenserUsage);
    } else {
      return NotFound();
    }
  }
}
