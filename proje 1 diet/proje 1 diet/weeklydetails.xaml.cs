using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class weeklydetails : ContentPage
    {
        static ObservableCollection<Weekly> WeeklyInfo;
        public weeklydetails()
        {
            InitializeComponent();
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = "Monday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Tuesday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Wednesday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Thursday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Friday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Saturday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Sunday",Value="2000" ,Icon="yemek.png" }
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;

            this.BindingContext = this;
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
                new Weekly{ Date = "Monday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Tuesday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Wednesday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Thursday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Friday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Saturday",Value="2000" ,Icon="waterbottle.png" },
                new Weekly{ Date = "Sunday",Value="2000" ,Icon="waterbottle.png" }
            };
            WeeklyDataList.ItemsSource= WeeklyInfo; 
        }

        private void GetWeeklyMealInfo(object sender, EventArgs e)
        {
            
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = "Monday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Tuesday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Wednesday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Thursday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Friday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Saturday",Value="2000" ,Icon="yemek.png" },
                new Weekly{ Date = "Sunday",Value="2000" ,Icon="yemek.png" }
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;
        }
        private void GetWeeklyExerciseInfo(object sender, EventArgs e)
        {
         
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = "Monday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Tuesday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Wednesday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Thursday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Friday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Saturday",Value="2000" ,Icon="kedi.png" },
                new Weekly{ Date = "Sunday",Value="2000" ,Icon="kedi.png" }
            };
            WeeklyDataList.ItemsSource = WeeklyInfo;
        }
        private void GetWeeklySuccessInfo(object sender, EventArgs e)
        {
            WeeklyInfo = null;
            WeeklyInfo = new ObservableCollection<Weekly>
            {
                new Weekly{ Date = "Monday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Tuesday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Wednesday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Thursday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Friday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Saturday",Value="2000" ,Icon="congratulate.png" },
                new Weekly{ Date = "Sunday",Value="2000" ,Icon="congratulate.png" }
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
    }
}