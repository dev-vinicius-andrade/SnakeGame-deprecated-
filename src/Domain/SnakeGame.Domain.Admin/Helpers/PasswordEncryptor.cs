using System;
using System.Security.Cryptography;
using System.Text;
using static SnakeGame.Domain.Admin.Models.AdminModels;

namespace SnakeGame.Domain.Admin.Helpers
{
    internal class PasswordEncryptor
    {
        private readonly PasswordEncrypt _passwordEncrypt;

        public PasswordEncryptor(PasswordEncrypt passwordEncrypt )
        {
            _passwordEncrypt = passwordEncrypt;
        }

        public  string EncrypPassword(string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes($"{password}{_passwordEncrypt.GuidKey.ToString()}");
            var base64Password = Convert.ToBase64String(passwordBytes, Base64FormattingOptions.None);
            var base64PasswordBytes = Encoding.ASCII.GetBytes(base64Password);
            var sha256Bytes = SHA256.Create().ComputeHash(base64PasswordBytes);
            var builder = new StringBuilder();  
            foreach (var t in sha256Bytes)
                builder.Append(t.ToString("x2"));
            return builder.ToString();  
        }
    }
}
