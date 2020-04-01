using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicHeroes.App.Helpers
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("Settings.json")
                .Build();

            AppSettings appSettings = configurationRoot.Get<AppSettings>();

            return appSettings;
        }
    }
}
