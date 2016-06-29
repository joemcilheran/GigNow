using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Track
    {
        [Key]
        public int TraclId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}