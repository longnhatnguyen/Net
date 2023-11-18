namespace Net6.IRepository
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken(string username);
    }
}
