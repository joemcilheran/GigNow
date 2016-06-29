using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }
        [ForeignKey("address")]
        public int? AddressId { get; set; }
        public Address address { get; set; }
        public string ContactName { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
        public string Genre3 { get; set; }
        public string Type { get; set; }
        public int? NumberOfMembers { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}