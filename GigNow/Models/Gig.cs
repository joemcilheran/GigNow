using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Gig
    {
        [Key]
        public int GigId { get; set; }
        public int Cover { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [DisplayName("Gig Name")]
        public string Name { get; set; }
        [DisplayName("Default Type of Act")]
        public string DefaultArtistType { get; set; }
        [DisplayName("Default Compensation")]
        public int? DefaultCompensation { get; set; }
        [DisplayName("Default Genre")]
        public string DefaultGenre { get; set; }
        [DisplayName("Default Perks")]
        public string DefaultPerks { get; set; }
        [DisplayName("Load-in Instructions")]
        public string LoadInInsrtuctions { get; set; }
        public virtual Venue Venue { get; set; }

    }
}