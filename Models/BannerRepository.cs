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
            var bn = from b in _bakeryDbContext.Banners
                     where b.Active== true
                     select new { b.BannerId,b.Link,b.Active};
            List<Banner> banners = new List<Banner>();
            foreach(var item in bn)
            {
                Banner banner = new Banner();
                banner.BannerId = item.BannerId;
                banner.Link = item.Link;
                banner.Active = item.Active;
                banners.Add(banner);
            }
            return banners;
           
        }
    }
}
