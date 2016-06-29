using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

    }
}