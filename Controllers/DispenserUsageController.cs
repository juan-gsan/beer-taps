using Microsoft.AspNetCore.Mvc;
using BeerTap.Models;

namespace BeerTap.Controllers;

[ApiController]
[Route("[controller]")]

public class UsageController : ControllerBase
{
  private readonly DispenserUsageRepository _usageRepository;
  private readonly DispenserRepository _dispenserRepository;

  public UsageController(DispenserUsageRepository usageRepository, DispenserRepository dispenserRepository)
  {
    this._usageRepository = usageRepository;
    this._dispenserRepository = dispenserRepository;
  }

  [HttpGet("")]
  
  public IActionResult GetAll()
  {
    var dispenserUsages = this._usageRepository.GetAll();
    return Ok(dispenserUsages);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var dispenserUsage = this._usageRepository.GetById(id);
    return Ok(dispenserUsage);
  }

  [HttpPost("{id}")]

  public IActionResult Open(int id)
  {
    var dispenser = this._dispenserRepository.GetById(id);
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

    dispenser.Status = !dispenser.Status;
    this._usageRepository.Create(newUsage);
    return Ok(newUsage);
  }

  [HttpPatch("{id}")]

  public IActionResult Update(int id)
  {
    var dispenserUsage = this._usageRepository.GetById(id);
    var dispenserId = dispenserUsage!.DispenserId;
    var dispenser = this._dispenserRepository.GetById(dispenserId);

    if (dispenserUsage != null && dispenser != null) {

      decimal amountDispensed = dispenser.FlowVolume;
      decimal cost = amountDispensed * dispenser.Cost;
      dispenserUsage.Amount += amountDispensed;
      dispenserUsage.Cost += cost;

      dispenser.TotalAmount += amountDispensed; 
      dispenser.TotalCost += cost;

      this._usageRepository.Update(dispenserUsage);
      this._dispenserRepository.Update(dispenser);
      return Ok(dispenserUsage);
    } else {
      return NotFound();
    }
  }

  [HttpPatch("close/{id}")]

  public IActionResult Close(int id)
  {
    var dispenserUsage = this._usageRepository.GetById(id);
    var dispenserId = dispenserUsage!.DispenserId;
    var dispenser = this._dispenserRepository.GetById(dispenserId);

    if (dispenserUsage != null && dispenser != null) {

      dispenserUsage.EndTime = DateTime.Now;
      TimeSpan timeDifference = DateTime.Now - dispenserUsage.StartTime;
      decimal amountDispensed = (decimal)timeDifference.TotalSeconds * dispenser.FlowVolume;
      decimal cost = amountDispensed * dispenser.Cost;
      dispenserUsage.Amount = amountDispensed;
      dispenserUsage.Cost = cost;

      dispenser.TimesUsed +=1;
      dispenser.TotalAmount += amountDispensed; 
      dispenser.TotalCost += cost;
      dispenser.Status = !dispenser.Status;

      this._usageRepository.Update(dispenserUsage);
      this._dispenserRepository.Update(dispenser);
      return Ok(dispenserUsage);
    } else {
      return NotFound();
    }
  }
}
