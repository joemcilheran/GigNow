using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class ListenerNotification
    {
        [Key]
        public int ListenerNotificationId { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public bool read { get; set; }
        public virtual Listener listener { get; set; }
        public virtual Gig gig { get; set; }
    }
}