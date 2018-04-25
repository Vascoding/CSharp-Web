namespace BankSystem.Client.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Client.IO;
    using BankSystem.Data.Database;

    public class DeductFeeCommand
    {
        public static void DeductFee(BankSystemDbContext db, string[] tokens, OutputWriter writer)
        {
            var accountNumber = tokens[0];

            try
            {
                var account = db.CheckingAccounts.FirstOrDefault(s => s.AccountNumber == accountNumber);

                account.DeductFee();
                db.SaveChanges();

                writer.WriteLine(string.Format(Messages.SuccessDeductFee, accountNumber, account.Balance));
            }
            catch (Exception)
            {
                writer.WriteLine(string.Format(Messages.CannotDeductFee, accountNumber));
            }
        }
    }
}