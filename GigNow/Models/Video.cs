using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public virtual Artist Artist { get; set; }
    }
}