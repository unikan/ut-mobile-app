using System;
using System.Collections.Generic;
using System.Text;
using XamarinFirebase.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UtMobileApp.Extensions;

namespace XamarinFirebase.Helper
{
    class FirebaseHelper
    {
        readonly FirebaseClient firebase = new FirebaseClient("https://utappdatabase.firebaseio.com/");


        public async Task<List<Registrations>> GetAllRegistrations()
        {

            return (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Select(item => new Registrations
              {
                  FirstName = item.Object.FirstName,
                  LastName = item.Object.LastName,
                  Semestri = item.Object.Semestri,
                  Email = item.Object.Email,
                  IndexNr = item.Object.IndexNr,
                  Faculty = item.Object.Faculty,
                  Program = item.Object.Program,
                  Group = item.Object.Group,
              }).ToList();
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            return (await firebase
              .Child("Schedule")
              .OnceAsync<Schedule>()).Select(item => new Schedule
              {
                  OrariDimrorID = item.Object.OrariDimrorID,
                  OrariVerorID = item.Object.OrariVerorID,
                  KonsulltimeID = item.Object.KonsulltimeID,
                  ProvimeID = item.Object.ProvimeID,
                  KonllokfiumeID = item.Object.KonllokfiumeID
              }).ToList();
        }

        public async Task AddPerson(string email, string firstname, string lastname, string indexnumber, string semestri)
        {
            
            await firebase
              .Child("Registrations")
              .PostAsync(new Registrations() { Email = email, FirstName = firstname, LastName = lastname, IndexNr = indexnumber, Semestri = semestri });
        }

        public async Task<bool> UserExists(string email)
        {
            var allRegistrations = await GetAllRegistrations();
            await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>();
            return allRegistrations.Any(a => a.Email == email);
        }

        public async Task AddStudent( string email, string firstname, string lastname, string indexnumber, string group, string semestri, string faculty, string program)
        {

            await firebase
              .Child("Registrations")
              .PostAsync(new Registrations() {Email = email ,FirstName = firstname, LastName = lastname, IndexNr = indexnumber, Group = group ,Semestri = semestri, Faculty = faculty, Program = program });
        }

        public async Task<Registrations> GetPerson(string email)
        {
            var allRegistrations = await GetAllRegistrations();
            await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>();
            return allRegistrations.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task UpdatePerson(string email, string indexnumber)
        {
            var toUpdatePerson = (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await firebase
              .Child("Registrations")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Registrations() { Email = email, IndexNr = indexnumber });
        }

        public async Task DeletePerson(string email)
        {
            var toDeletePerson = (await firebase
              .Child("Registrations")
              .OnceAsync<Registrations>()).Where(a => a.Object.Email == email).FirstOrDefault();
            await firebase.Child("Registrations").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}
