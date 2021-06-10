using Microsoft.Extensions.Configuration;
using System;

namespace Totvs.Sample.Shop.Infra
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.Sqlite.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.Sqlite;
            }
            else
            {
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");
            }

            ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
        }

        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        Sqlite
    }
}