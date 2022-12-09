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
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            flyout.FlyList.ItemSelected += OnSelectedItem;
        }
        public MainPage(string person)
        {
            Preferences.Set("person", person);
            InitializeComponent();
            flyout.FlyList.ItemSelected += OnSelectedItem;
        }

        
        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageNavigationClass;
            if (item != null)
            {
                if (item.Title=="LogOut")
                {
                    App.Current.MainPage = new LoginUl();
                    return;
                }
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyout.FlyList.SelectedItem = item;
                IsPresented = false;
            }
        }
    }
}