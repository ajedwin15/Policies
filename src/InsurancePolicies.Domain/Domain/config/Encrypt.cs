using BC = BCrypt.Net.BCrypt;

namespace src.InsurancePolicies.Domain.Domain.config
{
    public static class Encrypt
    {
        public static string hashData(this string data)
        {
            return BC.HashPassword(data);
        }
        public static bool verifyHashData(string data, string hashData)
        {
            return BC.Verify(data, hashData); ;
        }
    }
}