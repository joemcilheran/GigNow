using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        [DisplayName("Apt #")]
        public string Apt { get; set; }
        public virtual Zipcode zipcode { get; set; }

    }
}