using System.ComponentModel.DataAnnotations;

namespace PregnAPI.Models{
  public class Weight{
      
      [Key]
      public string UserMail { get; set; }
      
    [Required]
      public int UserWeight { get; set; }

      [Required]
      public int WeightDegree { get; set; }

       [Required]
       public decimal Difference { get; set; } 

       [Required]
       public DateTime WeightDate { get; set; }

       [Required]
       public DateTime WeightHour{ get; set; }

  }
}