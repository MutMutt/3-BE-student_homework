using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_3_Student.Models
{
    public class Students
    {
        [Key]

        public int id { get; set; }
        public string grade { get; set; }


    }
}
