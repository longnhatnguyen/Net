using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class Cyber
    {
        public int Id { get; set; }
        public string CyberName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CyberDescription { get; set; }
        public double RatingPoint { get; set; }
        public string BossCyberName { get; set; }
        public int BossCyberID { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int status { get; set; }
        public string image { get; set; }
        public string BusinessLicense { get; set; }
    }
}
