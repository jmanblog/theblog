using System;
using System.Collections.Generic;

namespace theblog.Models
{
    public partial class History
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int? ArticleId { get; set; }
        public int? UserRating { get; set; }
    }
}
