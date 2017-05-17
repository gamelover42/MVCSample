using System.Collections.Generic;

namespace MvcSample.Models
{
    /// <summary>
    /// An interface to the Food "repository"
    /// </summary>
    public interface IFoodRepository
    {
        void Add(FoodItem food);
        IEnumerable<FoodItem> Get();
        FoodItem GetByKey(int id);
        void Delete(int id);
    }
}