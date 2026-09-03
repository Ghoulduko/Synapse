using System.Security.Cryptography;
using System.Text;

namespace Synapse.Infrastructure.ExtensionMethods;

public static class TokenHasher
{
    public static string HashToken(this string token)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}