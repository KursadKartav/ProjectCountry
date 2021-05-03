using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCountry.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        //public int CountryId { get; set; }


        //public int StateId { get; set; }
        public State State { get; set; }
    }
}
