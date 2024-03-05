using Api.Models.Entities;

namespace Api.Models.Dtos
{
    public class EditProfileByAdminDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new List<Role>();
    }

}
