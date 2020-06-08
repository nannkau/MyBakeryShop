using Microsoft.AspNetCore.Http;
using MyBakeryShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.ViewModels
{
    public class CreateProductVm
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AllergyInformation { get; set; }
        public decimal Price { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        public IFormFile ProfileImage { get; set; }
    }
}
