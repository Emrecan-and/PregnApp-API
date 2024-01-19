namespace PregnAPI.Models
{
    public class ErrorModel{
       public int StatusCode { get; set; }
       public string Message { get; set; }

       public ErrorModel(int durumKodu,string error)
       {
          StatusCode=durumKodu;
          Message=error;
       }
    }

}