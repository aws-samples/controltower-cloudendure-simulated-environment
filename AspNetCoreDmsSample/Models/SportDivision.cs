using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportDivision
    {
        
        [Display(Name = "Sport")]
        public string SportTypeName { get; set; }
        
        [Display(Name = "League")]
        public string SportLeagueShortName { get; set; }
        
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        
        [Display(Name = "Name")]
        public string LongName { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "League")]
        public SportLeague SportLeagueShortNameNavigation { get; set; }
        
        [Display(Name = "Sport Type")]
        public SportType SportTypeNameNavigation { get; set; }
    }
}
