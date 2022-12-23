using Firebase.Database;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class excersizePage : ContentPage
    {
        public excersizePage()
        {
            InitializeComponent();
            mail = Preferences.Get("person", "null");

        }
        PersonRepository personRepository = new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        static int currentTime;
        static DateTime day;
        static double allExersize=0;

        static bool ısWalk = false;
        static bool ısRun = false;
        static bool ısSwim = false;
        static bool ısSoccer = false;
        static bool ısBoxing = false;
        static bool ısRope = false;
        static bool ısCyclist = false;
        static bool ısDance = false;
        static double clock;

        static double walkCal = 4;
        static double runCal = 15;
        static double swimCal = 12;
        static double soccerCal = 8;
        static double boxingCal = 10.83;
        static double ropekCal = 13.33;
        static double cyclistCal = 11.66;
        static double danceCal = 4.16;
        protected async override void OnAppearing()
        {

            person = await PersonGet();
            currentTime = DateTime.Now.Day;
            allExersize =Convert.ToDouble(person.Excersize[currentTime - 1]);
            day = DateTime.Now;
            
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
                     snack = item.Object.snack,
                     Water = item.Object.Water,
                     Excersize = item.Object.Excersize,
                     Diet = item.Object.Diet,
                     Img = item.Object.Img,
                     ProfileImg = item.Object.ProfileImg,
                     Job = item.Object.Job,
                     Adress = item.Object.Adress,
                     timeInfo = item.Object.timeInfo,
                     Goal = item.Object.Goal,
                 }).ToList()[0];
        }

       
        private void Tapwalk_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısWalk) { walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;  }
            else 
            { 
                walkingframe.BackgroundColor = Color.LightGreen; walkingframe.Margin = new Thickness(0,-10,0,10); ısWalk = true;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
            
        }

        private void Tapruning_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısRun) { running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false; }
            else 
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.LightGreen; running.Margin = new Thickness(0, -10, 0, 10); ısRun = true;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Tapswim_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısSwim) { swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.LightGreen; swim.Margin = new Thickness(0, -10, 0, 10); ısSwim = true;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Tapsoccer_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısSoccer) { soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.LightGreen; soccer.Margin = new Thickness(0, -10, 0, 10); ısSoccer = true;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Tapboxing_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısBoxing) { boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.LightGreen; boxing.Margin = new Thickness(0, -10, 0, 10); ısBoxing = true;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Taprope_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısRope) { rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.LightGreen; rope.Margin = new Thickness(0, -10, 0, 10); ısRope = true;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Tapcyclist_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısCyclist) { cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.LightGreen; cyclist.Margin = new Thickness(0, -10, 0, 10); ısCyclist = true;
                dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false;
            }
        }

        private void Tapdance_Tapped(object sender, EventArgs e)
        {
            animation.Source = "ball.gif";
            if (ısDance) { dance.BackgroundColor = Color.White; dance.Margin = new Thickness(0, 0, 0, 0); ısDance = false; }
            else
            {
                walkingframe.BackgroundColor = Color.White; walkingframe.Margin = new Thickness(0, 0, 0, 0); ısWalk = false;
                running.BackgroundColor = Color.White; running.Margin = new Thickness(0, 0, 0, 0); ısRun = false;
                swim.BackgroundColor = Color.White; swim.Margin = new Thickness(0, 0, 0, 0); ısSwim = false;
                soccer.BackgroundColor = Color.White; soccer.Margin = new Thickness(0, 0, 0, 0); ısSoccer = false;
                boxing.BackgroundColor = Color.White; boxing.Margin = new Thickness(0, 0, 0, 0); ısBoxing = false;
                rope.BackgroundColor = Color.White; rope.Margin = new Thickness(0, 0, 0, 0); ısRope = false;
                cyclist.BackgroundColor = Color.White; cyclist.Margin = new Thickness(0, 0, 0, 0); ısCyclist = false;
                dance.BackgroundColor = Color.LightGreen; dance.Margin = new Thickness(0, -10, 0, 10); ısDance = true;
            }
        }
        
        private async void buton_Clicked(object sender, EventArgs e)
        {
            clock = Convert.ToDouble(time.Text);
            if (ısWalk) { allExersize += (clock * walkCal); }
            else if (ısRun) { allExersize += (clock * runCal); }
            else if (ısSwim) { allExersize += (clock * swimCal); }
            else if (ısSoccer) { allExersize += (clock * soccerCal); }
            else if (ısBoxing) { allExersize += (clock * boxingCal); }
            else if (ısRope) { allExersize += (clock * ropekCal); }
            else if (ısCyclist) { allExersize += (clock * cyclistCal); }
            else if (ısDance) { allExersize += (clock * danceCal); }
            else {await DisplayAlert("warning", "please select a excersize", "ok"); return; }
            person.Excersize[currentTime - 1] = allExersize.ToString();
            bool isUpdate = await personRepository.Update(person);
            if (!isUpdate)
            {
                await DisplayAlert("hata", "güncellenemedi", "ok");
            }
            else await DisplayAlert("Info", "Excersize has been saved", "ok");
        }
    }
}