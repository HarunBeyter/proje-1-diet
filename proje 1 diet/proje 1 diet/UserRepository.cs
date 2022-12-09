using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace proje_1_diet
{
    public class UserRepository
    {
        string webAPIKey = "AIzaSyD8uBZu4vBAUW7zSmemW3z3g39B9F_wYXI";
        FirebaseAuthProvider authProvider;

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }
        public async Task<bool> Register(string email,string password,string name)
        {
            var token=await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);
            if(!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string> SignIn(string email,string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if(!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }
    }
}
