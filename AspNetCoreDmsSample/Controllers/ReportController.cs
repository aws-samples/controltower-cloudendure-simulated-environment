using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMSSample.Models;
using DMSSample.Services;
using Amazon.DatabaseMigrationService;
using Amazon.Extensions.NETCore.Setup;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime.CredentialManagement;
using DMSSample.Constants;
using Microsoft.AspNetCore.Http;

namespace DMSSample.Controllers
{
    public class ReportController : BaseController
    {
        IAmazonDatabaseMigrationService DbMigrationService { get; set; }

        public ReportController(IDbContextService dbContextService, IAmazonDatabaseMigrationService dbMigrationService) : base(dbContextService){
            this.DbMigrationService = dbMigrationService;
        }

        public async Task<IActionResult> NameDataCount(){
            
            RecordCounts recordCounts = new RecordCounts();
            var nameData = from n in _context.NameData select n;
            var nameDataR = from n in _replicaContext.NameData select n;
            recordCounts.Table = "Names Data";
            recordCounts.Primary = await nameData.CountAsync();
            recordCounts.Replica = await nameDataR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "user";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);

        }

        public async Task<IActionResult> PersonCount(){
            RecordCounts recordCounts = new RecordCounts();

            var person = from p in _context.Person select p;
            var personR = from pr in _replicaContext.Person select pr;
            recordCounts.Table = "Persons";
            recordCounts.Primary = await person.CountAsync();
            recordCounts.Replica = await personR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "user";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }

        public async Task<IActionResult> PlayerCount(){
            RecordCounts recordCounts = new RecordCounts();

            var player = from p in _context.Player select p;
            var playerR = from pr in _replicaContext.Player select pr;
            recordCounts.Table = "Players";
            recordCounts.Primary = await player.CountAsync();
            recordCounts.Replica = await playerR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "user";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SeatCount(){
            RecordCounts recordCounts = new RecordCounts();

            var seat = from s in _context.Seat select s;
            var seatR = from s in _replicaContext.Seat select s;
            recordCounts.Table = "Seats";
            recordCounts.Primary = await seat.CountAsync();
            recordCounts.Replica = await seatR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";

            recordCounts.Icon = "globe";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SeatTypeCount(){
            RecordCounts recordCounts = new RecordCounts();

            var seatType = from s in _context.SeatType select s;
            var seatTypeR = from s in _replicaContext.SeatType select s;
            recordCounts.Table = "Seat Types";
            recordCounts.Primary = await seatType.CountAsync();
            recordCounts.Replica = await seatTypeR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "globe";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportDivisionCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportDivision = from s in _context.SportDivision select s;
            var sportDivisionR = from s in _replicaContext.SportDivision select s;
            recordCounts.Table = "Divisions";
            recordCounts.Primary = await sportDivision.CountAsync();
            recordCounts.Replica = await sportDivisionR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "trophy";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportingEventCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportingEvent = from s in _context.SportingEvent select s;
            var sportingEventR = from s in _replicaContext.SportingEvent select s;
            recordCounts.Table = "Events";
            recordCounts.Primary = await sportingEvent.CountAsync();
            recordCounts.Replica = await sportingEventR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "soccer-ball-o";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportingEventTicketCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportingEventTicket = from s in _context.SportingEventTicket select s;
            var sportingEventTicketR = from s in _replicaContext.SportingEventTicket select s;
            recordCounts.Table = "Tickets";
            recordCounts.Primary = await sportingEventTicket.CountAsync();
            recordCounts.Replica = await sportingEventTicketR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "ticket";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportLeagueCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportLeague = from s in _context.SportLeague select s;
            var sportLeagueR = from s in _replicaContext.SportLeague select s;
            recordCounts.Table = "Leagues";
            recordCounts.Primary = await sportLeague.CountAsync();
            recordCounts.Replica = await sportLeagueR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "trophy";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportLocationCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportLocation = from s in _context.SportLocation select s;
            var sportLocationR = from s in _replicaContext.SportLocation select s;
            recordCounts.Table = "Locations";
            recordCounts.Primary = await sportLocation.CountAsync();
            recordCounts.Replica = await sportLocationR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "globe";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public async Task<IActionResult> SportTeamCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportTeam = from s in _context.SportTeam select s;
            var sportTeamR = from s in _replicaContext.SportTeam select s;
            recordCounts.Table = "Teams";
            recordCounts.Primary = await sportTeam.CountAsync();
            recordCounts.Replica = await sportTeamR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "users";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }

        public async Task<IActionResult> SportTypeCount(){
            RecordCounts recordCounts = new RecordCounts();

            var sportType = from s in _context.SportType select s;
            var sportTypeR = from s in _replicaContext.SportType select s;
            recordCounts.Table = "Sports";
            recordCounts.Primary = await sportType.CountAsync();
            recordCounts.Replica = await sportTypeR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "soccer-ball-o";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }

        public async Task<IActionResult> TicketPurchaseHistCount(){
            RecordCounts recordCounts = new RecordCounts();

            var ticketPurchaseHist = from s in _context.TicketPurchaseHist select s;
            var ticketPurchaseHistR = from s in _replicaContext.TicketPurchaseHist select s;
            recordCounts.Table = "Purchase History";
            recordCounts.Primary = await ticketPurchaseHist.CountAsync();
            recordCounts.Replica = await ticketPurchaseHistR.CountAsync();
            recordCounts.Status = (recordCounts.Primary != recordCounts.Replica) 
                                            ? "danger" 
                                            : (recordCounts.Primary == 0) 
                                                ? "warning" 
                                                : "success";
            recordCounts.Icon = "ticket";

            return PartialView("ReplicaCompareRecordsCard", recordCounts);
        }
        public IActionResult ReplicaCompareRecords()
        {
            return View();
        }

        // GET: Player
        public async Task<IActionResult> ReplicationStatistics(){
            
            NetSDKCredentialsFile credentialsFile = new NetSDKCredentialsFile();
            CredentialProfileOptions options = new CredentialProfileOptions();
            options.AccessKey = HttpContext.Session.GetString("AccessKey");
            options.SecretKey = HttpContext.Session.GetString("SecretKey");
            credentialsFile.RegisterProfile(new CredentialProfile("default", options));

            DescribeTableStatisticsRequest request = new DescribeTableStatisticsRequest();
            request.ReplicationTaskArn = HttpContext.Session.GetString("ReplicationTaskArn");
            
            var statistics = await DbMigrationService.DescribeTableStatisticsAsync(request);

            List<DisplayTableStatistics> tableStatistics = new List<DisplayTableStatistics>();
            foreach(TableStatistics stat in statistics.TableStatistics){
                tableStatistics.Add(new DisplayTableStatistics(stat, displayIcon(stat.TableName), statusColor(stat)));
            }

            return View(tableStatistics);
        }

        public async Task<IActionResult> ReplicationDetails(String tableName){
            
            NetSDKCredentialsFile credentialsFile = new NetSDKCredentialsFile();
            CredentialProfileOptions options = new CredentialProfileOptions();
            options.AccessKey = HttpContext.Session.GetString("AccessKey");
            options.SecretKey = HttpContext.Session.GetString("SecretKey");
            credentialsFile.RegisterProfile(new CredentialProfile("default", options));

            DescribeTableStatisticsRequest request = new DescribeTableStatisticsRequest();
            request.ReplicationTaskArn = HttpContext.Session.GetString("ReplicationTaskArn");

            request.Filters.Add(new Filter {Name = "table-name", Values = { tableName }});
            
            var statistics = await DbMigrationService.DescribeTableStatisticsAsync(request);

            var item = statistics.TableStatistics.SingleOrDefault(t => t.TableName == tableName);
            if(item == null){
                return NotFound();
            }

            return View(new DisplayTableStatistics(item, displayIcon(item.TableName), statusColor(item)));

        }
        private String displayIcon(String tableName){
            if(Icons.TableIcons.ContainsKey(tableName)){
                return Icons.TableIcons.GetValueOrDefault(tableName);
            }else{
                return "fa-table";
            }
        }
        private String statusColor(TableStatistics statistics){

            switch(statistics.TableState.ToUpper()){
                case TableStates.TABLE_NOT_EXIST:
                    return "bg-light";
                case TableStates.TABLE_ERROR:
                    return "bg-danger";
                case TableStates.TABLE_BEING_RELOADED:
                case TableStates.BEFORE_LOAD:
                    return "bg-info";
                case TableStates.TABLE_CANCELLED:
                    return "bg-warning";
                case TableStates.TABLE_COMPLETED:
                case TableStates.TABLE_ALL:
                case TableStates.TABLE_UPDATES:
                case TableStates.FULL_LOAD:
                    switch(statistics.ValidationState.ToUpper()){
                        case ValidationStates.ERROR:
                        case ValidationStates.MISMATCHED_RECORDS:
                        case ValidationStates.TABLE_ERROR:
                            return "bg-danger";
                        case ValidationStates.NO_PRIMARY_KEY:
                        case ValidationStates.NOT_ENABLED:
                        case ValidationStates.SUSPENDED_RECORDS:
                            return "bg-warning";
                        case ValidationStates.PENDING_RECORDS:
                            return "bg-primary";
                        case ValidationStates.VALIDATED:
                            return "bg-success";
                        default:
                            return "bg-secondary";
                    }
                default:
                    return "bg-secondary";
            }
        }
    }
}
