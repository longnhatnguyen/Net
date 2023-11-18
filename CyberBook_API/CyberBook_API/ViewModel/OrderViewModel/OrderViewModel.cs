using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.ViewModel.OrderViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? StatusOrder { get; set; }
        public int CyberId { get; set; }
        public List<Slot> SlotOrderId { get; set; }

    }
}
