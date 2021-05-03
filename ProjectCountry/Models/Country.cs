using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCountry.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Climate { get; set; }
        public int Population { get; set; }
        [Display(Name = "Ülkeye Bağlı Şehir Sayısı")]
        public int StateCount
        {
            get { return States == null ? 0 : States.Count; }
        }

        public ICollection<State> States { get; set; }
        //public ICollection<Person> People { get; set; }
    }
}
