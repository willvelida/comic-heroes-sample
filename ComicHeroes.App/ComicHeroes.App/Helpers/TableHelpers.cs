using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicHeroes.App.Helpers
{
    public class TableHelpers
    {
        public static CloudStorageAccount ConnectToTableStorage(string connectionString)
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. Exception thrown: {ex.Message}");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public static CloudTableClient ConnectToClient(string connectionString)
        {
            CloudStorageAccount storageAccount = ConnectToTableStorage(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(
                new TableClientConfiguration());

            return tableClient;
        }
    }
}
