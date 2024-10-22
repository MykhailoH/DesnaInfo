using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesnaInfo.Domain.Classes;
using HtmlAgilityPack;

namespace DesnaInfo.LevelFetcher
{
    public static class Fetcher
    {

        public static IEnumerable<WaterLevel> Fetch()
        {
            string[] dates = new string[3];
            for (int i = 0; i < 3; i++)
            {
                var date = DateTime.Today.AddDays(-i);
                dates[i] = date.Day < 10 ? date.ToString("d.MM.yyyy") : date.ToString("dd.MM.yyyy");
            }
            var web = new HtmlWeb();
            var document = web.Load("https://www.rubhoz.com/river/74/136");
            var nodes = document.DocumentNode.SelectNodes("//tr").Where(x => dates.Any(y => x.InnerHtml.Contains(y)));
            List<WaterLevel> levels = new List<WaterLevel>();
            foreach (var node in nodes)
            {
                var html = node.InnerHtml.Replace("<td>", ";").Replace("</td>", "").Trim();
                var splittedHtml = html.Split(';');
                levels.Add(new WaterLevel
                {
                    Date = splittedHtml[1],
                    AbsoluteLevel = splittedHtml[2],
                    Change = splittedHtml[3]
                });

            }

            return levels;
        }

        public static async Task<IEnumerable<WaterLevel>> FetchAsync()
        {
            string[] dates = new string[3];
            for (int i = 0; i < 3; i++)
            {
                var date = DateTime.Today.AddDays(-i);
                dates[i] = date.Day < 10 ? date.ToString("d.MM.yyyy") : date.ToString("dd.MM.yyyy");
            }
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync("https://www.rubhoz.com/river/74/136");
            var nodes = document.DocumentNode.SelectNodes("//tr").Where(x => dates.Any(y => x.InnerHtml.Contains(y)));
            List<WaterLevel> levels = new List<WaterLevel>();
            foreach (var node in nodes)
            {
                var html = node.InnerHtml.Replace("<td>", ";").Replace("</td>", "").Trim();
                var splittedHtml = html.Split(';');
                levels.Add(new WaterLevel
                {
                    Date = splittedHtml[1],
                    AbsoluteLevel = splittedHtml[2],
                    Change = splittedHtml[3]
                });

            }

            return levels;
        }
    }
}
