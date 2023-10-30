namespace ToDosAPI.Models.UserClasses
{
    public interface IUserServices
    {
        Task<String> AddNewUser(User user);
        Task<String> login(String Username, String Password);


    }
}
