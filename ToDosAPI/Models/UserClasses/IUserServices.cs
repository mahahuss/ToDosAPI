namespace ToDosAPI.Models.UserClasses
{
    public interface IUserServices
    {
        Task<bool> AddNewUser(User user);
        Task<String> Login(String username, String password);


    }
}
