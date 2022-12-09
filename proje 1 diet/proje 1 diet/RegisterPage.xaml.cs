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
    public partial class RegisterPage : ContentPage
    {
        PersonRepository repository=new PersonRepository();
        UserRepository userRepository=new UserRepository();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string surName = TxtSurName.Text;
                string email = TxtMail.Text;
                string password = TxtPassword.Text;
                string passwordIsTrue = TxtPasswordistrue.Text;
                DateTime BirthDate = TxtBirthTime.Date;
                if (string.IsNullOrEmpty(name)) { await DisplayAlert("warning", "Please enter your name", "cancel"); return; }
                if (string.IsNullOrEmpty(surName)) { await DisplayAlert("warning", "Please enter your surname", "cancel"); return; }
                if (string.IsNullOrEmpty(email)) { await DisplayAlert("warning", "Please enter your mail", "cancel"); return; }
                if (string.IsNullOrEmpty(password)) { await DisplayAlert("warning", "Please enter your password", "cancel"); return; }
                if (string.IsNullOrEmpty(passwordIsTrue)) { await DisplayAlert("warning", "Please enter your confirm password", "cancel"); return; }
                if (password != passwordIsTrue) { await DisplayAlert("warning", "the password must match to confirm password", "cancel"); return; }
                if (string.IsNullOrEmpty(BirthDate.ToString())) { await DisplayAlert("warning", "Please enter your name", "cancel"); return; }


                string[] caloei = new string[3] { "a", "a", "a" };

                Person person = new Person();
                person.Name = name;
                person.SurName = surName;
                person.Mail = email;
                person.Password = password;
                person.BirthTime = BirthDate;
                person.Calories = caloei;
                bool IsSaved = await userRepository.Register(email, password, name);
                var isSaved = await repository.Save(person);
                if (isSaved && IsSaved)
                {
                    await DisplayAlert("Information", "person has been saved", "ok");
                }
                else
                {
                    await DisplayAlert("Error", "person saved faild", "ok");
                    return;
                }


            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("warning", "email exist", "ok");
                }
                else
                {
                    await DisplayAlert("error", exception.Message, "ok");
                }
            }

            
            
            
        }
    }
}