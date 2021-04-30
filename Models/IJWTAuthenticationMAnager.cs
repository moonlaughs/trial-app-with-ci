namespace trial_api.Models
{
    public interface IJWTAuthenticationMAnager
    {
         string Authenticate(string username, string password);
    }
}