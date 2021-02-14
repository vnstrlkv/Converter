
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Converter.Model
{
    public  class CBRDaily
    {
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset PreviousDate { get; set; }
        public string PreviousUrl { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Dictionary<string, Valute> Valute { get; set; }

       async public Task<CBRDaily> GetValutes()
        { 
            var requestUri = "https://www.cbr-xml-daily.ru/daily_json.js";

            string json = String.Empty;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }
            }
            var valutes = JsonConvert.DeserializeObject<CBRDaily>(json);
            valutes.Valute.Add("RUB", new Valute { Name = "Российский рубль", Value = 1, Nominal = 1, CharCode="RUB"});
            return valutes;
        }
    }

    public  class Valute
    {
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public long Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Previous { get; set; }
    }
}
