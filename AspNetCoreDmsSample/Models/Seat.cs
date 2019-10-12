using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DMSSample.Models
{
    public partial class Seat
    {
        public Seat()
        {
            SportingEventTicket = new HashSet<SportingEventTicket>();
        }
        
        [Display(Name = "Location ID")]
        public int SportLocationId { get; set; }

        [Display(Name = "Level")]
        public int SeatLevel { get; set; }
        
        [Display(Name = "Section")]
        public string SeatSection { get; set; }
        
        [Display(Name = "Row")]
        public string SeatRow { get; set; }
        
        [Display(Name = "Seat")]
        public string Seat1 { get; set; }
        
        [Display(Name = "Type")]
        public string SeatType { get; set; }

        [Display(Name = "Seat")]
        public SeatType SeatTypeNavigation { get; set; }
        
        [Display(Name = "Location")]
        public SportLocation SportLocation { get; set; }
        
        [Display(Name = "Events")]
        public ICollection<SportingEventTicket> SportingEventTicket { get; set; }
        
    }
}
