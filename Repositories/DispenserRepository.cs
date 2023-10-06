using Microsoft.AspNetCore.Mvc;

namespace BeerTap.Models
{
  public class DispenserRepository
  {
    private readonly BeerTapsContext _DBContext;

    public DispenserRepository(BeerTapsContext dbContext)
    {
      _DBContext = dbContext;
    }

    public IEnumerable<Dispenser> GetAll()
    {
      var result = _DBContext.Dispenser.ToList();
      return result;
    }

    public Dispenser? GetById(int id)
    {
      var result = _DBContext.Dispenser.FirstOrDefault(element=>element.Id==id);
      if (result==null) return null;
        return result;
    }

    public void Create([FromBody] Dispenser dispenser)
    {
      _DBContext.Dispenser.Add(dispenser);
      _DBContext.SaveChanges();
    }

    public void Remove(Dispenser dispenser)
    {
        _DBContext.Dispenser.Remove(dispenser);
        _DBContext.SaveChanges();
      }

    public void Update(Dispenser dispenser) 
    {
      _DBContext.Dispenser.Update(dispenser);
      _DBContext.SaveChanges();
    }
  }
}
