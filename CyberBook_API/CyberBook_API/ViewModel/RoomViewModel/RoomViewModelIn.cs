using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.ViewModel.RoomViewModel
{
    public class RoomViewModelIn
    {
        public int CyberId { get; set; }
        public Room Room { get; set; }
    }
}
