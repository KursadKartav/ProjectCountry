using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCountry.Models
{
    public class WorldModel
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int PersonId { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
