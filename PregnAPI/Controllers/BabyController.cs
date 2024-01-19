using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PregnAPI.DTO;
using PregnAPI.Models;

namespace PregnAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BabyController:ControllerBase{
         private readonly PregnAppContext _context;        
    public BabyController(PregnAppContext context)
    {
        _context=context;  
    }
    
    [HttpGet("GetBabyDatas")]
    public async Task<IActionResult> GetBabyDatas(String Usermail){
       if(Usermail==null){
        ErrorModel errorModel=new ErrorModel(400,"İstek atarken kullanıcı maili girmelisiniz!");
        return BadRequest(errorModel);
       }else{
         User user=await _context.Users.FirstOrDefaultAsync(vt=>vt.UserMail==Usermail);
         if(user==null){
            ErrorModel errorModel=new ErrorModel(404,"Geçersiz mail!");
           return NotFound(errorModel);
         }else{
          Baby baby=await _context.Babies.FirstOrDefaultAsync(vt=>vt.Week==WeekCalculator(user.ReglDate));
          if(baby==null){
            ErrorModel errorModel=new ErrorModel(400,"Tarihte hata var!");
           return BadRequest(errorModel);
          }else{
           BabytoBabyDTO(baby);
           return Ok(BabytoBabyDTO(baby));
          }
         }
       }
    }  
    public static BabyDTO BabytoBabyDTO(Baby baby){
     return new BabyDTO{
        Week=baby.Week,
        Height=baby.Height,
        Weight=baby.Weight,
        Fruit=baby.Fruit
     }; 
    }
    public static int WeekCalculator(DateTime reglDate)
        {
        TimeSpan time=DateTime.Now-reglDate;
        return time.Days/7;
    }
    }
}