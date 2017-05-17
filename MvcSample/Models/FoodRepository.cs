using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    public class FoodRepository : IFoodRepository
    {
        private static List<FoodItem> foods = new List<FoodItem>();

        public IEnumerable<FoodItem> Get()
        {
            return foods;
        }

        public void Add(FoodItem food)
        {
            lock (foods)
            {
                foods.Add(food);
            }
        }

        public FoodItem GetByKey(int id)
        {
            var results = from f in foods
                       where f.ID.Equals(id)
                       select f;
            return results.FirstOrDefault();
        }

        public void Delete(int id)
        {
            foods.RemoveAll(f => f.ID == id);
        }
    }
}