namespace FruitAPI.Models
{
    public class Fruit
    {
        public string name { get; set; }
        public int id { get; set; }
        public string family { get; set; }
        public string order { get; set; }
        public string genus { get; set; }
        public string image { get; set; }
        public Nutrition nutritions { get; set; }
    }

    public class Nutrition
    {
        public int calories { get; set; }
        public double fat { get; set; }
        public double sugar { get; set; }
        public double carbohydrates { get; set; }
        public double protein { get; set; }
    }
}
