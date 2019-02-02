using System;
using System.Collections.Generic;

namespace theblog.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string IdentityString { get; set; }
        public string AuthMethod { get; set; }
    }
}
