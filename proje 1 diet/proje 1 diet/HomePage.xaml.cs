using Microcharts;
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
using Firebase.Database;
using System.Collections;
using System.Runtime.CompilerServices;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://diet-data-23870-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        static DateTime currentTime;
        static int waterınfo;
        static int time;
        static int ay;
        static bool isBreakfast=false;
        static bool isLunch;
        static bool isDinner;
        static bool isSnack;
       


        public HomePage()
        {
            InitializeComponent();
            //önemli api bağlantısı

            mail = Preferences.Get("person", "null");

            chartViewRadialGauge.Chart = new RadialGaugeChart
            {
                Entries = chartentry(),
                MaxValue = 10000,
                LineSize = -10,
                LineAreaAlpha = 52,
                StartAngle = -90,
                LabelTextSize = 30,
                IsAnimated = true
            };

        }
        protected async override void OnAppearing()
        {
            person = await PersonGet();
            currentTime = DateTime.Now;
            time = Convert.ToInt32(person.timeInfo);
            ay= Convert.ToInt32(person.monthInfo);
            waterınfo = Convert.ToInt32(person.Water[currentTime.Day - 1]);
            breakfast.Text = (person.Calories[currentTime.Day - 1]);
            breakfastList = person.Breakfast;
            lunchList = person.lunch;
            dinnerList = person.dinner;
            snackList = person.snack;
            if (breakfastList == null) { breakfastList = new List<meal>(); }
            if(lunchList == null) { lunchList = new List<meal>(); } 
            if(snackList == null) { snackList = new List<meal>(); }
            if(dinnerList == null) { dinnerList = new List<meal>(); }
            if (currentTime.Day != time) {
                person.timeInfo = Convert.ToString(currentTime.Day);
                breakfastList.Clear(); 
                lunchList.Clear(); 
                dinnerList.Clear(); 
                snackList.Clear(); }
            if (currentTime.Month != ay)
            {
                string[] reset = new string[31];
                for (int i = 0; i < reset.Length; i++)
                {
                    reset[i] = "0";
                }
                person.monthInfo = Convert.ToString(currentTime.Month);
                person.Calories = reset;
                person.Water = reset;
                person.Excersize = reset;
            }
            BreakfastListInfo.ItemsSource = breakfastList;
            LunchListInfo.ItemsSource = lunchList;
            DinnerListInfo.ItemsSource = dinnerList;
            SnackListInfo.ItemsSource = snackList;
            bool isUpdate = await personRepository.Update(person);
            if (!isUpdate)
            {
                await DisplayAlert("hata", "güncellenemedi", "ok");
            }
            chartViewRadialGauge.Chart = new RadialGaugeChart
            {
                Entries = chartentry(),
                MaxValue = 10000,
                LineSize = -10,
                LineAreaAlpha = 52,
                StartAngle = -90,
                LabelTextSize = 30,
                IsAnimated = true
            };
           ChartEntry[] chartentry()
            {
                ChartEntry[] entries = new[]
            {

             new ChartEntry(248)
            {

                Color=SKColor.Parse("#77d065")
            },
              new ChartEntry((float)(20))
            {

                Color=SKColor.Parse("#3498db")
            },
               new ChartEntry((float)(allCalories))
            {
                Color=SKColor.Parse("#F20C0C"),

            } };
                return entries;
            }

        }
        public async Task<Person> PersonGet()
        {
            return (await firebase.Child(nameof(Person)).OnceAsync<Person>())
                 .Where(x => x.Object.Mail == mail).Select(item => new Person
                 {
                     Id = item.Object.Id,
                     Name = item.Object.Name,
                     SurName = item.Object.SurName,
                     Mail = item.Object.Mail,
                     Password = item.Object.Password,
                     BirthTime = item.Object.BirthTime,
                     Height = item.Object.Height,
                     Weight = item.Object.Weight,
                     BloodType = item.Object.BloodType,
                     Calories = item.Object.Calories,
                     Breakfast = item.Object.Breakfast,
                     lunch=item.Object.lunch,
                     dinner=item.Object.dinner,
                     snack=item.Object.snack,
                     Water = item.Object.Water,
                     Excersize = item.Object.Excersize,
                     Diet = item.Object.Diet,
                     Img = item.Object.Img,
                     ProfileImg = item.Object.ProfileImg,
                     Job = item.Object.Job,
                     Adress = item.Object.Adress,
                     timeInfo = item.Object.timeInfo,
                     Goal=item.Object.Goal,
                     monthInfo = item.Object.monthInfo,
                 }).ToList()[0];
        }
      
        public ChartEntry[] chartentry ()
        {
            ChartEntry[] entries = new[]
        {

             new ChartEntry(248)
            {

                Color=SKColor.Parse("#77d065")
            },
              new ChartEntry((float)(20))
            {

                Color=SKColor.Parse("#3498db")
            },
               new ChartEntry((float)(allCalories))
            {
                Color=SKColor.Parse("#F20C0C"),
                
            } };
            return entries;
        }

      /*  private readonly ChartEntry[] entries = new[]
        {

             new ChartEntry(248)
            {

                Color=SKColor.Parse("#77d065")
            },
              new ChartEntry((float)(waterınfo))
            {

                Color=SKColor.Parse("#3498db")
            },
               new ChartEntry((float)(allCalories))
            {
                Color=SKColor.Parse("#F20C0C"),

            }
        };*/
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
        public static double sum;
       

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
        public static List<meal> breakfastList;
        public static List<meal> lunchList;
        public static List<meal> dinnerList;
        public static List<meal> snackList;
        public static double allCalories;
        private async void saveMeal(object sender, EventArgs e)
        {
            try
            {
                sum = Convert.ToDouble(sumInfo.Text);
                meal = mealInfo.Text;
                //mealInfo.Text = meal;
                //sumInfo.Text = Convert.ToString(sum);
                var result = await ApiCaller.Get("https://api.calorieninjas.com/v1/nutrition");

                mealList = JsonConvert.DeserializeObject<Root>(result.Response);
                double oran = sum / 100;
                double fat = Convert.ToDouble(mealList.items[0].fat_total_g)/10*oran;
                double protein = Convert.ToDouble(mealList.items[0].protein_g) / 10 * oran;
                double calories = Convert.ToDouble(mealList.items[0].calories) / 10 * oran;
                meal brek = new proje_1_diet.meal();
                brek.name = mealList.items[0].name;
                brek.fat = fat.ToString();
                brek.protein = protein.ToString();
                brek.calories = calories.ToString();
                if (isBreakfast == true) { breakfastList = change(breakfastList, brek); isBreakfast = false; person.Breakfast = breakfastList; }
                else if (isLunch == true) { lunchList = change(lunchList, brek); isLunch = false; person.lunch = lunchList; }
                else if (isDinner == true) { dinnerList = change(dinnerList, brek); isDinner = false; person.dinner = dinnerList; }
                else if (isSnack == true) { snackList = change(snackList, brek); isSnack = false; person.snack = snackList; }
                else { await DisplayAlert("warning", "please select your meal type", "ok"); }
                bool isUpdate = await personRepository.Update(person);
                if (!isUpdate)
                {
                    await DisplayAlert("hata", "güncellenemedi", "ok");
                }
                else {
                    saveform.Background = Color.Green;
                    saveform.TextColor = Color.White;
                }

            }
            catch
            {
                await DisplayAlert("warning", "someting wrong", "ok");
            }


        }

        public List<meal> change(List<meal> myList,meal brek)
        {
            allCalories = Convert.ToDouble(person.Calories[currentTime.Day - 1]);
            if (myList == null) { myList = new List<meal> { }; }
            myList.Add(brek);
            double oran = sum / 100;
            allCalories += Convert.ToDouble(mealList.items[0].calories)/10*oran;
            person.Calories[currentTime.Day - 1] = Convert.ToString(allCalories);
            return myList;
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

        private void breakfast_Tapped(object sender, EventArgs e)
        {
            if(isBreakfast) { breakfast_IsClicked.TextColor = Color.DeepPink; isBreakfast = false; }
            else 
            {
                breakfast_IsClicked.TextColor = Color.Aqua; isBreakfast = true;
                lunch_IsClicked.TextColor = Color.DeepPink; isLunch = false;
                dinner_IsClicked.TextColor = Color.DeepPink; isDinner = false;
                snack_IsClicked.TextColor = Color.DeepPink; isSnack = false;
            }
        }
        private void lunch_Tapped(object sender, EventArgs e)
        {
            if (isLunch) { lunch_IsClicked.TextColor = Color.DeepPink; isLunch = false; }
            else 
            {
                breakfast_IsClicked.TextColor = Color.DeepPink; isBreakfast = false;
                lunch_IsClicked.TextColor = Color.Aqua; isLunch = true;
                dinner_IsClicked.TextColor = Color.DeepPink; isDinner = false;
                snack_IsClicked.TextColor = Color.DeepPink; isSnack = false;
            }
        }
        private void dinner_Tapped(object sender, EventArgs e)
        {
            if(isDinner) { dinner_IsClicked.TextColor = Color.DeepPink; isDinner = false; }
            else 
            {
                breakfast_IsClicked.TextColor = Color.DeepPink; isBreakfast = false;
                lunch_IsClicked.TextColor = Color.DeepPink; isLunch = false;
                dinner_IsClicked.TextColor = Color.Aqua; isDinner = true;
                snack_IsClicked.TextColor = Color.DeepPink; isSnack = false;
            }
        }
        private void snack_Tapped(object sender, EventArgs e)
        {
            if (isSnack) { snack_IsClicked.TextColor = Color.DeepPink; isSnack = false; }
            else 
            {
                breakfast_IsClicked.TextColor = Color.DeepPink; isBreakfast = false;
                lunch_IsClicked.TextColor = Color.DeepPink; isLunch = false;
                dinner_IsClicked.TextColor = Color.DeepPink; isDinner = false;
                snack_IsClicked.TextColor = Color.Aqua; isSnack = true;
            }
        }
        private  void refresh_list(object sender, EventArgs e)
        {
            try
            {
                BreakfastListInfo.ItemsSource = null;
                breakfastList = (person.Breakfast);
                BreakfastListInfo.ItemsSource = breakfastList;
                BreakfastListInfo.EndRefresh();
            }
            catch (FirebaseException ex)
            {
                 DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }

        }
    }
}