using System.ComponentModel.DataAnnotations;

namespace HEMSystems.Services.KhoaHLD.DTOs
{
    public class GetUserAccountRequest
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "UserName must be between 1 and 150 characters.")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Password must be between 1 and 100 characters.")]
        public string Password { get; set; } = default!;
    }
}
