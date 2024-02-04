using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using PregnAPI.DTO;
using PregnAPI.Models;

namespace PregnAPI.Controllers{
    //localhost:5000/api/User
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase{
      private readonly PregnAppContext _context;
      public UserController(PregnAppContext context)
      {
        _context=context;

      }

     
      
      [HttpPost("login")]
      public async Task<IActionResult> Login(string mail,string şifre){    
        if(await _context.Users.FirstOrDefaultAsync(o=>o.UserMail==mail)==null){
             var errorModel = new ErrorModel(404,"Hatalı mail adresi");
             return NotFound(errorModel);
        }
         string pass=PasswordHasher.HashPassword(şifre);
         var user=await _context.Users.Where(vt=>vt.UserMail==mail&&vt.UserPassword==pass).Select(p=>UserToUserDTO(p)).ToListAsync();
         if(user.Count==0){
               var errorModel=new ErrorModel(404,"Hatalı şifre");
               return NotFound(errorModel);
         }else{
               return Ok(user);
         }
      }

      [HttpGet("GetUser")]
      public async Task<IActionResult> GetUser(string mail){
        if(mail==null){
          var errorModel=new ErrorModel(400,"İstek atarken mail girilmedi!");
          return BadRequest(errorModel);
        }else{
          var user=await _context.Users.FirstOrDefaultAsync(vt=>vt.UserMail==mail);
          if(user==null){
            var errorModel=new ErrorModel(404,"Bu mailde kullanıcı bulunamadı!");
           return NotFound(errorModel);
          }else{
           return Ok(UserToUserDTO(user));
          }
        }
      }
      
      [HttpPut("{mail}")]
      public async Task<IActionResult> UpdateUserInformation(string mail,UserDTO userDTO){
          if(mail==null || userDTO==null){
            BadRequest();
          }
          var kullanıcı=await _context.Users.FirstOrDefaultAsync(o=>o.UserMail==mail);
          if(kullanıcı==null){
            var errorModel=new ErrorModel(404,"Böyle bir mail yok");
            return NotFound(errorModel);
          }else{
            kullanıcı.ReglDate=userDTO.ReglDate;
            kullanıcı.UserName=userDTO.UserName;
            kullanıcı.UserWeight=userDTO.UserWeight;
            _context.Users.Update(kullanıcı);
            try{
             await _context.SaveChangesAsync();
             return Ok(userDTO);
            }catch(Exception){
              return NotFound();
            }
          }
      }

      [HttpDelete("{mail}")]
      public async Task<IActionResult> DeleteUser(string mail){
         if(mail==null){
            return NotFound();
         }
         var user=await _context.Users.FirstOrDefaultAsync(k=>k.UserMail==mail);
         if(user==null){
            return NotFound();
         }else{
            _context.Remove(user);

            try{
             await _context.SaveChangesAsync();
            }catch(Exception){
             return NotFound();
            }
            return NoContent();
         }
      }
      [HttpPost("register")]
      public async Task<IActionResult> Register(User user){
        if(isValidEmail(user.UserMail)==false){
           var errorModel=new ErrorModel(404,"Geçersiz mail adresi");
           return NotFound(errorModel);
        }else if(isValidPassword(user.UserPassword)==false){
         var errorModel=new ErrorModel(404,"Şifreniz en az 6 karakter uzunluğunda olmalı");
         return NotFound(errorModel);
        }else if(isValidName(user.UserName)==false){
         var errorModel=new ErrorModel(404,"Kullanıcı adınız çok kısa. En az 3 karakter uzunluğunda olmalı");
         return NotFound(errorModel);}else{
         var kullanıcı=await _context.Users.FirstOrDefaultAsync(o=>o.UserMail==user.UserMail);
         if(kullanıcı==null){
            user.UserPassword=PasswordHasher.HashPassword(user.UserPassword);
            _context.Users.Add(user);
            try{
            await _context.SaveChangesAsync();}catch(Exception){
               return NotFound();
            }
            var k=await _context.Users.Where(o=>o.UserMail==user.UserMail).Select(p=>UserToUserDTO(p)).ToListAsync();
            return Created("",k);
         }else{
            var errorModel=new ErrorModel(404,"Bu email zaten kullanılmış");
            return NotFound(errorModel);
         }

         }
         
      }

      [HttpPost("WeightChecker")]
      public async Task<IActionResult> SendWeight(Weight weight){
         if(weight==null){
          var errorModel=new ErrorModel(404,"Weight objesi boş!");
           return NotFound(errorModel);
         }else{
          var kilo=await _context.Weights.OrderByDescending(vt=>vt.WeightDate).FirstOrDefaultAsync(vt=>vt.UserMail==weight.UserMail);
          var user=await _context.Users.FirstOrDefaultAsync(vt=>vt.UserMail==weight.UserMail);
          if(kilo==null){
            if(user==null){
              var errorModel=new ErrorModel(404,"Girdiğiniz mailde kayıtlı kullanıcı bulunmadı!");
              return NotFound(errorModel);
            }else{
               var initialWeight=new Weight();
               initialWeight.WeightHour=user.ReglDate; initialWeight.WeightDate=user.ReglDate; initialWeight.WeightDegree=0;
               initialWeight.UserWeight=user.UserWeight; initialWeight.UserMail=user.UserMail; initialWeight.Difference=0;
               _context.Weights.Add(initialWeight);
               _context.Weights.Add(weight);
                try{
            await _context.SaveChangesAsync();}catch(Exception){
               return NotFound();
            }  return Created("",weight);
            }
          }
            return Created("",weight);
         }
      }
      private static UserDTO UserToUserDTO(User user){
         return new UserDTO{
           UserMail=user.UserMail,
           UserName=user.UserName,
           UserWeight=user.UserWeight,
           ReglDate=user.ReglDate
         };
      }
      private static Boolean isValidEmail(string email){
         try{
           MailAddress mailAddress=new MailAddress(email);
           return true;
         }catch(Exception){
            return false;
         }
      }
      private static Boolean isValidPassword(string password){
          if(password.Length>=6){
            return true;
          }else{
            return false;
          }
      }
      private static Boolean isValidName(string password){
          if(password.Length>=3){
            return true;
          }else{
            return false;
          }
      }
    }
}