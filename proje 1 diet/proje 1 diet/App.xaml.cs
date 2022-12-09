using proje_1_diet.Services;
using proje_1_diet.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginUl());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
