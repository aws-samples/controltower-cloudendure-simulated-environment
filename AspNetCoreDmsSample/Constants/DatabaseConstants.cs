using System;
using System.Collections.Generic;

namespace DMSSample.Constants{

    public class DatabaseConstants{
        public const String SQL_SERVER = "SQLServer";
        public const String MY_SQL = "MySQL";        
    }

    public class TableStates{
        public const String TABLE_NOT_EXIST = "TABLE DOES NOT EXIST";
        public const String BEFORE_LOAD = "BEFORE LOAD";
        public const String FULL_LOAD = "FULL LOAD";
        public const String TABLE_COMPLETED = "TABLE COMPLETED";
        public const String TABLE_CANCELLED = "TABLE CANCELLED";
        public const String TABLE_ERROR = "TABLE ERROR";
        public const String TABLE_ALL = "TABLE ALL";
        public const String TABLE_UPDATES = "TABLE UPDATES";
        public const String TABLE_BEING_RELOADED = "TABLE IS BEING RELOADED";
    }

    public class ValidationStates{
        public const String NOT_ENABLED = "NOT ENABLED";
        public const String PENDING_RECORDS = "PENDING RECORDS";
        public const String MISMATCHED_RECORDS = "MISMATCHED RECORDS";
        public const String SUSPENDED_RECORDS = "SUSPENDED RECORDS";
        public const String NO_PRIMARY_KEY = "NO PRIMARY KEY";
        public const String TABLE_ERROR = "TABLE ERROR";
        public const String VALIDATED = "VALIDATED";
        public const String ERROR = "ERROR";
    }

    public class Icons{
        public static Dictionary<String, String> TableIcons = new Dictionary<String, String> { 
                {"sport_location", "fa-home"},
                {"name_data", "fa-user"},
                {"mlb_data", "fa-line-chart"},
                {"ticket_purchase_hist", "fa-history"},
                {"sport_team", "fa-group"},
                {"nfl_stadium_data", "fa-globe"},
                {"seat_type", "fa-archive"},
                {"sport_type", "fa-futbol-o"},
                {"sporting_event_ticket", "fa-ticket"},
                {"sporting_event", "fa-trophy"},
                {"sport_division", "fa-trophy"},
                {"nfl_data", "fa-area-chart"},
                {"person", "fa-child"},
                {"sport_league", "fa-trophy"},
                {"player", "fa-id-card"},
                {"seat", "fa fa-street-view"}
            };
    }
}
