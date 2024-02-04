using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PregnAPI.DTO{
    public class Login{
        public string Email { get; set; }

        public string Şifre { get; set; }
    }
}