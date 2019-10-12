using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DMSSample.Models
{

    public class DBServerTypeModel{
        public String Id { get; set; }
        public String Name { get; set; }

        public DBServerTypeModel(String value){
            Id = value;
            Name = value;
        }
    }


    public class LoginModel
    {
        [Display(Name = "Server Type")]
        public String PrimaryServerType {get; set;}
        
        [Display(Name = "Server Name")]
        public String PrimaryServerName {get; set;}
        
        [Display(Name = "DB Name")]
        public String PrimaryDatabaseName { get; set; }

        [Display(Name = "User Name")]
        public String PrimaryUserName { get; set; }

        [Display(Name = "Password")]
        public String PrimaryPassword { get; set; }

        [Display(Name = "Server Type")]
        public String ReplicaServerType {get; set;}
        [Display(Name = "Server Name")]
        public String ReplicaServerName {get; set;}
        
        [Display(Name = "DB Name")]
        public String ReplicaDatabaseName { get; set; }

        [Display(Name = "User Name")]
        public String ReplicaUserName { get; set; }

        [Display(Name = "Password")]
        public String ReplicaPassword { get; set; }

        [Display(Name = "Access Key")]
        public String AccessKey { get; set; }

        [Display(Name = "Secret Key")]
        public String SecretKey { get; set; }

        [Display(Name = "Replication Task ARN")]
        public String ReplicationTaskArn { get; set; }
        [Display(Name = "Endpoint ARN")]
        public String EndPointArn { get; set; }
    }

    public static class LoginModelExtensions
    {
        public static LoginModel CreateLoginModelFromSession(this ISession session)
        {
            var primaryServerType = session.GetString("PrimaryServerType");
            var primaryServerName = session.GetString("PrimaryServerName");
            var primaryDatabaseName = session.GetString("PrimaryDatabaseName");
            var primaryUserName = session.GetString("PrimaryUserName");
            var primaryPassword = session.GetString("PrimaryPassword");

            var replicaServerType = session.GetString("ReplicaServerType");
            var replicaServerName = session.GetString("ReplicaServerName");
            var replicaDatabaseName = session.GetString("ReplicaDatabaseName");
            var replicaUserName = session.GetString("ReplicaUserName");
            var replicaPassword = session.GetString("ReplicaPassword");

            var accessKey = session.GetString("AccessKey");
            var secretKey = session.GetString("SecretKey");
            var replicationTaskArn = session.GetString("ReplicationTaskArn");
            var endPointArn = session.GetString("EndPointArn");

            return new LoginModel
            {
                PrimaryServerType = primaryServerType,
                PrimaryServerName = primaryServerName,
                PrimaryDatabaseName = primaryDatabaseName,
                PrimaryUserName = primaryUserName,
                PrimaryPassword = primaryPassword,
                ReplicaServerType = replicaServerType,
                ReplicaServerName = replicaServerName,
                ReplicaDatabaseName = replicaDatabaseName,
                ReplicaUserName = replicaUserName,
                ReplicaPassword = replicaPassword,
                AccessKey = accessKey,
                SecretKey = secretKey,
                ReplicationTaskArn = replicationTaskArn,
                EndPointArn = endPointArn

            };
        }
    }
}