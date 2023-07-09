namespace Carmax.API.Models
{
    // We are not taking data from data base so we get data from constant
    public class UserConstants
    {
        public static List<UserModel> Users = new()
            {
                    new UserModel(){ Username="administrator@localhost",Password="Administrator1!",Role="Admin"}
            };
    }
}
