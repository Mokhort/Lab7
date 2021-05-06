using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lab7
{
    public class Address {
        //"house_number":"71","road":"Via Guglielmo Marconi",
        //"suburb":"Marconi","city":"Болонья","county":"Болонья","state":"Эмилия-Романья",
        //"postcode":"40122","country":"Италия","country_code":"it"}
        public string house_number { get; set; }
        public string road { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country_code { get; set; }
        public string state_district { get; set; }
    }
    public class SearchJson
    { 
        public string lat { get; set; }
        public string lon { get; set; }
        public string type { get; set; }
        public string addresstype { get; set; }
        public string display_name { get; set; }
        public Address address{ get; set; }

    }
    class Program
    {
        static async Task Main(string[] args)
        {
            string lat;
            string lon;
            Console.WriteLine("Hello. I will get you place by coordinate");
            Console.WriteLine("Enter lat:");
            lat = Console.ReadLine();
            Console.WriteLine("Enter lon:");
            lon = Console.ReadLine();
            using (HttpClient client = new HttpClient())
                {
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                //nominatim.openstreetmap.org/reverse?format=jsonv2&lat=34.44076&lon=-58.70521
                //44.50155
                //11.33989

                //34.874324
                //76.087334
                HttpResponseMessage pointsResponse = await client.GetAsync("https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=" + lat + "&lon=" + lon );

                    if (pointsResponse.IsSuccessStatusCode)
                    {
                        SearchJson roots = await pointsResponse.Content.ReadFromJsonAsync<SearchJson>();

                        if (roots != null) { 
                        Console.WriteLine($"Type: {roots.type}");
                        Console.WriteLine($"Address type: {roots.addresstype}");
                        Console.WriteLine($"Display mame: {roots.display_name}");

                        if (roots.address != null)
                        {
                            Console.WriteLine($"House number: {roots.address.house_number}");
                            Console.WriteLine($"Road: {roots.address.road}");
                            Console.WriteLine($"Suburb: {roots.address.suburb}");
                            Console.WriteLine($"City: {roots.address.city}");
                            Console.WriteLine($"County: {roots.address.county}");
                            Console.WriteLine($"Country: {roots.address.country}");
                            Console.WriteLine($"State: {roots.address.state}");
                            Console.WriteLine($"Postcode: {roots.address.postcode}");
                            Console.WriteLine($"Country code: {roots.address.country_code}");
                            Console.WriteLine($"State district: {roots.address.state_district}");
                        }
                    }
                        else Console.WriteLine("Can't detect place");
                    }else{
                        string resp = await pointsResponse.Content.ReadAsStringAsync();
                        Console.WriteLine(resp);
                    }
                }
            }
        }
 }

