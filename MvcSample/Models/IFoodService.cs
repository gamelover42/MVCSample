using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSample.Models
{
    /// <summary>
    /// An interface to a Food service
    /// </summary>
    public interface IFoodService
    {
        void Add(FoodItem food);
        IEnumerable<FoodItem> Get();
        FoodItem GetByKey(int id);
        void Delete(int id);
    }
}
