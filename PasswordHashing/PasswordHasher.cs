using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using trial_api.PasswordHashing;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace trial_api.PasswordHashing

{
    public class PasswordHasher// : IPasswordHasher
    {
        byte[] salt = new byte[128 / 8];
        // private const int SaltSize = 16; // 128 bit 
        public string hashPass(string password)
        {
            // using (var rng = RandomNumberGenerator.Create())
            // {
            //     rng.GetBytes(salt);
            // }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            ));
            return hashedPassword;
        }
        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // byte[] salt2 = new byte[128 / 8];
            // var saltBytes = Convert.FromBase64String(salt2.ToString());
            var hashOfEntered = hashPass(enteredPassword);
            // var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            // return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
            return hashOfEntered == storedHash;
        }
        // private const int SaltSize = 16; // 128 bit 
        // private const int KeySize = 32; // 256 bit
        // public (bool Verified, bool NeedsUpgarde) Check(string hash, string password)
        // {
        //     var parts = hash.Split('.',3);

        //     if(parts.Length != 3){
        //         throw new FormatException("Unexpected hash format. " + 
        //         "Should be formatted as `{iterations}.{salt}.{hash}`");
        //     }

        //     var iterations = Convert.ToInt32(parts[0]);
        //     var salt = Convert.FromBase64String(parts[1]);
        //     var key = Convert.FromHexString(parts[2]);

        //     var needsUpgarde = iterations != Options.Iterations;

        //     using (var algorithm = new Rfc2898DeriveBytes(
        //         password,
        //         salt,
        //         iterations,
        //         HashAlgorithmName.SHA256
        //     ))
        //     {
        //         var keyToCheck = algorithm.GetBytes(KeySize);
        //         var verified = keyToCheck.SequenceEqual(key);
        //         return (verified, needsUpgarde);
        //     }
        // }

        // public string Hash(string pass)
        // {
        //     using (var algorithm = new Rfc2898DeriveBytes(
        //         pass,
        //         SaltSize,
        //         Options.Iterations,
        //         HashAlgorithmName.SHA256
        //     ))
        //     {
        //         var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        //         var salt = Convert.ToBase64String(algorithm.Salt);

        //         return $"{Options.Iterations}.{salt}.{key}";
        //     }
        // }

        // public PasswordHasher(IOptions<HashOptions> options)
        // {
        //     Options = options.Value;
        // }

        // private HashOptions Options { get; }
    }
}