using MyBakeryShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.ViewModels
{
    public class OderDtViewModel
    {
        public IEnumerable<OrderDetail> OderDt { get; internal set; }
    }
}
