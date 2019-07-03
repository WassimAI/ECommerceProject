using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.Entity;
using ECommerceProject.Models;
using ECommerceProject.Models.Entities;

namespace ECommerceProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base("EcommerceDB")
        {
        }

        public DbSet<Collection> collections { get; set; }
        public DbSet<Vendor> vendors { get; set; }

        public DbSet<Product> products { get; set; }
    }
}