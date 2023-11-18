using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.ViewModel.SlotViewModel
{
    public class SlotViewModelIn
    {
        public int CyberId { get; set; }
        public int RoomId { get; set; }
        public Slot Slot { get; set; }
    }
}
