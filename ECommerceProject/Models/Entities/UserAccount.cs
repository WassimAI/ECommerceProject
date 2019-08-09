using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceProject.Models.Entities
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public Guid UserID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string Residence { get; set; }
        public string Phone { get; set; }
    }
}