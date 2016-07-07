using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public virtual State state { get; set; }
    }
}