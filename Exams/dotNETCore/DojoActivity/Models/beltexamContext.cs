using Microsoft.EntityFrameworkCore;
 
namespace beltexam.Models
{
    public class beltexamContext: DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public beltexamContext(DbContextOptions<beltexamContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<DojoActivity> Activities {get; set;}
        public DbSet<Participant> Participant {get; set;}
    }
}




