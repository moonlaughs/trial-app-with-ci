using Microsoft.AspNetCore.Mvc;
using trial_api.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trial_api.DTOs;
using trial_api.Models;
using trial_api.PasswordHashing;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using trial_api.Data;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
// using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// using System.Threading.Tasks;

namespace trial_api.Controllers
{
    [ApiController]
    [Route("tokens")]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DataContext _context;

        public TokenController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDTOs _createBookData)
        {
            if(_createBookData != null && _createBookData.BookName != null && _createBookData.Password != null)
            {
                var book = await GetBook(_createBookData.BookName, _createBookData.Password);
                if(book != null)
                {
                    // create claims details based on the user information
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                            // new Claim("Id", 5.ToString()),
                        new Claim("BookName", book.BookName),
                        new Claim("Password", book.Password)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else {
                    return BadRequest("Invalid credentials");
                }
            } else {
                return BadRequest();
            }
        }

        private async Task<Book> GetBook(string bookName, string pass)
        {
        PasswordHasher ph = new PasswordHasher();
            return await _context.Books.FirstOrDefaultAsync(b => b.BookName == bookName && b.Password == ph.hashPass(pass));
        }

    }
}