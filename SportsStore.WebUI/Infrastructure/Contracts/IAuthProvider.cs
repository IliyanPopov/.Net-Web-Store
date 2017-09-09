namespace SportsStore.WebUI.Infrastructure.Contracts
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}