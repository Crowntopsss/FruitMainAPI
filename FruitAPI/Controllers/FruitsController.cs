using Microsoft.AspNetCore.Mvc;
using FruitAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FruitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitService _fruitService;

        public FruitsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpGet]
        public IEnumerable<Fruit> GetAllFruits()
        {
            var fruits = _fruitService.GetAllFruits();
            return fruits;
        }
    }
}
