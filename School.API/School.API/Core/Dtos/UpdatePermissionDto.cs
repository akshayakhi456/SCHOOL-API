using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Dtos
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string? role { get; set; }


    }
}
