using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace weatherForecast
{
    class Program
    {
        static void Main(string[] args)
        {
            bool check = true;
            string ulan_ude = "http://api.openweathermap.org/data/2.5/weather?q=Ulan-Ude&units=metric&appid=224bf816fb43a6d9e133ecabf07a51e3";
            string london = "http://api.openweathermap.org/data/2.5/weather?q=London&units=metric&appid=224bf816fb43a6d9e133ecabf07a51e3";
            string moscow = "http://api.openweathermap.org/data/2.5/weather?q=Moscow&units=metric&appid=224bf816fb43a6d9e133ecabf07a51e3";
            string url = "";
            string finish = "finish";

            List<string> Cities = new List<string>();
            Cities.Add("Москва-1");
            Cities.Add("Лондон-2");
            Cities.Add("Улан-Удэ-3");
            Cities.Add("Для завершения нажмите-4");

            Console.WriteLine("Температура.");
            while (check == true)
            {
                Console.WriteLine("___________________");
                Console.WriteLine("Выберите номер города, температуру которого хотите узнать:");
                foreach (var City in Cities)
                    Console.WriteLine(City);
                Console.WriteLine("Ввод:");
                int.TryParse(Console.ReadLine(),out int selection);

                switch (selection)
                {
                    case 1:
                        {
                            url = moscow;
                            break;
                        }
                    case 2:
                        {
                            url = london;
                            break;
                        }
                    case 3:
                        {
                            url = ulan_ude;
                            break;
                        }
                    case 4:
                        {
                            url = finish;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка ввода.");
                            break;
                        }
                }
                

                if (url == london || url == moscow || url == ulan_ude)
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    string response;
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                    Console.WriteLine($"temperature in {weatherResponse.Name}: {weatherResponse.Main.Temp} C.");
                }
                else if (url == finish)
                {
                    check = false;
                    Console.WriteLine("Завершение...");
                }
                else Console.WriteLine("Повторный ввод");
            }
        }
    }
}
