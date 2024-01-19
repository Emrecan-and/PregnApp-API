using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

    namespace PregnAPI.Models{
        public class Baby{
         [Key]
        public int Id { get; set; }

        [Required]      
        public int Week { get; set; }
        
        [Required]
        [DefaultValue(0)]
        public decimal Height { get; set; }
        
        [Required]
        [DefaultValue(0)]
        public int Weight { get; set; }
        
        
        [Required]
        public string Fruit { get; set; }

        }
    }