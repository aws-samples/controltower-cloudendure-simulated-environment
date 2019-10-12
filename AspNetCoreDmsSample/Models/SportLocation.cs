using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportLocation
    {
        public SportLocation()
        {
            Seat = new HashSet<Seat>();
            SportTeam = new HashSet<SportTeam>();
            SportingEvent = new HashSet<SportingEvent>();
        }

        
        [Display(Name = "ID")]
        public int Id { get; set; }
        
        [Display(Name = "Location")]
        public string Name { get; set; }
        
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Display(Name = "Capcity")]
        public int? SeatingCapacity { get; set; }
        
        [Display(Name = "Levels")]
        public int? Levels { get; set; }
        
        [Display(Name = "Sections")]
        public int? Sections { get; set; }

        [Display(Name = "Seats")]
        public ICollection<Seat> Seat { get; set; }

        [Display(Name = "Teams")]
        public ICollection<SportTeam> SportTeam { get; set; }

        [Display(Name = "Events")]
        public ICollection<SportingEvent> SportingEvent { get; set; }
    }
}
