using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using trial_api.Models;
using trial_api.PasswordHashing;

namespace trial_api.Data
{
    //this adds some static data to db
    public class TestData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                if(context.Books != null && context.Books.Any())
                    return;
                
                var users = TestData.GetAllUsers().ToArray();
                context.Books.AddRange(users);
                context.SaveChanges();
            }
        }

        public static List<Book> GetAllUsers()
        {
        PasswordHasher ph = new PasswordHasher();
            // string hash1 = ph.hashPass("Cvb123");
            List<Book> userList = new()
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    BookName = "User1",
                    Password = ph.hashPass("Cvb123"),
                    Author = "author"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    BookName = "User2",
                    Password = ph.hashPass("Cvb12322"),
                    Author = "author2"
                }
            };
            return userList;
        }
    }
}