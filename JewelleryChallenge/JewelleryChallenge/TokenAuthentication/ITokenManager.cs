namespace JewelleryChallenge.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string username, string password);
        Token NewToken(string username);
        bool VerifyToken(string token);
    }
}