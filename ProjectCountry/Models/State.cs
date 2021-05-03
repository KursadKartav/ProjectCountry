using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCountry.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int PlateCode { get; set; }
        public int PersonCount
        {
            get { return People == null ? 0 : People.Count; }
        }
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
