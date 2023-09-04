using Microsoft.AspNetCore.Mvc;
using BeerTap.Models;
namespace BeerTap.Controllers;

[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{

    private readonly BeerTapsContext _DBContext;

    public BeerController(BeerTapsContext dbContext)
    {
        this._DBContext = dbContext;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var dispensers=this._DBContext.Dispenser.ToList();
        return Ok(dispensers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var dispenser=this._DBContext.Dispenser.FirstOrDefault(element=>element.Id==id);
        return Ok(dispenser);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        var dispenser=this._DBContext.Dispenser.FirstOrDefault(element=>element.Id==id);
        if(dispenser!=null) {
            this._DBContext.Remove(dispenser);
            this._DBContext.SaveChanges();
            return Ok(true);
        }
        return Ok(false);
    }

    [HttpPost("")]
   public IActionResult Create([FromBody] Dispenser _dispenser)
{
    this._DBContext.Dispenser.Add(_dispenser);
    this._DBContext.SaveChanges();
    return Ok(_dispenser);
}

    [HttpPatch("{id}")]

    public IActionResult Toggle(int id)
    {
        var dispenser = this._DBContext.Dispenser.FirstOrDefault(element => element.Id == id);
        
        if(dispenser == null) {
            return NotFound("Dispenser not found");
        }

        dispenser.Status = !dispenser.Status;

        if (dispenser.Status) {
            dispenser.TimesUsed += 1;
        }

        this._DBContext.SaveChanges();
        return Ok(dispenser);
    }
}
