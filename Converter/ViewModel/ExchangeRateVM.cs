using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Converter.Model;
using Newtonsoft.Json;

namespace Converter.ViewModel
{
    public class ExchangeRateVM
    {
        private Dictionary<string,Valute> valutes;
        private List<string> test;
        void GetExchange()
        {
            var requestUri = "https://www.cbr-xml-daily.ru/daily_json.js";

            string json = String.Empty;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(requestUri).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }

            var deserialized = JsonConvert.DeserializeObject<SBR>(json);

            valutes = deserialized.Valute;
            
        }
       public List<string> Test
        {
            get { return new List<string>() { "123", "12424" }; }
            set { test = value; }            
        }



        public ExchangeRateVM()
        {
            GetExchange();
        }


    }
}
