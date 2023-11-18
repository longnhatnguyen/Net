using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string SlotOrderId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? StatusOrder { get; set; }
        public int CyberId { get; set; }
    }
}
