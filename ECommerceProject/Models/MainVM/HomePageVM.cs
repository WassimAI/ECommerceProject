﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Models.MainVM
{
    public class HomePageVM
    {
        public IEnumerable<CollectionVM> Collections { get; set; }
        public IEnumerable<ProductVM> FeaturedProducts { get; set; }
        public IEnumerable<ProductVM> PromotedProducts { get; set; }
    }
}