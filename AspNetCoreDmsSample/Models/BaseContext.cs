using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql;

namespace DMSSample.Models
{
    public partial class BaseContext : DbContext
    {

        #region  Configuring Multi-Tenancy on Database Connection
        public BaseContext(String connectionString){
            ConnectionString = connectionString;
        }
        public String ConnectionString { get; }
        #endregion

        public virtual DbSet<NameData> NameData { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<SeatType> SeatType { get; set; }
        public virtual DbSet<SportDivision> SportDivision { get; set; }
        public virtual DbSet<SportingEvent> SportingEvent { get; set; }
        public virtual DbSet<SportingEventTicket> SportingEventTicket { get; set; }
        public virtual DbSet<SportLeague> SportLeague { get; set; }
        public virtual DbSet<SportLocation> SportLocation { get; set; }
        public virtual DbSet<SportTeam> SportTeam { get; set; }
        public virtual DbSet<SportType> SportType { get; set; }
        public virtual DbSet<TicketPurchaseHist> TicketPurchaseHist { get; set; }

        // Unable to generate entity type for table 'mlb_data'. Please see the warning messages.
        // Unable to generate entity type for table 'nfl_data'. Please see the warning messages.
        // Unable to generate entity type for table 'nfl_stadium_data'. Please see the warning messages.
    }
}