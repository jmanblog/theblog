using System;
using System.Collections.Generic;

namespace theblog.Models
{
    public partial class Articles
    {
        public int ArticleId { get; set; }
        public string ArticleShort { get; set; }
        public string ArticleTitle { get; set; }
        public int ArticleMainTopic { get; set; }
        public int? ArticleRelatedTopic { get; set; }
        public DateTime ArticleDate { get; set; }
        public DateTime? ArticleUpdated { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleImage { get; set; }
        public string ArticleKeywords { get; set; }
    }
}
