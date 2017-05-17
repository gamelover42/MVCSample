using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    /// <summary>
    /// Simple model to represent entered food items and their calorie count
    /// </summary>
    public class FoodItem
    {
        public string Name { get; set; }
        public int KCals { get; set; }
        public int ID { get; set; }
    }
}