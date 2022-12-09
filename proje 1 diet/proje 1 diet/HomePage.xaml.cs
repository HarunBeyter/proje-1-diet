﻿using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts.Forms;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using proje_1_diet.Models;
using System.Collections.ObjectModel;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly ChartEntry[] entries = new[]
        {

             new ChartEntry(248)
            {

                Color=SKColor.Parse("#77d065")
            },
              new ChartEntry(128)
            {

                Color=SKColor.Parse("#3498db")
            },
               new ChartEntry((float)(allCalories))
            {
                Color=SKColor.Parse("#F20C0C"),

            }
        };
        public class ApiCaller
        {
            public static async Task<ApiResponse> Get(string u)
            {
                var client = new HttpClient();

                var builder = new UriBuilder(u);
                builder.Query = "query=" + meal;

                var url = builder.ToString();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", "zaqI/TxfwhL8y+CSx0vekA==bQ07d4PFPUE73D06");
                var res = await client.GetAsync(url);
                return new ApiResponse { Response = await res.Content.ReadAsStringAsync() };

            }
        }
        Root mealList;
        public static string meal;
        public static int sum;
        public HomePage()
        {
            InitializeComponent();
            //önemli api bağlantısı

            
            
            chartViewRadialGauge.Chart = new RadialGaugeChart
            {
                Entries = entries,
                LineSize = -10,
                LineAreaAlpha = 52,
                StartAngle = -90,
                LabelTextSize = 30,
                IsAnimated = true
            };
          
        }

        private void AddMealFonk(object sender, EventArgs e)
        {

            if (miniWindow.IsVisible == false)
            {
                miniWindow.IsVisible = true;
            }
            else
            {
                miniWindow.IsVisible = false;
            }

        }
        public static ObservableCollection<Item> myList;
        public static double allCalories;
        public static string totalcal;
        private async void saveMeal(object sender, EventArgs e)
        {
            sum = Convert.ToInt32(sumInfo.Text);
            meal = mealInfo.Text;
            //mealInfo.Text = meal;
            //sumInfo.Text = Convert.ToString(sum);
            var result = await ApiCaller.Get("https://api.calorieninjas.com/v1/nutrition");

            mealList = JsonConvert.DeserializeObject<Root>(result.Response);
            

            if (myList == null) { myList = new ObservableCollection<Item> { }; }
            myList.Add(mealList.items[0]);
            BreakfastList.ItemsSource = myList;
           
            for(int i = 0; i < myList.Count; i++)
            {
                allCalories += Convert.ToDouble(myList[i].calories);
            }
            totalcal= allCalories.ToString();
            yemek.Text=totalcal;

        }




        private void mealInfo_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        public class ApiResponse
        {
            public string Response { get; set; }
        }
        public class Item
        {
            public string sugar_g { get; set; }
            public string fiber_g { get; set; }
            public string serving_size_g { get; set; }
            public string sodium_mg { get; set; }
            public string name { get; set; }
            public string potassium_mg { get; set; }
            public string fat_saturated_g { get; set; }
            public string fat_total_g { get; set; }
            public string calories { get; set; }
            public string cholesterol_mg { get; set; }
            public string protein_g { get; set; }
            public string carbohydrates_total_g { get; set; }
        }

        public class Root
        {
            public List<Item> items { get; set; }
        }
    }
}