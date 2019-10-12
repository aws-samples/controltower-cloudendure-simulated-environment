using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DMSSample.Models
{
    public partial class SportingEvent
    {
        public SportingEvent()
        {
            SportingEventTicket = new HashSet<SportingEventTicket>();
        }

        [Display(Name = "ID")]
        public long Id { get; set; }
        
        [Display(Name = "Sport")]
        public string SportTypeName { get; set; }
        
        [Display(Name = "Home Team ID")]
        public int HomeTeamId { get; set; }
        
        [Display(Name = "Away Team ID")]
        public int AwayTeamId { get; set; }
        
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }
        
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Sold Out")]
        public int SoldOut { get; set; }

        [Display(Name = "Away Team")]
        public SportTeam AwayTeam { get; set; }
        
        [Display(Name = "Home Team")]
        public SportTeam HomeTeam { get; set; }
        
        [Display(Name = "Location")]
        public SportLocation Location { get; set; }
        
        [Display(Name = "Sport Type")]
        public SportType SportTypeNameNavigation { get; set; }
        
        [Display(Name = "Tickets")]
        public ICollection<SportingEventTicket> SportingEventTicket { get; set; }

        public String EventDescription 
        {
            get
            {
                StringBuilder sb = new  StringBuilder();
                if(HomeTeam != null && AwayTeam != null){
                    sb.AppendFormat("{0} x {1}", HomeTeam.Name, AwayTeam.Name);
                }
                return sb.ToString();
            }
        }
    }
}
