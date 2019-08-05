using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Models.MainVM
{
    public class CollectionsPageVM
    {
        public IEnumerable<CollectionVM> Collections { set; get; }
    }
}