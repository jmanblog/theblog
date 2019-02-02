using System;
using System.Collections.Generic;

namespace theblog.Models
{
    public partial class Topics
    {
        public int TopicId { get; set; }
        public string MainTopicName { get; set; }
        public string SubTopicName { get; set; }
        public string TopicKeywords { get; set; }
    }
}
