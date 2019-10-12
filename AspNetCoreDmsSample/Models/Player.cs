using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class Player
    {        
        [Display(Name = "ID")]
        public int Id { get; set; }
                
        [Display(Name = "Team ID")]
        public int SportTeamId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
                
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Team")]
        public SportTeam SportTeam { get; set; }
    }
}
