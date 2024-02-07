using FruitAPI.Data;
using FruitAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FruitAPI.DTOs;

namespace FruitAPI.Services
{
    public class FruitService : IFruitService
    {
        private readonly AppDbContext _dbContext;

        public FruitService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Fruit> GetAllFruits()
        {
            // Assuming you have a DbSet<Fruit> named Fruits in your DbContext
            return _dbContext.Fruits.ToList();
        }
    }
}

public interface IFruitService
{
    IEnumerable<Fruit> GetAllFruits();
}