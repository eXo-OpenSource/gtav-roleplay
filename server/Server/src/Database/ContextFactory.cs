using server.Util.Log;
using System;
using MySql.Data.MySqlClient;

namespace server.Database
{
    public static class ContextFactory
    {
        private static readonly Logger Logger = new Logger(typeof(ContextFactory));

        private static DatabaseContext _instance;
        private static bool _checkedConnection;

        public static void SetConnectionString(MySqlConnectionStringBuilder connectionString)
        {
            ConnectionString = connectionString.ConnectionString;
        }

        public static string ConnectionString { get; private set; }

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance != null) return _instance;
                return _instance = Create();
            }
        }

        private static  DatabaseContext Create()
        {
            if (_checkedConnection)
                return null;

            _checkedConnection = true;

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException("An attempt was made to instantiate a database connection without connection parameters.");

            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                connection.Close();
                Logger.Info("Successfully established a connection with the database.");
            }
            catch (MySqlException ex)
            {
                Logger.Fatal($"MySql error: {ex.Message} ErrorCode: {ex.Number}");
                return null;
            }

            var databaseContext = new DatabaseContext();
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

    }
}
