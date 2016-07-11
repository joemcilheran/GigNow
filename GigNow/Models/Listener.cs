using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public virtual Address address { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
        [DisplayName("Primary Genre")]
        public string Genre1 { get; set; }
        [DisplayName("Secondary Genre")]
        public string Genre2 { get; set; }
        [DisplayName("Tertiary Genre")]
        public string Genre3 { get; set; }

    }
}