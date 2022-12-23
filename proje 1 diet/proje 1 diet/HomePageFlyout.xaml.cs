using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageFlyout : ContentPage
    {
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        public HomePageFlyout()
        {
            InitializeComponent();
            mail = Preferences.Get("person", "null");
        }
        protected async override void OnAppearing()
        {
            person = await PersonGet();
            name.Text = person.Name + " " + person.SurName;
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
    }
}