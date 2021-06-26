using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebWal.Interface;

namespace WebWal.Helpers
{
    public class ConvertHelpers: IConvertHelpers
    {
        public class RootObject
        {
            public string code { get; set; }
            public string number { get; set; }
            public string name { get; set; }
        }
        public  string ReturnWebRateData()
        {
            var json = new WebClient().DownloadString("http://data.fixer.io/api/latest?access_key=0dbbc6b1ed7d7b57392d9c3e5ce269c9&format=1");
            return json;
        }

        // Creates a Dictionary of all Supported Currencies
        public  Dictionary<string, Currency> GetCurrencies()
        {
            Dictionary<string, Currency> _currencies = new Dictionary<string, Currency>();
            string path = "Currency.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                dynamic currencyParse = JsonConvert.DeserializeObject<List<RootObject>>(json);
                foreach (var item in currencyParse)
                {
                    _currencies.Add(item.code, new Currency(item.code, Int32.Parse(item.number), item.name));
                }
                return _currencies;
            }
        }
        public  decimal ParseData(Currency Currency)
        {
            try
            {
                //Scrapes Web Data to get the rates.
                string jsonOriginal = ReturnWebRateData();
                //Removes the headers
                string[] splitInformation = jsonOriginal.Split('{', '}');
                //Our current Data can then be split into "Name : Rate"
                string[] rows = splitInformation[2].Split(',');
                foreach (var row in rows)
                {
                    //Gets rid of spaces and new line characters
                    string line = row.Trim();
                    //Gets the currency code name
                    string name = line.Substring(1, 3);
                    if (name == Currency.CurrencyCode)
                    {
                        //and extracts the amount
                        var amount = Convert.ToDecimal(line.Substring(6));
                        return amount;
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
    }
}
