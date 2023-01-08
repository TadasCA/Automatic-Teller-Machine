using Automatic_Teller_Machine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Repositories
{
    public class TransactionRepo
    {
        Database databaseObject;

        public TransactionRepo(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<Transaction> GetSortedTransactions() 
        {
            List<Transaction> sortedTransactionData = new List<Transaction>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            using (var connection = databaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $@"SELECT 
                                    transaction.trnID,
                                    transaction.date,
                                    transaction.amount,
                                    transaction.userID,
                                    user.name AS userName,
                                    user.password AS userPassword,
                                    user.iban AS userIban,
                                    transaction.bankCardGUID,
                                    bankCard.cardNumber AS cardNumber,
                                    bankCard.balance AS balance,

                                    FROM transaction

                                    INNER JOIN user
                                    ON transaction.userID = user.id
                                    INNER JOIN bankCard
                                    ON transaction.bankCardGUID = bankCard.BCguid

                                    ORDER BY user.name ASC, transaction.date ASC";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sortedTransactionData.Add(
                                new Transaction(
                                Convert.ToInt32(reader["trnID"]),
                                Convert.ToDateTime(reader["date"]),
                                Convert.ToInt32(reader["amount"]),
                                new User(
                                    Convert.ToInt32(reader["userID"]),
                                    reader["userName"].ToString(),
                                    reader["userPassword"].ToString(),
                                    reader["userIban"].ToString()),
                                new BankCard(
                                    (Guid)(reader["bankCardGUID"]),
                                    Convert.ToInt32(reader["cardNumber"]),
                                    Convert.ToDouble(reader["balance"]))));
                        }
                    }
                    return sortedTransactionData;
                }
            }
        }

        public List<Transaction> GetTransactions(int userId)
        {
            List<Transaction> userTransactions = new List<Transaction>();

            using (var connection = databaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $@"SELECT 
                                    transaction.trnID,
                                    transaction.date,
                                    transaction.amount,
                                    transaction.userID,
                                    user.name AS userName,
                                    user.password AS userPassword,
                                    user.iban AS userIban,
                                    transaction.bankCardGUID,
                                    bankCard.cardNumber AS cardNumber,
                                    bankCard.balance AS balance,

                                    FROM transaction

                                    INNER JOIN user
                                    ON transaction.userID = user.id
                                    INNER JOIN bankCard
                                    ON transaction.bankCardGUID = bankCard.BCguid

                                    WHERE transaction.userID = {userId}

                                    ORDER BY user.name ASC, transaction.date ASC";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userTransactions.Add(
                                new Transaction(
                                Convert.ToInt32(reader["trnID"]),
                                Convert.ToDateTime(reader["date"]),
                                Convert.ToInt32(reader["amount"]),
                                new User(
                                    Convert.ToInt32(reader["userID"]),
                                    reader["userName"].ToString(),
                                    reader["userPassword"].ToString(),
                                    reader["userIban"].ToString()),
                                new BankCard(
                                    (Guid)(reader["bankCardGUID"]),
                                    Convert.ToInt32(reader["cardNumber"]),
                                    Convert.ToDouble(reader["balance"]))));
                        }
                    }
                    return userTransactions;
                }
            }
        }
        public int AddTransaction(Transaction transaction)
        {
            using (var connection = databaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO transaction (trnID, date, amount, userID, userName, userPassword, userIban, bankCardGUID, cardNumber, balance) 
                                        VALUES (@trnID, @date, @amount, @userID, @userName, @userPassword, @userIban, @bankCardGUID, @cardNumber, @balance)";
                connection.Open();
                command.Parameters.AddWithValue("@trnID", transaction.TrnID);
                command.Parameters.AddWithValue("@date", transaction.Date);
                command.Parameters.AddWithValue("@amount", transaction.Amount);
                command.Parameters.AddWithValue("@userID", transaction.User.Id);
                command.Parameters.AddWithValue("@userName", transaction.User.Name);
                command.Parameters.AddWithValue("@userPassword", transaction.User.Password);
                command.Parameters.AddWithValue("@userIban", transaction.User.Iban);
                command.Parameters.AddWithValue("@bankCardGUID", transaction.BankCard.BCGuid);
                command.Parameters.AddWithValue("@cardNumber", transaction.BankCard.cardNumber);
                command.Parameters.AddWithValue("@balance", transaction.BankCard.Balance);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }
    } 
}
