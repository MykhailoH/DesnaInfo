using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesnaInfo.Domain.Classes;
using DesnaInfo.LevelFetcher;
using HtmlAgilityPack;

namespace DesnaInfoConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var levels = await Fetcher.FetchAsync();
            foreach (var waterLevel in levels)
            {
                Console.WriteLine($"On {waterLevel.Date} the water level was {waterLevel.AbsoluteLevel}, change {waterLevel.Change}");
            }

            Console.ReadKey();
        }
    }
}
