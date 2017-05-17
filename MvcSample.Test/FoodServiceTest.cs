using System;
using System.Linq;
using Xunit;
using MvcSample.Support;
using System.Reflection;
using MvcSample.Controllers;
using System.Web.Mvc;
using MvcSample.Models;
using System.Collections.Generic;

namespace MvcSample.Test
{
    public class FoodServiceTest
    {
        /// <summary>
        /// The "real" food repo has a static list which does not lend itself
        /// to testing.
        /// </summary>
        private class MockFoodRepo : IFoodRepository
        {
            public MockFoodRepo()
            {
                Foods = new List<FoodItem>();
            }

            public int LastFoodId { get; set; }
            public List<FoodItem> Foods { get; set; }

            public void Add(FoodItem food)
            {
                lock (Foods)
                {
                    LastFoodId++;
                    food.ID = LastFoodId;
                    Foods.Add(food);
                }
            }

            public void Delete(int id)
            {
                Foods.RemoveAll(f => f.ID == id);
            }

            public IEnumerable<FoodItem> Get()
            {
                return Foods;
            }

            public FoodItem GetByKey(int id)
            {
                return Foods.Find(f => f.ID == id);
            }
        }

        [Fact]
        public void TestItemsGetAdded()
        {
            var repo = new MockFoodRepo();
            var service = new FoodService(repo);

            //double check it is empty to start
            Assert.Empty(repo.Foods);

            //add two items
            service.Add(new FoodItem { Name = "Carrot", KCals = 23 });
            service.Add(new FoodItem { Name = "Potato", KCals = 80 });

            //check that two items are in the repo
            var list = service.Get();
            Assert.Same(repo.Foods, list);

            Assert.Equal(2, list.Count());
        }

        [Fact]
        public void TestItemsReceiveCorrectId()
        {
            var carrot = new FoodItem { Name = "Carrot", KCals = 23 };
            var potato = new FoodItem { Name = "Potato", KCals = 80 };

            var repo = new MockFoodRepo();
            var service = new FoodService(repo);
            service.Add(carrot);
            service.Add(potato);

            Assert.Equal(1, carrot.ID);
            Assert.Equal(2, potato.ID);
        }
        
        [Fact]
        public void TestItemsGetDeleted()
        {
            var carrot = new FoodItem { ID = 1, Name = "Carrot", KCals = 23 };
            var potato = new FoodItem { ID = 2, Name = "Potato", KCals = 80 };

            var repo = new MockFoodRepo();
            repo.Foods.Add(carrot);
            repo.Foods.Add(potato);
            repo.LastFoodId = 2;
            var service = new FoodService(repo);

            //double check it is empty to start
            service.Delete(1);
            Assert.Equal(1, repo.Foods.Count);
            Assert.Same(potato, repo.Foods[0]);
        }
    }
}
