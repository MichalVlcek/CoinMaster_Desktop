using System;
using System.Security.Cryptography;
using System.Text;

namespace CoinMaster.Utility
{
    public static class UserUtility
    {
        public static string HashPassword(string password)
        {
            using var sha256 = new SHA256Managed();
            var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashed).Replace("-", "");
        }
    }
}