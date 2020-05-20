using System;
using Microsoft.Extensions.Options;
using DMSSample.Models;
using Microsoft.EntityFrameworkCore;
using DMSSample.Constants;

namespace DMSSample.Services
{
    public class DbContextService : IDbContextService
    {
        public DbContextService(IOptions<AppSettings> appSettings)
        {
            SQLConnectionString = appSettings.Value.SQLConnectionString;
            MySQLConnectionString = appSettings.Value.MySQLConnectionString;
        }

        public String SQLConnectionString { get; }
        public String MySQLConnectionString { get; }

        public BaseContext CreateDbContext(LoginModel model)
        {
            String primaryConnectionString;
            BaseContext primaryDBContext;
            switch(model.PrimaryServerType){
                case DatabaseConstants.SQL_SERVER:
                    primaryConnectionString = SQLConnectionString.Replace("{server}", model.PrimaryServerName).Replace("{database}", model.PrimaryDatabaseName).Replace("{user id}", model.PrimaryUserName).Replace("{password}", model.PrimaryPassword);
                    primaryDBContext = new SQLContext(primaryConnectionString);
                    break;
                case DatabaseConstants.MY_SQL:
                    primaryConnectionString = MySQLConnectionString.Replace("{server}", model.PrimaryServerName).Replace("{database}", model.PrimaryDatabaseName).Replace("{user id}", model.PrimaryUserName).Replace("{password}", model.PrimaryPassword);
                    primaryDBContext = new MySQLContext(primaryConnectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("PrimaryServerName", null, "Could not establish connection to Primary Database Server");
                    // primaryConnectionString = SQLConnectionString.Replace("{server}", model.PrimaryServerName).Replace("{database}", model.PrimaryDatabaseName).Replace("{user id}", model.PrimaryUserName).Replace("{password}", model.PrimaryPassword);    
                    // primaryDBContext = new SQLContext(primaryConnectionString);
                    // break;
            }
            return primaryDBContext;
        }

        public BaseContext CreateReplicaDbContext(LoginModel model)
        {
            String replicaConnectionString;
            BaseContext replicaDBContext;
            switch(model.ReplicaServerType){
                case DatabaseConstants.SQL_SERVER:
                    replicaConnectionString = SQLConnectionString.Replace("{server}", model.ReplicaServerName).Replace("{database}", model.ReplicaDatabaseName).Replace("{user id}", model.ReplicaUserName).Replace("{password}", model.ReplicaPassword);
                    replicaDBContext = new SQLContext(replicaConnectionString);
                    break;
                case DatabaseConstants.MY_SQL:
                    replicaConnectionString = MySQLConnectionString.Replace("{server}", model.ReplicaServerName).Replace("{database}", model.ReplicaDatabaseName).Replace("{user id}", model.ReplicaUserName).Replace("{password}", model.ReplicaPassword);
                    replicaDBContext = new MySQLContext(replicaConnectionString);
                    break;
                default:
                    replicaConnectionString = SQLConnectionString.Replace("{server}", model.ReplicaServerName).Replace("{database}", model.ReplicaDatabaseName).Replace("{user id}", model.ReplicaUserName).Replace("{password}", model.ReplicaPassword);    
                    replicaDBContext = new SQLContext(replicaConnectionString);
                    break;
            }
            return replicaDBContext;
        }
    }
}