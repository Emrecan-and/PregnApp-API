using System.ComponentModel.DataAnnotations;

namespace PregnAPI.Models{
 public class Article{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }

    public DateTime ArticleDate { get; set; }
         


 }
}