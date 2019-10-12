using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.DatabaseMigrationService.Model;

namespace DMSSample.Models
{
    public partial class DisplayTableStatistics
    {        

        public DisplayTableStatistics(TableStatistics tableStatistics, String displayIcon, String statusColor){
            this.Statistics = tableStatistics;
            this.DisplayIcon = displayIcon;
            this.StatusColor = statusColor;
        }

        public string DisplayIcon { get; set; }
        public string StatusColor { get; set; }
        public TableStatistics Statistics { get; set; }
    }
}
