using System.Security.Cryptography;
using System.Text;

namespace net.applicationperformance.ChatApp.Auth;

public static class Token
{ 
    public static string ComputeToken(Guid id, string userName)
    {
        if (id == Guid.Empty)
        {
            return string.Empty;
        }

        if (userName == string.Empty)
        {
            return string.Empty;
        }
        
        const string extraKey = $"fdsfkl;kfjflkj455rttrtret4ttregdfggdfgfgdf24554frgfgdfgdfgb$#43434"; // need to store somewhere else obviously...
        var bytes = Encoding.UTF8.GetBytes(userName+id.ToString()+extraKey);
        var hashBytes = SHA256.HashData(bytes);
        var idBytes = Encoding.UTF8.GetBytes(id.ToString()+":");
        return Convert.ToBase64String(idBytes.Concat(hashBytes).ToArray());
    }

    private static (Guid, string) ParseToken(string token)
    {
        try
        {
            var arr = Convert.FromBase64String(token);
            var decoded = Encoding.UTF8.GetString(arr).Split(":");
            return (new Guid(decoded[0]), decoded[1]);
        }
        catch (Exception)
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