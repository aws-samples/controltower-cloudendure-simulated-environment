using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportType
    {
        public SportType()
        {
            SportDivision = new HashSet<SportDivision>();
            SportLeague = new HashSet<SportLeague>();
            SportTeam = new HashSet<SportTeam>();
            SportingEvent = new HashSet<SportingEvent>();
        }

        
        [Display(Name = "Sport Type")]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Divisions")]
        public ICollection<SportDivision> SportDivision { get; set; }

        [Display(Name = "Leagues")]
        public ICollection<SportLeague> SportLeague { get; set; }

        [Display(Name = "Teams")]
        public ICollection<SportTeam> SportTeam { get; set; }

        [Display(Name = "Events")]
        public ICollection<SportingEvent> SportingEvent { get; set; }
    }
}
