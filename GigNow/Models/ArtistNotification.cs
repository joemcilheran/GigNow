using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class ArtistNotification
    {
        [Key]
        public int ArtistNotificationId { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public bool read { get; set; }
        public virtual Artist artist { get; set; }
        public virtual Slot slot { get; set; }
    }
}