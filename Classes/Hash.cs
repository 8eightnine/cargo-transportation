
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace cargo_transportation.Classes
{
    public static class Hash
    {
        public static string hashPassword(string password)
        {
            using (var hash = SHA1.Create())
            {
                string temp = string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
                hash.Dispose();
                return temp;
            }
        }
    }
}