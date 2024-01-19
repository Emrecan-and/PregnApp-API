using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PregnAPI.Models{
    public class User{
        
        [EmailAddress]
        [Key]
        public string UserMail { get; set; }

        [Required]
        public string UserName { get; set; }
         

        [Required]
        public string UserPassword { get; set; }


        [Required]
        public DateTime ReglDate { get; set; }


        [Required]
        public int UserWeight { get; set; }
    }
}