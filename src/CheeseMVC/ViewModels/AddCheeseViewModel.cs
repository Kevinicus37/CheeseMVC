using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name="Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        public CheeseType Type { get; set; }

        public List<SelectListItem> CheeseTypes { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public AddCheeseViewModel()
        {
            CheeseTypes = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (CheeseType cheese in Enum.GetValues(typeof(CheeseType)))
            {
                CheeseTypes.Add(new SelectListItem
                {
                    Value = ((int)cheese).ToString(),
                    Text = cheese.ToString()
                });

            }
        }

        public Cheese CreateCheese()
        {
            Cheese newCheese = new Cheese {
                Name = this.Name,
                Description = this.Description,
                Rating = this.Rating,
                Type = this.Type
            };

            return newCheese;
        }
    }
}
