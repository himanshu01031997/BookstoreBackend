using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserTicket
    {
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
        public DateTime IssueAt { get; set; }
    }
}
