﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace MaterApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ? Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Salt { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; }

        public string ?FirstName { get; set; }
        public string ? LastName { get; set; }

        public DateTime ? DateOfBirth { get; set; }

        public Gender ? Gender { get; set; }

        public string ?Address { get; set; }

       
        public virtual ICollection<UserInterest> UserInterests { get; set; }
   
        public virtual ICollection<BlockedUsers> BlockedUsers { get; set; }

        [InverseProperty("Likee")]
        public ICollection<Like> LikedByUsers { get; set; }

        public string ?ProfilePhoto { get; set; }

        public string? Phone { get; set; } = string.Empty;

        public User()
        {
            BlockedUsers = new List<BlockedUsers>();
        }

        public void SetPassword(string password)
        {
            // Генерация случайной соли
            var saltBytes = GenerateSalt();

            // Хеширование пароля с использованием соли
            PasswordHash = HashPassword(password, saltBytes);

            // Сохранение соли
            Salt = ConvertToBase64String(saltBytes);
        }


        public bool VerifyPassword(string password)
        {
            // Получение соли из сохраненного значения
            var saltBytes = ConvertFromBase64String(Salt);

            // Хеширование введенного пароля с использованием соли
            var hashedPassword = HashPassword(password, saltBytes);

            // Сравнение хешей паролей
            return string.Equals(hashedPassword, PasswordHash);
        }

        private byte[] GenerateSalt()
        {
            var saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        private string HashPassword(string password, byte[] saltBytes)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);
                var hashedBytes = sha256.ComputeHash(combinedBytes);
                return ConvertToBase64String(hashedBytes);
            }
        }

        private string ConvertToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        private byte[] ConvertFromBase64String(string base64String)
        {
            base64String = base64String.Replace('-', '+').Replace('_', '/');
            switch (base64String.Length % 4)
            {
                case 2: base64String += "=="; break;
                case 3: base64String += "="; break;
            }
            return Convert.FromBase64String(base64String);
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }


}
