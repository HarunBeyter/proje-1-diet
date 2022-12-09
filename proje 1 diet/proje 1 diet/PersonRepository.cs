using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace proje_1_diet
{
    public class PersonRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<bool> Save(Person person)
        {
            var data=await firebaseClient.Child(nameof(Person)).PostAsync(JsonConvert.SerializeObject(person));
            if(!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
    }
}
