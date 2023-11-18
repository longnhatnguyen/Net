using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class SlotHardwareConfig
    {
        public int Id { get; set; }
        public string Monitor { get; set; }
        public string Gpu { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string NameHardware { get; set; }
        public int CyberID { get; set; }
    }
}
