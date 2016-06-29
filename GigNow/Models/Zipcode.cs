using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Zipcode
    {
        [Key]
        public int ZipcodeId { get; set; }
        public int ZipCode { get; set; }
        [ForeignKey("address")]
        public int AddressId { get; set; }
        public Address address { get; set; }
    }
}