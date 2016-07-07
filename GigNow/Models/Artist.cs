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
        public virtual Address address { get; set; }
        public string ContactName { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
        public string Genre3 { get; set; }
        public string Type { get; set; }
        public int? NumberOfMembers { get; set; }
        public string FBLink { get; set; }
        public string SiteLink { get; set; }
        public string TwitterLink { get; set; }
        public string ExtraLink { get; set; }
        public string ExtraLink2 { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}