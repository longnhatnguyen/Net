using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public int? RoleId { get; set; }
        public string Image { get; set; }
        public DateTime? Dob { get; set; }
        public double RatingPoint { get; set; }
        public int AccountID { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
