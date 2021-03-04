using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AddStudent
    {
        [Required]
        public string Grade { get; set; }
        [Required]
        public int Students_Id { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name {get; set;}
        [Required]
        public string Address {get; set;}
        [Required]
        public string Country {get; set;}
    }
}