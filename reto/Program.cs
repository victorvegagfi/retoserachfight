using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using reto.Entities;
using reto.Helpers;

namespace reto
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Dictionary<int, Source> siteUrls = service_helper.getSources();
            List<Result> resultList = await service_helper.GetData(args,siteUrls);

            foreach (Result res in resultList)
            {
                Console.WriteLine(res.name + ":" + "Google:" + res.google + " Bing:" + res.bing + " Yahoo:" + res.yahoo);
            }

            Result googleWinner = resultList.OrderByDescending(x => x.google).First();
            Result bingWinner = resultList.OrderByDescending(x => x.bing).First();
            Result yahooWinner = resultList.OrderByDescending(x => x.yahoo).First();
            Result totalWinner = resultList.OrderByDescending(x => x.total).First();
            Console.WriteLine("Google winner: " + googleWinner.name);
            Console.WriteLine("Bing winner: " + bingWinner.name);
            Console.WriteLine("Yahoo winner: " + yahooWinner.name);
            Console.WriteLine("Total winner: " + totalWinner.name);
        }               
    }
}
