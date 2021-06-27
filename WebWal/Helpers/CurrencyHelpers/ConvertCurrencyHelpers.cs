using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WebWal.Helpers.Enums;
using Newtonsoft.Json.Linq;
using WebWal.Helpers.Models;

namespace WebWal.Helpers
{
    public static class ConvertCurrencyHelpers
    {
        public const string FreeBaseUrl = "https://free.currencyconverterapi.com/api/v6/";

        public static decimal ExchangeRate(string from, string to, string apiKey)
        {
            string url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + apiKey;
            var jsonString = GetResponse(url);
            return JObject.Parse(jsonString).First.First["val"].ToObject<decimal>();
        }

        private static string GetResponse(string url)
        {
            string jsonString;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
