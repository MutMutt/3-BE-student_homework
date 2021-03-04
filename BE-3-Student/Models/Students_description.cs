using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_3_Student.Models
{
    public class Students_description
    {
        [Key]
        public int id { get; set; }
        public int students_id { get; set; }
        public int age { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;  }
        public string address { get; set; }
        public string country { get; set; }

    }
}
