using Microsoft.AspNetCore.Mvc;

namespace BeerTap.Models
{
  public class DispenserUsageRepository
  {
    private readonly BeerTapsContext _DBContext;

    public DispenserUsageRepository(BeerTapsContext dbContext)
    {
      _DBContext = dbContext;
    }

    public IEnumerable<DispenserUsage> GetAll()
    {
      var result = _DBContext.DispenserUsage.ToList();
      return result;
    }

    public DispenserUsage? GetById(int id)
    {
      var result = _DBContext.DispenserUsage.FirstOrDefault(element=>element.Id==id);
      if (result==null) return null;
      return result;
    }

    public void Create(DispenserUsage dispenserUsage) 
    {
      _DBContext.DispenserUsage.Add(dispenserUsage);
      _DBContext.SaveChanges();
    }

    public void Update(DispenserUsage dispenserUsage)
    {
      _DBContext.DispenserUsage.Update(dispenserUsage);
      _DBContext.SaveChanges();
    }
  }
}
