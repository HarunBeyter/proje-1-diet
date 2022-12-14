using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel;

namespace proje_1_diet
{
    public class PersonRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://dietdatabase-b0f8f-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<bool> Save(Person person)
        {
            await firebaseClient.Child(nameof(Person)+"/"+person.Id).PutAsync(JsonConvert.SerializeObject(person));
            return true;
        }

        public async Task<bool> Update(Person person)
        {
            await firebaseClient.Child(nameof(Person) + "/" + person.Id).PutAsync(JsonConvert.SerializeObject(person));
            return true;
        }
    }
}
