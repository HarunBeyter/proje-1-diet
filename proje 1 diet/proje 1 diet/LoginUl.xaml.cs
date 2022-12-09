using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proje_1_diet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUl : ContentPage
    {
        UserRepository userRepository=new UserRepository();
        public LoginUl()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void TapGestureRecognizer_ForgetPassword(object sender,EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string email=txtmail.Text;
            string password = txtPassword.Text;
            string token = await userRepository.SignIn(email, password);
            if(!string.IsNullOrEmpty(token))
            {
               await Navigation.PushAsync(new MainPage(email));
            }
            else
            {
                await DisplayAlert("warning", "password or mail is incorrect", "ok");
                
            }
        }

        private void eye_Clicked(object sender, EventArgs e)
        {
            if(txtPassword.IsPassword==true)
            {
                txtPassword.IsPassword = false;
                eye.Source = "eye.png";
            }
            else
            {
                txtPassword.IsPassword = true;
                eye.Source = "hidden.png";
            }
        }
    }
}