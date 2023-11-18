using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class RatingCyber
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public double RatePoint { get; set; }
        public string CommentContent { get; set; }
        public int? CyberId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Edited { get; set; }
        public int? OrderId { get; set; }
    }
}
