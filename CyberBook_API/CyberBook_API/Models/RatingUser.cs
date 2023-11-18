using System;
using System.Collections.Generic;

#nullable disable

namespace CyberBook_API.Models
{
    public partial class RatingUser
    {
        public int Id { get; set; }
        public int? CyberId { get; set; }
        public double RatePoint { get; set; }
        public string CommentContent { get; set; }
        public int? UsersId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Edited { get; set; }
    }
}
