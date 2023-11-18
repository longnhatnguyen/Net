using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class Room
    {
        public int Id { get; set; }
        public int? CyberId { get; set; }
        public string RoomName { get; set; }
        public int? RoomType { get; set; }
        public string RoomPosition { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public double PriceRoom { get; set; }
    }
}
