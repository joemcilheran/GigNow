using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Listener
    {
        [Key]
        public int ListenerID { get; set; }
        [ForeignKey("address")]
        public int? AddressId { get; set; }
        public Address address { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
        public string Genre3 { get; set; }

    }
}