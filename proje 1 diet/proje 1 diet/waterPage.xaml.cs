using Firebase.Database;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class waterPage : ContentPage
    {
        PersonRepository personRepository=new PersonRepository();
        FirebaseClient firebase = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");
        static string mail;
        static Person person;
        public  waterPage()
        {
            InitializeComponent();
            Budgets = GetBudgets();
            this.BindingContext = this;
            mail = Preferences.Get("person", "null");
            
        }
        
        protected async override void OnAppearing()
        {
            person = await PersonGet();
            if (person.Water == null) {person.Water = new string[31];
                for (int i = 0; i < person.Water.Length; i++)
                {
                    person.Water[i] = "0";
                }
            }
            waterHistory=person.Water;
            currentTime = DateTime.Now.Day;
            amount = Convert.ToDouble(waterHistory[currentTime-1]);
            
        }
        
        public async Task<Person> PersonGet()
        {
           return (await firebase.Child(nameof(Person)).OnceAsync<Person>())
                .Where(x => x.Object.Mail == mail).Select(item => new Person
                {
                    Id = item.Object.Id,
                    Name=item.Object.Name,
                    SurName=item.Object.SurName,
                    Mail=item.Object.Mail,
                    Password = item.Object.Password,
                    BirthTime=item.Object.BirthTime,
                    Height = item.Object.Height,
                    Weight = item.Object.Weight,
                    BloodType = item.Object.BloodType,
                    Calories = item.Object.Calories,
                    Protein = item.Object.Protein,
                    Fat = item.Object.Fat,
                    Water = item.Object.Water,
                    Excersize = item.Object.Excersize,
                    Diet = item.Object.Diet,
                    Img = item.Object.Img,
                    ProfileImg = item.Object.ProfileImg,
                    Job = item.Object.Job,
                    Adress = item.Object.Adress,
                }).ToList()[0];
        }

        private ObservableCollection<Budget> budgets;
        public ObservableCollection<Budget> Budgets
        {
            get { return budgets; }
            set { 
                budgets = value;
                OnPropertyChanged();
            }
        }

        static string[] waterHistory;
        static int currentTime;
        private double amount;
        public static float deger = 0;
        public static float gecici = 0;
        public double SelectedAmount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Budget> GetBudgets()
        {
            return new ObservableCollection<Budget>
            {
                new Budget {Name="1 glass water",Amount=200,Color=Color.Blue,Image="waterglass.png"},
                new Budget {Name="0.5 lt bottle water",Amount=500,Color=Color.AliceBlue,Image="waterbottle.png"},
                new Budget {Name="1 lt bottle water",Amount=1000,Color=Color.AliceBlue,Image="waterbottle.png"},
            };
        }


        private void ItemTapped(object sender, EventArgs e)
        {
            
            var grid = sender as Grid;
            var selectedItem = grid.BindingContext as Budget;
            var parent = grid.Parent as StackLayout;

            ((parent.Parent) as ScrollView).ScrollToAsync(grid, ScrollToPosition.MakeVisible, true);

            foreach(var item in parent.Children)
            {
                
                var bg = item.FindByName<BoxView>("MainBg");
                var details = item.FindByName<StackLayout>("DetailsView");
                if (bg.IsVisible==true && details.IsVisible==true) { 
                details.TranslateTo(-40, 0, 200, Easing.SinInOut);
                bg.IsVisible = false;
                details.IsVisible = false;
                }
            }

            var selectionBg = grid.FindByName<BoxView>("MainBg");
            var selectionDetails = grid.FindByName<StackLayout>("DetailsView");
            if (selectionDetails.IsVisible == false && selectionBg.IsVisible == false)
            {
                selectionBg.IsVisible = true;
                selectionDetails.IsVisible = true;
                selectionDetails.TranslateTo(0, 0, 300, Easing.SinInOut);
            }
           
            gecici= selectedItem.Amount;
            AnimatedText();
            
            

        }
        private void AnimatedText()
        {
            
           basıldımı = true;
        }

        static bool basıldımı = false;




      

       /* private void Button_ClickedForGlass(object sender, EventArgs e)
        {
            whicWater = 1;
        }

        private void Button_ClickedForBottle05(object sender, EventArgs e)
        {
            whicWater = 2;
        }

        private void Button_ClickedForBottle1(object sender, EventArgs e)
        {
            whicWater = 3;
        }
        private void artir(double sayı)
        {
            for (double i = 0; i < sayı; i += 0.0001)
            {
                 //sumatarası.Margin = new Thickness(95, 345 - i, 10, 55);

            }
           

        }*/

        private async void Button_ClickedSave(object sender, EventArgs e)
        {
            /*
            if (whicWater == 1) { drinkWater += 200; }
            else if (whicWater == 2) { drinkWater += 500; }
            else if (whicWater == 3) { drinkWater += 1000; }
            else if (whicWater == 0) { DisplayAlert("opss...", "Please select the amount you drink before save", "ok"); }
            else { DisplayAlert("opss", "someting isnt going well","ok"); }
            */

            deger += gecici;
            if (basıldımı)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Device.StartTimer(TimeSpan.FromSeconds(1 / 100f), () =>
                {
                    double t = stopwatch.Elapsed.TotalMilliseconds % 500 / 500;

                    SelectedAmount = Math.Min((float)deger, (float)(10 * t) + SelectedAmount);
                    double sayı = SelectedAmount * 0.1;
                    if (sayı >= 200) { sayı = 200; }
                    sumatarası.Margin = new Thickness(120, 320 - sayı, 120, 65);
                    if (SelectedAmount >= (float)deger)
                    {
                        stopwatch.Stop();
                        basıldımı = false;
                        return false;

                    }
                    basıldımı = false;
                    return true;
                });

                waterHistory[currentTime-1] = Convert.ToString(deger);
                person.Water = waterHistory;
                bool isUpdate = await personRepository.Update(person);
                if(!isUpdate)
                {
                    await DisplayAlert("hata", "güncellenemedi", "ok");
                }
                


            }
            //

            basıldımı = false;
            
            
        }

        

       
    }
    public class Budget
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public float Amount { get; set; }
        public Color Color { get; set; }
    }
}