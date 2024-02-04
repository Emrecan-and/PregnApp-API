using Microsoft.EntityFrameworkCore;

namespace PregnAPI.Models{
    public class PregnAppContext:DbContext{
        public PregnAppContext(DbContextOptions<PregnAppContext> options): base(options)
        {   
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Baby> Babies{get; set;}

        public DbSet<Article> Articles{get; set;}

        public DbSet<Weight>Weights{get; set;}
    }
     
}