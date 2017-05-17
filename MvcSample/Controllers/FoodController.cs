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
        public FoodController(IFoodRepository repo)
        {
            this.repo = repo;
        }
        private IFoodRepository repo;

        // GET: Food
        public ActionResult Index()
        {
            return View(repo.Get());
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

                repo.Add(f);
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
                repo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
