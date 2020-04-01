using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicHeroes.App.Models
{
    public class HeroEntity : TableEntity
    {
        public HeroEntity()
        {

        }

        public HeroEntity(string heroName, string alterEgo)
        {
            PartitionKey = heroName;
            RowKey = alterEgo;
        }

        public string ComicBookUniverse { get; set; }
        public int YearFirstAppeared { get; set; }
        public string Hometown { get; set; }
        public string TeamAffiliation { get; set; }
    }
}
