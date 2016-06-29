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
        public int AddressId { get; set; }
        public Address address { get; set; }
        public string ContactName { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
        public string Genre3 { get; set; }
        public string Type { get; set; }
        public int NumberOfMembers { get; set; }
        [ForeignKey("photo")]
        public int PhotoId { get; set; }
        public Photo photo { get; set; }
        [ForeignKey("video")]
        public int VideoId { get; set; }
        public Video video { get; set; }
        [ForeignKey("track1")]
        public int track1Id { get; set; }
        public Track track1 { get; set; }
        [ForeignKey("track2")]
        public int track2Id { get; set; }
        public Track track2 { get; set; }
        [ForeignKey("track3")]
        public int track3Id { get; set; }
        public Track track3 { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}