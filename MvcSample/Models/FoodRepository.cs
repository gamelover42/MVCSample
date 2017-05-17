using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    /// <summary>
    /// A class which stores Food items in an in-memory static list
    /// </summary>
    public class FoodRepository : IFoodRepository
    {
        //since this is a "Fake" repository, this is just to simulate a DB identity column
        private static int lastId = 0;

        //the "fake" data store, normally these would go into a DB table
        private static List<FoodItem> foods = new List<FoodItem>();

        /// <summary>
        /// Retrieves a list of all foods entered
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FoodItem> Get()
        {
            return foods;
        }

        /// <summary>
        /// Add the new food object. The food will be assigned an ID.
        /// </summary>
        /// <param name="food"></param>
        public void Add(FoodItem food)
        {
            lock (foods)
            {
                //give each food a new "ID"
                lastId++;
                food.ID = lastId;
                foods.Add(food);
            }
        }

        /// <summary>
        /// Retrieve the food by it's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FoodItem GetByKey(int id)
        {
            var results = from f in foods
                       where f.ID.Equals(id)
                       select f;
            return results.FirstOrDefault();
        }

        /// <summary>
        /// Delete the food by it's ID value
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            foods.RemoveAll(f => f.ID == id);
        }
    }
}