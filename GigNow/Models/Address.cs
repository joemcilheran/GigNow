using System;
using System.Collections.Generic;
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
        public string StreetAddress { get; set; }
        public string Apt { get; set; }
        [ForeignKey("zipcode")]
        public int? ZipCodeId { get; set; }
        public Zipcode zipcode { get; set; }

    }
}