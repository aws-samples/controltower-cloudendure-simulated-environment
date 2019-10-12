using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql;

namespace DMSSample.Models
{
    public partial class MySQLContext : BaseContext
    {

        public MySQLContext(string connectionString) : base(connectionString){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameData>(entity =>
            {
                entity.HasKey(e => new { e.NameType, e.Name });

                entity.ToTable("name_data");

                entity.Property(e => e.NameType)
                    .HasColumnName("name_type")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasMaxLength(60);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.HasIndex(e => e.SportTeamId)
                    .HasName("sport_team_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(30);

                entity.Property(e => e.SportTeamId)
                    .HasColumnName("sport_team_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SportTeam)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.SportTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sport_team_fk");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => new { e.SportLocationId, e.SeatLevel, e.SeatSection, e.SeatRow, e.Seat1 });

                entity.ToTable("seat");

                entity.HasIndex(e => e.SeatType)
                    .HasName("seat_type_fk");

                entity.HasIndex(e => e.SportLocationId)
                    .HasName("seat_sport_location_idx");

                entity.Property(e => e.SportLocationId)
                    .HasColumnName("sport_location_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeatLevel)
                    .HasColumnName("seat_level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeatSection)
                    .HasColumnName("seat_section")
                    .HasMaxLength(15);

                entity.Property(e => e.SeatRow)
                    .HasColumnName("seat_row")
                    .HasMaxLength(10);

                entity.Property(e => e.Seat1)
                    .HasColumnName("seat")
                    .HasMaxLength(10);

                entity.Property(e => e.SeatType)
                    .HasColumnName("seat_type")
                    .HasMaxLength(15);

                entity.HasOne(d => d.SeatTypeNavigation)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.SeatType)
                    .HasConstraintName("seat_type_fk");

                entity.HasOne(d => d.SportLocation)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.SportLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("s_sport_location_fk");
            });

            modelBuilder.Entity<SeatType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("seat_type");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(120);

                entity.Property(e => e.RelativeQuality)
                    .HasColumnName("relative_quality")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SportDivision>(entity =>
            {
                entity.HasKey(e => new { e.SportTypeName, e.SportLeagueShortName, e.ShortName });

                entity.ToTable("sport_division");

                entity.HasIndex(e => e.SportLeagueShortName)
                    .HasName("sd_sport_league_fk");

                entity.Property(e => e.SportTypeName)
                    .HasColumnName("sport_type_name")
                    .HasMaxLength(15);

                entity.Property(e => e.SportLeagueShortName)
                    .HasColumnName("sport_league_short_name")
                    .HasMaxLength(10);

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(120);

                entity.Property(e => e.LongName)
                    .HasColumnName("long_name")
                    .HasMaxLength(60);

                entity.HasOne(d => d.SportLeagueShortNameNavigation)
                    .WithMany(p => p.SportDivision)
                    .HasForeignKey(d => d.SportLeagueShortName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sd_sport_league_fk");

                entity.HasOne(d => d.SportTypeNameNavigation)
                    .WithMany(p => p.SportDivision)
                    .HasForeignKey(d => d.SportTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sd_sport_type_fk");
            });

            modelBuilder.Entity<SportingEvent>(entity =>
            {
                entity.ToTable("sporting_event");

                entity.HasIndex(e => e.AwayTeamId)
                    .HasName("se_away_team_id_fk");

                entity.HasIndex(e => e.HomeTeamId)
                    .HasName("se_home_team_id_fk");

                entity.HasIndex(e => e.LocationId)
                    .HasName("se_location_id_fk");

                entity.HasIndex(e => e.SportTypeName)
                    .HasName("se_sport_type_fk");

                entity.HasIndex(e => e.StartDate)
                    .HasName("se_start_date");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AwayTeamId)
                    .HasColumnName("away_team_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HomeTeamId)
                    .HasColumnName("home_team_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SoldOut)
                    .HasColumnName("sold_out")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SportTypeName)
                    .IsRequired()
                    .HasColumnName("sport_type_name")
                    .HasMaxLength(15);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartDateTime)
                    .HasColumnName("start_date_time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.SportingEventAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("se_away_team_id_fk");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.SportingEventHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("se_home_team_id_fk");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.SportingEvent)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("se_location_id_fk");

                entity.HasOne(d => d.SportTypeNameNavigation)
                    .WithMany(p => p.SportingEvent)
                    .HasForeignKey(d => d.SportTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("se_sport_type_fk");
            });

            modelBuilder.Entity<SportingEventTicket>(entity =>
            {
                entity.ToTable("sporting_event_ticket");

                entity.HasIndex(e => e.SportingEventId)
                    .HasName("set_sporting_event_idx");

                entity.HasIndex(e => e.TicketholderId)
                    .HasName("set_ticketholder_idx");

                entity.HasIndex(e => new { e.SportingEventId, e.TicketholderId })
                    .HasName("set_ev_id_tkholder_id_idx");

                entity.HasIndex(e => new { e.SportLocationId, e.SeatLevel, e.SeatSection, e.SeatRow, e.Seat })
                    .HasName("set_seat_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Seat)
                    .IsRequired()
                    .HasColumnName("seat")
                    .HasMaxLength(10);

                entity.Property(e => e.SeatLevel)
                    .HasColumnName("seat_level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeatRow)
                    .IsRequired()
                    .HasColumnName("seat_row")
                    .HasMaxLength(10);

                entity.Property(e => e.SeatSection)
                    .IsRequired()
                    .HasColumnName("seat_section")
                    .HasMaxLength(15);

                entity.Property(e => e.SportLocationId)
                    .HasColumnName("sport_location_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SportingEventId)
                    .HasColumnName("sporting_event_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TicketPrice)
                    .HasColumnName("ticket_price")
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.TicketholderId)
                    .HasColumnName("ticketholder_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SportingEvent)
                    .WithMany(p => p.SportingEventTicket)
                    .HasForeignKey(d => d.SportingEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("set_sporting_event_fk");

                entity.HasOne(d => d.Ticketholder)
                    .WithMany(p => p.SportingEventTicket)
                    .HasForeignKey(d => d.TicketholderId)
                    .HasConstraintName("set_person_id");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.SportingEventTicket)
                    .HasForeignKey(d => new { d.SportLocationId, d.SeatLevel, d.SeatSection, d.SeatRow, d.Seat })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("set_seat_fk");
            });

            modelBuilder.Entity<SportLeague>(entity =>
            {
                entity.HasKey(e => e.ShortName);

                entity.ToTable("sport_league");

                entity.HasIndex(e => e.SportTypeName)
                    .HasName("sl_sport_type_fk");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(120);

                entity.Property(e => e.LongName)
                    .IsRequired()
                    .HasColumnName("long_name")
                    .HasMaxLength(60);

                entity.Property(e => e.SportTypeName)
                    .IsRequired()
                    .HasColumnName("sport_type_name")
                    .HasMaxLength(15);

                entity.HasOne(d => d.SportTypeNameNavigation)
                    .WithMany(p => p.SportLeague)
                    .HasForeignKey(d => d.SportTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sl_sport_type_fk");
            });

            modelBuilder.Entity<SportLocation>(entity =>
            {
                entity.ToTable("sport_location");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(60);

                entity.Property(e => e.Levels)
                    .HasColumnName("levels")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60);

                entity.Property(e => e.SeatingCapacity)
                    .HasColumnName("seating_capacity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sections)
                    .HasColumnName("sections")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SportTeam>(entity =>
            {
                entity.ToTable("sport_team");

                entity.HasIndex(e => e.HomeFieldId)
                    .HasName("home_field_fk");

                entity.HasIndex(e => new { e.SportTypeName, e.SportLeagueShortName, e.Name })
                    .HasName("sport_team_u")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AbbreviatedName)
                    .HasColumnName("abbreviated_name")
                    .HasMaxLength(10);

                entity.Property(e => e.HomeFieldId)
                    .HasColumnName("home_field_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.SportDivisionShortName)
                    .HasColumnName("sport_division_short_name")
                    .HasMaxLength(10);

                entity.Property(e => e.SportLeagueShortName)
                    .IsRequired()
                    .HasColumnName("sport_league_short_name")
                    .HasMaxLength(10);

                entity.Property(e => e.SportTypeName)
                    .IsRequired()
                    .HasColumnName("sport_type_name")
                    .HasMaxLength(15);

                entity.HasOne(d => d.HomeField)
                    .WithMany(p => p.SportTeam)
                    .HasForeignKey(d => d.HomeFieldId)
                    .HasConstraintName("home_field_fk");

                entity.HasOne(d => d.SportTypeNameNavigation)
                    .WithMany(p => p.SportTeam)
                    .HasForeignKey(d => d.SportTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("st_sport_type_fk");
            });

            modelBuilder.Entity<SportType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("sport_type");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<TicketPurchaseHist>(entity =>
            {
                entity.HasKey(e => new { e.SportingEventTicketId, e.PurchasedById, e.TransactionDateTime });

                entity.ToTable("ticket_purchase_hist");

                entity.HasIndex(e => e.PurchasedById)
                    .HasName("tph_purch_by_id");

                entity.HasIndex(e => e.TransferredFromId)
                    .HasName("tph_trans_from_id");

                entity.Property(e => e.SportingEventTicketId)
                    .HasColumnName("sporting_event_ticket_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PurchasedById)
                    .HasColumnName("purchased_by_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionDateTime)
                    .HasColumnName("transaction_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnName("purchase_price")
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.TransferredFromId)
                    .HasColumnName("transferred_from_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PurchasedBy)
                    .WithMany(p => p.TicketPurchaseHistPurchasedBy)
                    .HasForeignKey(d => d.PurchasedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tph_ticketholder_id");

                entity.HasOne(d => d.SportingEventTicket)
                    .WithMany(p => p.TicketPurchaseHist)
                    .HasForeignKey(d => d.SportingEventTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tph_sport_event_tic_id");

                entity.HasOne(d => d.TransferredFrom)
                    .WithMany(p => p.TicketPurchaseHistTransferredFrom)
                    .HasForeignKey(d => d.TransferredFromId)
                    .HasConstraintName("tph_transfer_from_id");
            });
        }
    }
}
