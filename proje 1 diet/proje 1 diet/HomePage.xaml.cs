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

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        static int currentTime;
        static int waterınfo;
        static int time;
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
            currentTime = DateTime.Now.Day;
            time = Convert.ToInt32(person.timeInfo);
            waterınfo = Convert.ToInt32(person.Water[currentTime - 1]);
            breakfast.Text = person.Calories[currentTime - 1];
            breakfastList = person.Breakfast;
            lunchList = person.lunch;
            dinnerList = person.dinner;
            snackList = person.snack;
            if (currentTime != time) { person.timeInfo = Convert.ToString(currentTime); breakfastList.Clear(); lunchList.Clear(); dinnerList.Clear(); snackList.Clear(); }
            BreakfastListInfo.ItemsSource = breakfastList;
            LunchListInfo.ItemsSource = lunchList;
            DinnerListInfo.ItemsSource = dinnerList;
            SnackListInfo.ItemsSource = snackList;
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
               new ChartEntry((float)(20))
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
        public static int sum;
       

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
                sum = Convert.ToInt32(sumInfo.Text);
                meal = mealInfo.Text;
                //mealInfo.Text = meal;
                //sumInfo.Text = Convert.ToString(sum);
                var result = await ApiCaller.Get("https://api.calorieninjas.com/v1/nutrition");

                mealList = JsonConvert.DeserializeObject<Root>(result.Response);


                meal brek = new proje_1_diet.meal();
                brek.name = mealList.items[0].name;
                brek.fat = mealList.items[0].fat_total_g;
                brek.protein = mealList.items[0].protein_g;
                brek.calories = mealList.items[0].calories;
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
                await DisplayAlert("warning", "please write meal in entry", "ok");
            }


        }

        public List<meal> change(List<meal> myList,meal brek)
        {
            if (myList == null) { myList = new List<meal> { }; }
            myList.Add(brek);

            for (int i = 0; i < myList.Count; i++)
            {
                allCalories += Convert.ToDouble(mealList.items[0].calories);
            }
            person.Calories[currentTime - 1] = Convert.ToString(allCalories);
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
            isLunch = false;
            isDinner= false;
            isSnack = false;
            if(isBreakfast) { breakfast_IsClicked.TextColor = Color.DeepPink; isBreakfast = false; }
            else { breakfast_IsClicked.TextColor = Color.Aqua; isBreakfast = true; }
        }
        private void lunch_Tapped(object sender, EventArgs e)
        {
            isBreakfast = false;
            if (isLunch) { lunch_IsClicked.TextColor = Color.DeepPink; isLunch = false; }
            else { lunch_IsClicked.TextColor = Color.Aqua; isLunch = true; }
            isDinner = false;
            isSnack = false;
        }
        private void dinner_Tapped(object sender, EventArgs e)
        {
            isBreakfast = false;
            isLunch = false;
            if(isDinner) { dinner_IsClicked.TextColor = Color.DeepPink; isDinner = false; }
            else { dinner_IsClicked.TextColor = Color.Aqua; isDinner = true; }
            isSnack = false;
        }
        private void snack_Tapped(object sender, EventArgs e)
        {
            isBreakfast = false;
            isLunch = false;
            isDinner = false;
            if (isSnack) { snack_IsClicked.TextColor = Color.DeepPink; isSnack = false; }
            else { snack_IsClicked.TextColor = Color.Aqua; isSnack = true; }
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