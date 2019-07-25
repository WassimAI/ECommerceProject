using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Models.MainVM
{
    public class ProductsPageVM
    {
        public IEnumerable<ProductVM> Products { get; set; }
    }
}