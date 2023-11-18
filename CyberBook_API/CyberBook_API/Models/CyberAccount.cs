using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class CyberAccount
    {
        public int ID { get; set; }
        public int? UserId { get; set; }
        public int? CyberId { get; set; }
        public string PhoneNumber { get; set; }
        public string CyberName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
