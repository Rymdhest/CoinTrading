using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CoinTrading.Pages
{
    public class SignupModel : PageModel
    {
        public void OnGet()
        {
            Debug.WriteLine("Test");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserContext usersDB = new UserContext();

            var email = Request.Form["email"];
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var rep_password = Request.Form["repete_password"];
            if (password != rep_password) Debug.WriteLine("Nej nej det är fel här....");
            else Debug.WriteLine("Ja det är rätt");
            Users user = new();
            user.Email = email;
            user.Username = username;
            user.Password = password;
            usersDB.Users.Add(user);
            usersDB.SaveChanges();

            return RedirectToPage("./Index");
        }
    }

    /*
           * CREATE TABLE users (
           id INTEGER PRIMARY KEY,
           username TEXT,
           password TEXT,
           email TEXT,
           timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
           );
    */

    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Prices
    {
        public string close_price { get; set; }
    }

    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Prices> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\erikm\\source\\repos\\CoinTrading\\CoinTrading\\Data\\data.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
        }
    }
}
