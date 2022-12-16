using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class weeklydetails : ContentPage
    {
        static ObservableCollection<Weekly> WeeklyInfo;
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        static int currentTime;
        static DateTime day;
        public weeklydetails()
        {
            InitializeComponent();
            mail = Preferences.Get("person", "nul");
            

            this.BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            
            person = await PersonGet();
            currentTime = DateTime.Now.Day;
            day = DateTime.Now;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = day.DayOfWeek.ToString(),Value=person.Calories[day.Day-1],Icon="yemek.png",sym="cal" },
                new Weekly{ Date = day.AddDays(-1).DayOfWeek.ToString(),Value=person.Calories[day.Day-2],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-2).DayOfWeek.ToString(),Value=person.Calories[day.Day-3] ,Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-3).DayOfWeek.ToString(),Value=person.Calories[day.Day-4],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-4).DayOfWeek.ToString(),Value=person.Calories[day.Day-5],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-5).DayOfWeek.ToString(),Value=person.Calories[day.Day-6],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-6).DayOfWeek.ToString(),Value=person.Calories[day.Day-7],Icon="yemek.png" , sym = "cal"}
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;
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
                     lunch = item.Object.lunch,
                     dinner = item.Object.dinner,
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

        public static String myDate = DateTime.Now.ToString();
        /*
        TimeInfo date = new TimeInfo { Time = myDate };
        public TimeInfo Time { get => date; }
        */
        public string Time
        {
            get { return myDate; }
        }



        private void GetWeeklyWaterInfo(object sender, EventArgs e)
        {
            
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = day.DayOfWeek.ToString(),Value=person.Water[day.Day-1],Icon="waterbottle.png" ,sym="ml"},
                new Weekly{ Date = day.AddDays(-1).DayOfWeek.ToString(),Value=person.Water[day.Day-2],Icon="waterbottle.png" ,sym="ml"},
                new Weekly{ Date = day.AddDays(-2).DayOfWeek.ToString(),Value=person.Water[day.Day-3],Icon="waterbottle.png" ,sym="ml"},
                new Weekly{ Date = day.AddDays(-3).DayOfWeek.ToString(),Value=person.Water[day.Day-4],Icon="waterbottle.png" ,sym = "ml"},
                new Weekly{ Date = day.AddDays(-4).DayOfWeek.ToString(),Value=person.Water[day.Day-5],Icon="waterbottle.png" ,sym = "ml"},
                new Weekly{ Date = day.AddDays(-5).DayOfWeek.ToString(),Value=person.Water[day.Day-6],Icon="waterbottle.png" ,sym = "ml"},
                new Weekly{ Date = day.AddDays(-6).DayOfWeek.ToString(),Value=person.Water[day.Day-7],Icon="waterbottle.png" ,sym = "ml"}
            };
            WeeklyDataList.ItemsSource= WeeklyInfo; 
        }

        private void GetWeeklyMealInfo(object sender, EventArgs e)
        {
            
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = day.DayOfWeek.ToString(),Value=person.Calories[day.Day-1],Icon="yemek.png",sym="cal" },
                new Weekly{ Date = day.AddDays(-1).DayOfWeek.ToString(),Value=person.Calories[day.Day-2],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-2).DayOfWeek.ToString(),Value=person.Calories[day.Day-3] ,Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-3).DayOfWeek.ToString(),Value=person.Calories[day.Day-4],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-4).DayOfWeek.ToString(),Value=person.Calories[day.Day-5],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-5).DayOfWeek.ToString(),Value=person.Calories[day.Day-6],Icon="yemek.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-6).DayOfWeek.ToString(),Value=person.Calories[day.Day-7],Icon="yemek.png" , sym = "cal"}
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;
        }
        private void GetWeeklyExerciseInfo(object sender, EventArgs e)
        {
         
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = day.DayOfWeek.ToString(),Value=person.Excersize[day.Day-1] ,Icon="kedi.png",sym="cal" },
                new Weekly{ Date = day.AddDays(-1).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2],Icon="kedi.png",sym="cal" },
                new Weekly{ Date = day.AddDays(-2).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2] ,Icon="kedi.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-3).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2],Icon="kedi.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-4).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2],Icon="kedi.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-5).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2],Icon="kedi.png" , sym = "cal"},
                new Weekly{ Date = day.AddDays(-6).DayOfWeek.ToString(),Value=person.Excersize[day.Day-2] ,Icon="kedi.png" , sym = "cal"}
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;
        }
    }
    public class TimeInfo
    {
        public string Time { get; set; }
    }
    public class Weekly
    {
        public string Value { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }
        public string sym { get; set; } 
    }
}