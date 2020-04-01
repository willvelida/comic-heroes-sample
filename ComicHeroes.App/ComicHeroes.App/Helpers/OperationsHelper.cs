using ComicHeroes.App.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicHeroes.App.Helpers
{
    public class OperationsHelper
    {
        public static async Task<HeroEntity> InsertOrMergeHero(CloudTable table, HeroEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                HeroEntity heroEntity = result.Result as HeroEntity;

                return heroEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. Exception thrown: {ex.Message}");
                Console.ReadLine();
                throw;
            }
        }

        public static async Task<HeroEntity> GetHero(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation getOperation = TableOperation.Retrieve<HeroEntity>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(getOperation);
                HeroEntity heroEntity = result.Result as HeroEntity;

                if (heroEntity != null)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        heroEntity.PartitionKey,
                        heroEntity.RowKey,
                        heroEntity.ComicBookUniverse,
                        heroEntity.YearFirstAppeared,
                        heroEntity.Hometown,
                        heroEntity.TeamAffiliation);
                }

                return heroEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. Exception thrown: {ex.Message}");
                Console.ReadLine();
                throw;
            }
        }

        public static async Task DeleteHero(CloudTable table, HeroEntity heroEntity)
        {
            try
            {
                if (heroEntity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                TableOperation deleteOperation = TableOperation.Delete(heroEntity);
                TableResult result = await table.ExecuteAsync(deleteOperation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. Exception thrown: {ex.Message}");
                Console.ReadLine();
                throw;
            }
        }
    }
}
