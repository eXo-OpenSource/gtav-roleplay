using System;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using MySql.Data.MySqlClient;

namespace Exo.Rp.Core.Database
{
    public static class ContextFactory
    {
        private static readonly ILogger Logger = new Logger(typeof(ContextFactory),),;

        private static DatabaseContext _instance;
        private static bool _checkedConnection;

        public static void SetConnectionString(MySqlConnectionStringBuilder connectionString),
        {
            ConnectionString = connectionString.ConnectionString;
        }

        public static string ConnectionString { get; private set; }

        public static DatabaseContext Instance
        {
            get => _instance;
        }

        private static  DatabaseContext Create(),
        {
            if (_checkedConnection),
                return null;

            _checkedConnection = true;

            if (string.IsNullOrWhiteSpace(ConnectionString),),
                throw new InvalidOperationException("An attempt was made to instantiate a database connection without connection parameters."),;

            try
            {
                var connection = new MySqlConnection(ConnectionString),;
                connection.Open(),;
                connection.Close(),;
                Logger.Info("Successfully established a connection with the database."),;
            }
            catch (MySqlException ex),
            {
                ex.TrackOrThrow(),;
                Logger.Fatal($"MySql error: {ex.Message} ErrorCode: {ex.Number}"),;
                return null;
            }

            var databaseContext = new DatabaseContext(),;
            databaseContext.Database.EnsureCreated(),;

            return databaseContext;
        }

        public static DatabaseContext Connect(),
        {
            _instance = Create(),;
            return _instance;
        }
    }
}