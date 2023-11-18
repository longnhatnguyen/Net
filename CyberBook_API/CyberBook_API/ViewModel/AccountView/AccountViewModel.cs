using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.ViewModel.AccountView
{
    public class AccountViewModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
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
    }
}
