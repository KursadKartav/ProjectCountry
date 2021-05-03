using ProjectCountry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCountry.Models
{
    public class DbInitilazier
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.People.Any())
            {
                return;   // DB has been seeded
            }

            var people = new Person[]
            {
            new Person{Name="Carson",LastName="Alexander"/*,StateId=1*//*,CountryId=1*/},
            new Person{Name="Emre",LastName="Keskin"/*,StateId=2*//*,CountryId=2*/},
            new Person{Name="Ayşe",LastName="Ayan"/*,StateId=5*//*,CountryId=2*/},
            new Person{Name="Ali",LastName="Bakır"/*,StateId=4*//*,CountryId=1*/},
            new Person{Name="Mehmet",LastName="Ak"/*,StateId=3*//*,CountryId=3*/},
            new Person{Name="Selim",LastName="Kara"/*,StateId=2*//*,CountryId=4*/}
            };
            foreach (Person s in people)
            {
                context.People.Add(s);
            }
            context.SaveChanges();

            var states = new State[]
            {
            new State{StateName="İstanbul",CountryId=1,PlateCode=100},
            new State{StateName="Ankara",CountryId=1,PlateCode=200},
            new State{StateName="California",CountryId=2,PlateCode=300},
            new State{StateName="Texas",CountryId=2,PlateCode=400},
            new State{StateName="Florida",CountryId=2,PlateCode=500},
            new State{StateName="Londra",CountryId=3,PlateCode=600},
            new State{StateName="Cambridge",CountryId=3,PlateCode=700},
            new State{StateName="Liverpool",CountryId=3,PlateCode=800},
            new State{StateName="Lazio",CountryId=4,PlateCode=900},
            new State{StateName="Roma",CountryId=4,PlateCode=250},
            new State{StateName="Milano",CountryId=4,PlateCode=390},
            new State{StateName="Napoli",CountryId=4,PlateCode=860}
            
            };
            foreach (State c in states)
            {
                context.States.Add(c);
            }
            context.SaveChanges();

            var countries = new Country[]
            {
            new Country{CountryName="Turkey",Climate="Monsoon Climate",Population=83154990},
            new Country{CountryName="America",Climate="Equatorial climate",Population=327000000},
            new Country{CountryName="England",Climate="Tropical climate",Population=200000000},
            new Country{CountryName="Italy",Climate="Desert climate",Population=180000000}
           
            };
            foreach (Country e in countries)
            {
                context.Countries.Add(e);
            }
            context.SaveChanges();
        }

    }
}
