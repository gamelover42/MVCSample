using MvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSample.Controllers
{
    public class FoodController : Controller
    {
        private IFoodService foodService;
        public FoodController(IFoodService foodService)
        {
            this.foodService = foodService;
        }

        // GET: Food
        public ActionResult Index()
        {
            return View(foodService.Get());
        }

        // GET: Food/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        public ActionResult Create(FoodItem f)
        {
            try
            {

                foodService.Add(f);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                foodService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
