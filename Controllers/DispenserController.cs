using Microsoft.AspNetCore.Mvc;
using BeerTap.Models;

namespace BeerTap.Controllers;

[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{

    private readonly DispenserRepository _dispenserRepository;

    public BeerController(DispenserRepository dispenserRepository)
    {
        this._dispenserRepository = dispenserRepository;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var dispensers=this._dispenserRepository.GetAll();
        return Ok(dispensers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var dispenser=this._dispenserRepository.GetById(id);
        return Ok(dispenser);
    }

    [HttpPost("")]
   public IActionResult Create([FromBody] Dispenser dispenser)
    {
        this._dispenserRepository.Create(dispenser);
        return Ok(true);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {  
        var dispenser = _dispenserRepository.GetById(id);
        if (dispenser==null) return NotFound();
        this._dispenserRepository.Remove(dispenser);
        return Ok(true);
    }

    [HttpPatch("{id}")]

    public IActionResult Update(int id)
    {
        var dispenser = _dispenserRepository.GetById(id);
        if(dispenser==null) return NotFound();
        this._dispenserRepository.Update(dispenser);
        return Ok(true);
    }
}
