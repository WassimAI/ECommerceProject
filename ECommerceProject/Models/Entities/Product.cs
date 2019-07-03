using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceProject.Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal oldPrice { get; set; }
        public decimal newPrice { get; set; }
        public decimal stockPrice { get; set; }
        public int Discount { get; set; }
        //To show the NEW tag
        public bool isNew { get; set; } = false;
        public int numberInStock { get; set; }
        //To activate discount
        public bool isOnSale { get; set; } = false;
        public bool Discontinued { get; set; } = false;
        public int CollectionId { get; set; }
        //To display in featured items page and home page
        public bool isFeatured { get; set; } = false;

        [ForeignKey("CollectionId")]
        public virtual Collection collection { get; set; }
    }
}