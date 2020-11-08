using reto.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace reto.Helpers
{
    static class service_helper
    {
        public static Dictionary<int, Source> getSources()
        {
            return new Dictionary<int, Source>()
            {
                { 1,new Source { url="https://www.googleapis.com/customsearch/v1?key=AIzaSyC4MsXaEujxqi1ZUksYBvBNETFP0xhQflM&cx=017576662512468239146:omuauf_lfve&q=", source="google"} },
                { 2,new Source { url="https://www.bing.com/search?q=", source="bing"} },
                { 3,new Source { url="https://us.search.yahoo.com/search?fr=yhs-invalid&p=", source="yahoo"} }
            };
        }

        public static async Task<List<Result>> GetData(string[] args, Dictionary<int, Source> siteUrls)
        {
            HttpClient httpClient = new HttpClient();
            List<Result> resultList = new List<Result>();

            foreach (string arg in args)
            {
                Result result = new Result { name = arg, total = 0 };
                foreach (KeyValuePair<int, Source> siteUrl in siteUrls)
                {
                    HttpResponseMessage request = await httpClient.GetAsync(siteUrl.Value.url + arg);
                    string responseString = await request.Content.ReadAsStringAsync();

                    switch (siteUrl.Value.source)
                    {
                        case "google":
                            string resultG = format_helper.getNumberGoogle(responseString);
                            result.google = Int64.Parse(resultG);
                            result.total += Int64.Parse(resultG);
                            break;
                        case "bing":
                            string resultB = format_helper.getNumberBing(responseString);
                            result.bing = Int64.Parse(resultB);
                            result.total += Int64.Parse(resultB);
                            break;
                        case "yahoo":
                            string resultY = format_helper.getNumberYahoo(responseString);
                            result.yahoo = Int64.Parse(resultY);
                            result.total += Int64.Parse(resultY);
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }

                }
                resultList.Add(result);
            }
            return resultList;
        }        
    }
}
