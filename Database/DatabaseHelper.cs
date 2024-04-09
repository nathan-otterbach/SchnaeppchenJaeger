using System.Data;
using System.Data.SQLite;
using SchnaeppchenJaeger.Model;

namespace SchnaeppchenJaeger.Database
{
    /// <summary>
    /// Database helper class for interacting with SQLite database.
    /// </summary>
    public class DatabaseHelper : IDisposable
    {
        private static readonly Lazy<DatabaseHelper> _instance = new Lazy<DatabaseHelper>(() => new DatabaseHelper());

        private SQLiteConnection _connection;
        private SQLiteTransaction _transaction;
        private bool _disposed = false;

        /// <summary>
        /// Private constructor to enforce singleton pattern and initialize database connection.
        /// </summary>
        private DatabaseHelper()
        {
            var connectionString = $"Data Source={GetDatabaseFilePath()};Version=3;";
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Singleton instance property.
        /// </summary>
        public static DatabaseHelper Instance => _instance.Value;

        /// <summary>
        /// Method to create a shopping list table.
        /// </summary>
        /// <param name="tableName">Name of the table to create.</param>
        public void CreateShoppingListTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

            string commandText = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                                    AdvertiserName TEXT,
                                    Description TEXT,
                                    Price REAL,
                                    ReferencePrice REAL,
                                    Unit TEXT,
                                    FromDate DATETIME,
                                    ToDate DATETIME,
                                    RequiresLoyaltyMembership INTEGER)";

            using (var command = new SQLiteCommand(commandText, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to insert shopping lists into a table.
        /// </summary>
        /// <param name="tableName">Name of the table to insert into.</param>
        /// <param name="populatedData">Dictionary containing populated data for shopping lists.</param>
        public void InsertShoppingLists(string tableName, Dictionary<string, string> populatedData)
        {
            for (int i = 0; i < populatedData.Count; i++)
            {
                var shoppingList = new ShoppingList
                {
                    AdvertiserName = populatedData[$"AdvertiserName_{i}"],
                    Description = populatedData[$"Description_{i}"],
                    Price = float.Parse(populatedData[$"Price_{i}"]),
                    ReferencePrice = float.Parse(populatedData[$"ReferencePrice_{i}"]),
                    Unit = populatedData[$"Unit_{i}"],
                    FromDate = DateTime.Parse(populatedData[$"FromDate_{i}"]),
                    ToDate = DateTime.Parse(populatedData[$"ToDate_{i}"]),
                    RequiresLoyaltyMembership = bool.Parse(populatedData[$"RequiresLoyaltyMembership_{i}"])
                };

                InsertShoppingList(tableName, shoppingList);
            }
        }

        /// <summary>
        /// Checks if the connection to the database is successful.
        /// </summary>
        /// <returns>True if connected successfully, otherwise false.</returns>
        public bool IsDatabaseConnected()
        {
            return _connection.State == ConnectionState.Open;
        }

        /// <summary>
        /// Method to commit transaction.
        /// </summary>
        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// Method to rollback transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        /// <summary>
        /// Inserts a single shopping list into the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table to insert into.</param>
        /// <param name="shoppingList">ShoppingList object to insert.</param>
        private void InsertShoppingList(string tableName, ShoppingList shoppingList)
        {
            string commandText = $@"INSERT INTO {tableName} (AdvertiserName, Description, Price, ReferencePrice, Unit, FromDate, ToDate, RequiresLoyaltyMembership)
                                   VALUES (@AdvertiserName, @Description, @Price, @ReferencePrice, @Unit, @FromDate, @ToDate, @RequiresLoyaltyMembership)";

            using (var command = new SQLiteCommand(commandText, _connection))
            {
                command.Parameters.AddWithValue("@AdvertiserName", shoppingList.AdvertiserName);
                command.Parameters.AddWithValue("@Description", shoppingList.Description);
                command.Parameters.AddWithValue("@Price", shoppingList.Price);
                command.Parameters.AddWithValue("@ReferencePrice", shoppingList.ReferencePrice);
                command.Parameters.AddWithValue("@Unit", shoppingList.Unit);
                command.Parameters.AddWithValue("@FromDate", shoppingList.FromDate);
                command.Parameters.AddWithValue("@ToDate", shoppingList.ToDate);
                command.Parameters.AddWithValue("@RequiresLoyaltyMembership", shoppingList.RequiresLoyaltyMembership);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets the file path of the SQLite database file. If the file does not exist, creates a new one.
        /// </summary>
        /// <returns>File path of the SQLite database file.</returns>
        private string GetDatabaseFilePath()
        {
            string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string databaseFilePath = Path.Combine(directory, "database.db");

            if (!File.Exists(databaseFilePath))
            {
                SQLiteConnection.CreateFile(databaseFilePath);
            }

            return databaseFilePath;
        }


        #region IDisposable implementation

        /// <summary>
        /// Dispose method to release resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected method to dispose resources.
        /// </summary>
        /// <param name="disposing">Flag indicating whether disposing is in progress.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                    _connection.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizer to dispose resources.
        /// </summary>
        ~DatabaseHelper() { Dispose(false); }

        #endregion
    }
}