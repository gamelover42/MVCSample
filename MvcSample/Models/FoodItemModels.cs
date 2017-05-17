using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    public class FoodItem
    {
        public string Name { get; set; }
        public int KCals { get; set; }
        public int ID { get; internal set; }
    }
}