using Automatic_Teller_Machine.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Repositories
{
    public class ATMRepo
    {

        Database databaseObject;

        public ATMRepo(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<Guid> GetAllBanksGuids()
        {
            List<Guid> banksGuids = new List<Guid>();

            using (var connection = databaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM atms";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            banksGuids.Add((Guid)reader["guid"]);
                        }
                    }
                    return banksGuids;
                }
            }
        }
        public ATM GetATM(Guid bankGuid)
        {
            ATM atm = null;

            using (var connection = databaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM banks WHERE id = {bankGuid}"; ;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            atm = new ATM
                                (
                                (Guid)reader["guid"],
                                reader["bankName"].ToString()
                                );
                        }
                    }
                    return atm;
                }
            }
        }
    }
}
