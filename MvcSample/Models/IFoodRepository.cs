using System.Collections.Generic;

namespace MvcSample.Models
{
    public interface IFoodRepository
    {
        void Add(FoodItem food);
        IEnumerable<FoodItem> Get();
        FoodItem GetByKey(int id);
        void Delete(int id);
    }
}