using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api_Project.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public  DbSet<User> User { get; set; }
        
        



    }
}