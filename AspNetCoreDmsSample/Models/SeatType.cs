using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SeatType
    {
        public SeatType()
        {
            Seat = new HashSet<Seat>();
        }
        
        [Display(Name = "Type")]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Relative Quality")]
        public int? RelativeQuality { get; set; }

        [Display(Name = "Seats")]
        public ICollection<Seat> Seat { get; set; }
    }
}
