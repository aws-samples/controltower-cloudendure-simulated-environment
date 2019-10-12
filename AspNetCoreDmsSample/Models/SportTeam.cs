using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportTeam
    {
        public SportTeam()
        {
            Player = new HashSet<Player>();
            SportingEventAwayTeam = new HashSet<SportingEvent>();
            SportingEventHomeTeam = new HashSet<SportingEvent>();
        }

        
        [Display(Name = "ID")]
        public int Id { get; set; }
        
        [Display(Name = "Team")]
        public string Name { get; set; }
        
        [Display(Name = "Abbreviated Name")]
        public string AbbreviatedName { get; set; }
        
        [Display(Name = "Home Field ID")]
        public int? HomeFieldId { get; set; }

        [Display(Name = "Sport")]
        public string SportTypeName { get; set; }
        
        [Display(Name = "League")]
        public string SportLeagueShortName { get; set; }
        
        [Display(Name = "Division")]
        public string SportDivisionShortName { get; set; }

        [Display(Name = "Home Field")]
        public SportLocation HomeField { get; set; }

        [Display(Name = "Sport Type")]
        public SportType SportTypeNameNavigation { get; set; }

        [Display(Name = "Players")]
        public ICollection<Player> Player { get; set; }

        [Display(Name = "Away Events")]
        public ICollection<SportingEvent> SportingEventAwayTeam { get; set; }

        [Display(Name = "Home Events")]
        public ICollection<SportingEvent> SportingEventHomeTeam { get; set; }
    }
}
