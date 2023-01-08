using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Repositories
{
    public class Database
    {
        private string databaseName;

        private bool newDbCreated = false;
        public bool NewDbCreated { get { return newDbCreated; } }


        public Database(string databaseName)
        {
            this.databaseName = databaseName;

            if (!File.Exists($"./{databaseName}.sqlite"))
            {
                SQLiteConnection.CreateFile($"{databaseName}.sqlite");
                newDbCreated = true;
                Console.WriteLine("Database created!");
            }
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection($"Data Source = {databaseName}.sqlite");
        }
    }
}
