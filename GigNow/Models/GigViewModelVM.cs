using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class GigViewModelVM
    {
        public Gig gig { get; set; }
        public Venue venue { get; set; }
        public List<Slot> bill { get; set; }
    }
}