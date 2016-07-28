using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Venue Venue { get; set; }
    }
}