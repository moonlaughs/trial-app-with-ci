using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace trial_api.Models
{
    public record Book
    {
        public Guid Id { get; init; }
        public string BookName { get; init; }

        public string Password { get; init; }
        public string Author { get; init; }
        // public virtual string PasswordStored { get; init; }

        // [NotMapped]
        // public string Password
        // {
        //     get {return Decrypt(PasswordStored);}
        //     set{PasswordStored = Encrypt(value);}
        // }
    }
}