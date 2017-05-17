using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    /// <summary>
    /// A service to hold business logic for Food items
    /// </summary>
    public class FoodService : IFoodService
    {
        IFoodRepository repo;
        public FoodService(IFoodRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add the food to the repository.
        /// </summary>
        /// <param name="food"></param>
        public void Add(FoodItem food)
        {
            repo.Add(food);
        }

        /// <summary>
        /// Delete the food from the repository
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }

        /// <summary>
        /// Retrieves all foods from the repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FoodItem> Get()
        {
            return repo.Get();
        }

        /// <summary>
        /// Retrieves a single food from the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FoodItem GetByKey(int id)
        {
            return repo.GetByKey(id);
        }
    }
}