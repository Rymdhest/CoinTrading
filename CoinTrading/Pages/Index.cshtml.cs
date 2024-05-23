using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;

namespace WebApplication_Sqlite_Test_Slask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            UserContext user = new UserContext();

            foreach(var u in user.Users)
            {
                Debug.WriteLine($"{u.Username}");
                Debug.WriteLine($"{u.Email}");
                Debug.WriteLine($"{u.Password}");
            }
        }

        public JsonResult OnPost()
        {
            UserContext db = new UserContext();
            Users user = new();

            var email = Request.Form["Email"];
            /*var username = Request.Form["username"];
            var password = Request.Form["password"];
            var rep_password = Request.Form["repete_password"];*/

            user.Username = "TestUser";
            user.Email = email;
            user.Password = "TestPassword";


            db.Users.Add( user );
            db.SaveChanges();
            return new JsonResult(user);
        }
    }


    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Prices
    {
        public int Id { get; set; }
        public string close_price { get; set; }
    }

    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Prices> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Data\data.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
        }
    }
}
