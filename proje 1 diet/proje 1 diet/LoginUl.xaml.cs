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
            try
            {
                string email = txtmail.Text;
                string password = txtPassword.Text;
                string token = await userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    App.Current.MainPage = new MainPage(email);
                }
                else
                {
                    await DisplayAlert("warning", "please enter your password or email", "ok");

                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("error",ex.Message, "ok");
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