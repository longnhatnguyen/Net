using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class Slot
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public string SlotName { get; set; }
        public int? SlotHardwareConfigId { get; set; }
        public string SlotHardwareName { get; set; }
        public int? StatusId { get; set; }
        public string SlotDescription { get; set; }
        public int? SlotPositionX { get; set; }
        public int? SlotPositionY { get; set; }
    }
}
