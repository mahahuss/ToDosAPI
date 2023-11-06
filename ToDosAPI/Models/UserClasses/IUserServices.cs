namespace ToDosAPI.Models.UserClasses
{
    public interface IUserServices
    {
        Task<bool> AddNewUser(User user);
        Task<string?> Login(String username, String password);


    }
}
