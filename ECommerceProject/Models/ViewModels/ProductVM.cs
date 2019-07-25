using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECommerceProject.Models.Entities;

namespace ECommerceProject.Models.ViewModels
{
    public class ProductVM
    {
        public ProductVM()
        {

        }

        public ProductVM(Product row)
        {
            Id = row.Id;
            Title = row.Title;
            Description = row.Description;
            ImageUrl = row.ImageUrl;
            oldPrice = row.oldPrice;
            stockPrice = row.stockPrice;
            Discount = row.Discount;
            isNew = row.isNew;
            numberInStock = row.numberInStock;
            isOnSale = row.isOnSale;
            Discontinued = row.Discontinued;
            CollectionId = row.CollectionId;
            isFeatured = row.isFeatured;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Title can be 30 characters max")]
        public string Title { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Title can be 30 characters max")]
        public string Description { get; set; }
        [StringLength(250, ErrorMessage = "Title can be 30 characters max")]
        public string ImageUrl { get; set; }
        [Display(Name = "Old Price")]
        public decimal oldPrice { get; set; }
        [Display(Name = "New Price")]
        public decimal newPrice
        {
            get
            {
                return Math.Round(oldPrice - oldPrice * Discount / 100, 2);
            }
        }
        [Display(Name = "Stock Price")]
        public decimal stockPrice { get; set; }
        public int Discount { get; set; } = 0;
        //To show the NEW tag
        [Display(Name = "Is New")]
        public bool isNew { get; set; } = false;
        [Display(Name = "Number In Stock")]
        public int numberInStock { get; set; }
        //To activate discount
        [Display(Name = "Is On Sale")]
        public bool isOnSale { get; set; } = false;
        public bool Discontinued { get; set; } = false;
        public int CollectionId { get; set; }
        //To display in featured items page and home page
        [Display(Name = "Is Featured")]
        public bool isFeatured { get; set; } = false;
        public string CollectionName { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }
}