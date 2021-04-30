using Microsoft.AspNetCore.Mvc;
using trial_api.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trial_api.DTOs;
using trial_api.Models;
using trial_api.PasswordHashing;
using Microsoft.AspNetCore.Authorization;

namespace trial_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        // private readonly IInMemoryUserRepo repo;
        // public UserController(IInMemoryUserRepo repo){
        //     this.repo = repo;
        // }
        PasswordHasher ph = new PasswordHasher();

         private readonly EfCoreUserRepository repo;
        public BookController(EfCoreUserRepository repo){
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(){
            PasswordHasher ph = new PasswordHasher();
            string stored = ph.hashPass("Izabela");
            System.Console.WriteLine(stored);
            System.Console.WriteLine(ph.VerifyPassword("Izabela", stored));
            var allbooks = (await repo.GetAll()).Select(book => book.AsDTO());
            return allbooks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetUserAsync(Guid id)
        {
            var user = await repo.Get(id);
            if(user is null){
                return NotFound();
            }
            return Ok(user.AsDTO());
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateUserAsync(CreateBookDTOs userDTO)
        {
            Book newUser = new()
            {
                Id = Guid.NewGuid(),
                BookName = userDTO.BookName,
                Password = ph.hashPass(userDTO.Password),
                Author = "newAuthor"
            };

            await repo.Add(newUser);

            return CreatedAtAction(nameof(GetUserAsync), new {id = newUser.Id}, newUser.AsDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(Guid id, UpdateBookDTO userDto)
        {
            Book existingUser = await repo.Get(id);
            if(existingUser is null){
                return NotFound();
            }

           
            Book updatedUser = existingUser with 
            {
                Id = existingUser.Id,
                BookName = existingUser.BookName,
                Password = ph.hashPass(userDto.Password),
                Author = "newAuthor"
            };

            await repo.Update(updatedUser);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(Guid id){
            Book existingUser = await repo.Get(id);

            if(existingUser is null)
            {
                return NotFound();
            }

            await repo.Delete(id);
            return NoContent();
        }
    }
}