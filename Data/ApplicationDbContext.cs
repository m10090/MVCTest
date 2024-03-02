using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace testMvc.Data{
    public class ApplicationDbContext : DbContext
    {
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
        public DbSet<User> Users { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }
}
