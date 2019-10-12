using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class SportingEventTicket
    {
        public SportingEventTicket()
        {
            TicketPurchaseHist = new HashSet<TicketPurchaseHist>();
        }

        [Display(Name = "ID")]
        public long Id { get; set; }
        
        [Display(Name = "Event ID")]
        public long SportingEventId { get; set; }
        
        [Display(Name = "Location ID")]
        public int SportLocationId { get; set; }
        
        [Display(Name = "Level")]
        public int SeatLevel { get; set; }
        
        [Display(Name = "Section")]
        public string SeatSection { get; set; }
        
        [Display(Name = "Row")]
        public string SeatRow { get; set; }
        
        [Display(Name = "Seat")]
        public string Seat { get; set; }
        
        [Display(Name = "Ticket Holder ID")]
        public int? TicketholderId { get; set; }
        
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Display(Name = "Seat")]
        public Seat S { get; set; }

        [Display(Name = "Event")]
        public SportingEvent SportingEvent { get; set; }
        
        [Display(Name = "Ticket Holder")]
        public Person Ticketholder { get; set; }
        
        [Display(Name = "Ticket Purchases")]
        public ICollection<TicketPurchaseHist> TicketPurchaseHist { get; set; }
    }

    public class SportingEventTicketFilter
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SportingEventDateFilter { get; set; }
        public int? SportingEventIdFilter { get; set; }
    }
}
