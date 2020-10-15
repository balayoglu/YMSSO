namespace YMDiscourseSSO.Services
{
    public interface IHmacSHA256
    {
        string CreateHexToken(string message, string secret);

        bool VerifyHexToken(string message, string secret, string hexToken);
    }
}
