using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class RoomType
    {
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomDescription { get; set; }
        public int CyberID { get; set; }
    }                                                
}
