using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECommerceProject.Models.Entities;

namespace ECommerceProject.Models.ViewModels
{
    public class VendorVM
    {
        public VendorVM()
        {

        }

        public VendorVM(Vendor row)
        {
            Id = row.Id;
            Title = row.Title;
            Location = row.Location;
            IsActive = row.IsActive;
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [StringLength(30, ErrorMessage ="Title can maximum be 30 characters long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [StringLength(30, ErrorMessage = "Location can maximum be 50 characters long")]
        public string Location { get; set; }
        public bool IsActive { get; set; } = true;
    }
}