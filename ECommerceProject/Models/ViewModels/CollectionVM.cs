using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceProject.Models.Entities;

namespace ECommerceProject.Models.ViewModels
{
    public class CollectionVM
    {
        public CollectionVM()
        {

        }

        public CollectionVM(Collection row)
        {
            Id = row.Id;
            Title = row.Title;
            Description = row.Description;
            ImageUrl = row.ImageUrl;
            VendorId = row.VendorId;
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is Required")]
        [StringLength(30, ErrorMessage ="Collection Title can max be 30 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(250, ErrorMessage = "Description Title can max be 250 characters")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}