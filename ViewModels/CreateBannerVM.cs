using Microsoft.AspNetCore.Http;
using MyBakeryShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.ViewModels
{
    public class CreateBannerVM
    {
        public CreateBannerVM() { }
        public int BannerId { get; set; }
        public IFormFile ProfileImage { get; set; }
        public bool Active { get; set; }
    }
}
