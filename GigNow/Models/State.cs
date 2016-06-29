using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        [ForeignKey("city")]
        public int? CityId { get; set; }
        public City city { get; set; }

    }
}