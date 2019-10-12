using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{
    public partial class ReplicaCompareRecords
    {        
        public ReplicaCompareRecords(){
            NameData = new RecordCounts();
            Person = new RecordCounts();
            Player = new RecordCounts();
            Seat = new RecordCounts();
            SeatType = new RecordCounts();
            SportDivision = new RecordCounts();
            SportingEvent = new RecordCounts();
            SportingEventTicket = new RecordCounts();
            SportLeague = new RecordCounts();
            SportLocation = new RecordCounts();
            SportTeam = new RecordCounts();
            SportType = new RecordCounts();
            TicketPurchaseHist = new RecordCounts();
        }
        public RecordCounts NameData { get; set; }
        public RecordCounts Person { get; set; }
        public RecordCounts Player { get; set; }
        public RecordCounts Seat { get; set; }
        public RecordCounts SeatType { get; set; }
        public RecordCounts SportDivision { get; set; }
        public RecordCounts SportingEvent { get; set; }
        public RecordCounts SportingEventTicket { get; set; }
        public RecordCounts SportLeague { get; set; }
        public RecordCounts SportLocation { get; set; }
        public RecordCounts SportTeam { get; set; }
        public RecordCounts SportType { get; set; }
        public RecordCounts TicketPurchaseHist { get; set; }
    }

    public class RecordCounts{

        public String Table { get; set; }
        public String Status { get; set; }
        public String Icon { get; set; }
        public int Primary { get; set; }
        public int Replica { get;  set; }

    }
}
