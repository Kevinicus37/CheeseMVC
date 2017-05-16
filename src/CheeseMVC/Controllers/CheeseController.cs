using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {

            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();

            return View(addCheeseViewModel);
        }
        
        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese newCheese = addCheeseViewModel.CreateCheese();

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }
            
            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);

            }

            context.SaveChanges();
            return Redirect("/");
        }

        public IActionResult Edit(int id)
        {
            var addEditCheeseViewModel = new AddEditCheeseViewModel();
            addEditCheeseViewModel.ID = id;
            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            Cheese updateCheese = context.Cheeses.Single(c => c.ID == addEditCheeseViewModel.ID);
            
            updateCheese.Name = addEditCheeseViewModel.Name;
            updateCheese.Description = addEditCheeseViewModel.Description;
            updateCheese.Type = addEditCheeseViewModel.Type;
            updateCheese.Rating = addEditCheeseViewModel.Rating;
            context.SaveChanges();
            return Redirect("/");

        }
    }
}
