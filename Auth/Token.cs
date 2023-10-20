using System.Security.Cryptography;
using System.Text;

namespace net.applicationperformance.ChatApp.Auth;

public class Token
{ 
    public static string ComputeToken(Guid id, string userName)
    {
        var bytes = Encoding.UTF8.GetBytes(userName+id.ToString());
        var hashBytes = SHA256.HashData(bytes);

        var builder = new StringBuilder($"{id.ToString()}:");
        foreach (var b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }

        
        var arr =  Encoding.UTF8.GetBytes(builder.ToString());

        return Convert.ToBase64String(arr);
    }

    private static (Guid, string) ParseToken(string token)
    {
        try
        {
            var arr = Convert.FromBase64String(token);
            var decoded = Encoding.UTF8.GetString(arr).Split(":");
            return (new Guid(decoded[0]), decoded[1]);
        }
        catch (Exception e)
        {
            return (Guid.Empty, "");
        }
    }

    public static Guid ExtractId(string token)
    {
        var decoded = ParseToken(token);
        return decoded.Item1;
    }

    public static bool ValidateToken(string userName, string token)
    {
        var decoded = ParseToken(token);
        return token.Equals(ComputeToken(decoded.Item1,userName));
    }    
}