using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.Models
{
    public interface IBannerRepository

    {
        IEnumerable<Banner> ListBanner();
    }
}
