using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class Person
    {
        public Person()
        {
            SportingEventTicket = new HashSet<SportingEventTicket>();
            TicketPurchaseHistPurchasedBy = new HashSet<TicketPurchaseHist>();
            TicketPurchaseHistTransferredFrom = new HashSet<TicketPurchaseHist>();
        }
        
        [Display(Name = "ID")]
        public int Id { get; set; }
                
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
                
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Event")]
        public ICollection<SportingEventTicket> SportingEventTicket { get; set; }
        
        [Display(Name = "Purchased By")]
        public ICollection<TicketPurchaseHist> TicketPurchaseHistPurchasedBy { get; set; }
        
        [Display(Name = "Transferred From")]
        public ICollection<TicketPurchaseHist> TicketPurchaseHistTransferredFrom { get; set; }
    }
}
