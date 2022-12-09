using proje_1_diet.ViewModels;
using proje_1_diet.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace proje_1_diet
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void navigationList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}
