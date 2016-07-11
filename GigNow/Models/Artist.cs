using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigNow.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [DisplayName("Artist or Band Name")]
        public string Name { get; set; }
        public virtual Address address { get; set; }
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [DisplayName("Primary Genre")]
        public string Genre1 { get; set; }
        [DisplayName("Secondary Genre")]
        public string Genre2 { get; set; }
        [DisplayName("Tertiary Genre")]
        public string Genre3 { get; set; }
        [DisplayName("Type of Act")]
        public string Type { get; set; }
        [DisplayName("Number of Members")]
        public int? NumberOfMembers { get; set; }
        [DisplayName("Facebook Link")]
        public string FBLink { get; set; }
        [DisplayName("Site Link")]
        public string SiteLink { get; set; }
        [DisplayName("Twitter Link")]
        public string TwitterLink { get; set; }
        [AllowHtml]
        [DisplayName("Track One")]
        public string BandCampEmbed1 { get; set; }
        [AllowHtml]
        [DisplayName("Track Two")]
        public string BandCampEmbed2 { get; set; }
        [AllowHtml]
        [DisplayName("Track Three")]
        public string BandCampEmbed3 { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}