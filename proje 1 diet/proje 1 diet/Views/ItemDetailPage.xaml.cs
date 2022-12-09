using proje_1_diet.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace proje_1_diet.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}