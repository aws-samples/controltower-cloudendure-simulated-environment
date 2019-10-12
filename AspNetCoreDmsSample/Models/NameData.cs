using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class NameData
    {        
        [Display(Name = "Name Type")]
        public string NameType { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
