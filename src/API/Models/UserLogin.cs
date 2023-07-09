namespace Carmax.API.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
