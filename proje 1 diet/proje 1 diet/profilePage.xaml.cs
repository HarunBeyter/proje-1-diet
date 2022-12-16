using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class profilePage : ContentPage
    {
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        public profilePage()
        {
            InitializeComponent();
            mail = Preferences.Get("person", "null");
        }
        protected async override void OnAppearing()
        {
            person = await PersonGet();
            name.Text = person.Name + " " + person.SurName;
            job.Text = "(" + person.Job + ")";
            adress.Text =person.Adress;
            height.Text = person.Height;
            weight.Text = person.Weight;
            bloodtype.Text = person.BloodType;
            goal.Text = person.Goal;
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

        private async void saveform_Clicked(object sender, EventArgs e)
        {
            person.Adress=AdressInfo.Text;
            person.Job=jobInfo.Text;
            person.BloodType = BloodInfo.Text;
            person.Weight = weightInfo.Text;
            person.Height=heightInfo.Text;
            person.Goal=goalInfo.Text;
            if (person.Adress == null) { await DisplayAlert("warning", "please enter your adress", "ok"); return; }
            if (person.Job == null) { await DisplayAlert("warning", "please enter your job", "ok"); return; }
            if (person.BloodType == null) { await DisplayAlert("warning", "please enter your blood type", "ok"); return; }
            if (person.Weight == null) { await DisplayAlert("warning", "please enter your weight", "ok"); return; }
            if (person.Height == null) { await DisplayAlert("warning", "please enter your height", "ok"); return; }
            if (person.Goal == null) { await DisplayAlert("warning", "please enter your goal", "ok"); return; }
            bool isUpdate = await personRepository.Update(person);
            if (!isUpdate)
            {
                await DisplayAlert("hata", "güncellenemedi", "ok");
            }
            else { await DisplayAlert("hata", "Infos has saved", "ok"); }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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
    }
}