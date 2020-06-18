using MyBakeryShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.Models
{
    public class BannerRepository : IBannerRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;
        public BannerRepository(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }
        public IEnumerable<Banner> ListBanner()
        {
            
            return _bakeryDbContext.Banners;
        }
    }
}
