using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceProject.Models.ViewModels
{
    public class RegisterUserVM
    {
        public int Id { get; set; }
        public Guid UserID { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(150)]
        public string Residence { get; set; }
        public string Phone { get; set; }
    }
}