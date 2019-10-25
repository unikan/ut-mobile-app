using System;
using System.Collections.Generic;
using System.Text;
using XamarinFirebase.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinFirebase.Helper
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://utappdatabase.firebaseio.com/");


        public async Task<List<Registrations>> GetAllRegistrations()
        {

            return (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Select(item => new Registrations
              {
                  FirstName = item.Object.FirstName,
                  LastName = item.Object.LastName,
                  Semestri = item.Object.Semestri,
                  StudentId = item.Object.StudentId,
                  IndexNr = item.Object.IndexNr
              }).ToList();
        }

        public async Task AddPerson(string studentId, string firstname, string lastname, long indexnumber, string semestri)
        {

            await firebase
              .Child("Registrations")
              .PostAsync(new Registrations() { StudentId = studentId, FirstName = firstname, LastName = lastname, IndexNr = indexnumber, Semestri = semestri });
        }

        public async Task<Registrations> GetPerson(string studentId)
        {
            var allRegistrations = await GetAllRegistrations();
            await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>();
            return allRegistrations.Where(a => a.StudentId == studentId).FirstOrDefault();
        }

        public async Task UpdatePerson(string studentId, long indexnumber)
        {
            var toUpdatePerson = (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Where(a => a.Object.StudentId == studentId).FirstOrDefault();

            await firebase
              .Child("Registrations")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Registrations() { StudentId = studentId, IndexNr = indexnumber });
        }

        public async Task DeletePerson(string studentId)
        {
            var toDeletePerson = (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Where(a => a.Object.StudentId == studentId).FirstOrDefault();
            await firebase.Child("Registrations").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
