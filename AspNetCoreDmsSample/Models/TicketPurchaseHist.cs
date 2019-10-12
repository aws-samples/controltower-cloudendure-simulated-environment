using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class TicketPurchaseHist
    {
        
        [Display(Name = "Ticket")]
        public long SportingEventTicketId { get; set; }
        
        [Display(Name = "Purchased By")]
        public int PurchasedById { get; set; }
        
        [Display(Name = "Transaction Date Time")]
        public DateTime TransactionDateTime { get; set; }
        
        [Display(Name = "Transferred From")]
        public int? TransferredFromId { get; set; }
        
        [Display(Name = "Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Purchased By")]
        public Person PurchasedBy { get; set; }

        [Display(Name = "Event Ticket")]
        public SportingEventTicket SportingEventTicket { get; set; }

        [Display(Name = "Transferred From")]
        public Person TransferredFrom { get; set; }
    }
}
