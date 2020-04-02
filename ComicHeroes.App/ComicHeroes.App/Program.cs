using ComicHeroes.App.Helpers;
using ComicHeroes.App.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicHeroes.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Heroes Table Storage sample!");

            Console.WriteLine("Let's connect to our table");

            string connectionString = AppSettings.LoadAppSettings().ConnectionString;

            CloudTableClient tableClient = TableHelpers.ConnectToClient(connectionString);

            CloudTable cloudTable = tableClient.GetTableReference("HeroesDB");

            List<HeroEntity> heroes = GenerateHeros();

            try
            {
                foreach (var hero in heroes)
                {
                    await OperationsHelper.InsertOrMergeHero(cloudTable, hero);
                }

                Console.WriteLine("Let's see if we can find Superman! Performing a read.");

                var superman = await OperationsHelper.GetHero(cloudTable, "Superman", "Clark Kent");

                Console.WriteLine("Wait, his hometown isn't right! Let's update it.");

                superman.Hometown = "Kryptonopolis";

                await OperationsHelper.InsertOrMergeHero(cloudTable, superman);

                Console.WriteLine("Let's 'snap' Tony Stark out of the Universe (too soon?).");

                HeroEntity tonyStark = await OperationsHelper.GetHero(cloudTable, "Iron Man", "Tony Stark");

                await OperationsHelper.DeleteHero(cloudTable, tonyStark);

                Console.WriteLine("Love you 3000");

                Console.WriteLine("Sample over. Press any key to exit....");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. Exception thrown: {ex.Message}");
                Console.ReadLine();
                throw;
            }
        }

        public static List<HeroEntity> GenerateHeros()
        {
            return new List<HeroEntity>()
            {
                new HeroEntity("Iron Man", "Tony Stark") { ComicBookUniverse = "Marvel", YearFirstAppeared = 1963, Hometown = "New York", TeamAffiliation = "Avengers" },
                new HeroEntity("Superman", "Clark Kent") { ComicBookUniverse = "DC", YearFirstAppeared = 1938, Hometown = "Smallville", TeamAffiliation = "Justice League" },
                new HeroEntity("Thor", "Thor Odinson") { ComicBookUniverse = "Marvel", YearFirstAppeared = 1962, Hometown = "Asgard", TeamAffiliation = "Avengers" },
                new HeroEntity("Captain America", "Steve Rodgers") { ComicBookUniverse = "Marvel", YearFirstAppeared = 1941, Hometown = "New York", TeamAffiliation = "Avengers" },
                new HeroEntity("Wonder Woman", "Diana Prince") { ComicBookUniverse = "DC", YearFirstAppeared = 1941, Hometown = "Themyscira", TeamAffiliation = "Justice League" },
                new HeroEntity("Batman", "Bruce Wayne") { ComicBookUniverse = "DC", YearFirstAppeared = 1939, Hometown = "Gotham City", TeamAffiliation = "Justice League" }
            };
        }
    }
}
