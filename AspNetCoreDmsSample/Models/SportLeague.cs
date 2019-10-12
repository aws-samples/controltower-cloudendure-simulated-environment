using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportLeague
    {
        public SportLeague()
        {
            SportDivision = new HashSet<SportDivision>();
        }

        [Display(Name = "Sport")]
        public string SportTypeName { get; set; }
        
        [Display(Name = "League Short Name")]
        [StringLength(10)]
        public string ShortName { get; set; }
        
        [Display(Name = "League Name")]
        public string LongName { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Sport Type")]
        public SportType SportTypeNameNavigation { get; set; }
        
        [Display(Name = "Divisions")]
        public ICollection<SportDivision> SportDivision { get; set; }
    }
}
