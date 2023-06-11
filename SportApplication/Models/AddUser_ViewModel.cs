using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static SportApplication.Infrastructure.Enumerations;

namespace SportApplication.Models
{
    public class AddUser_ViewModel
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public UserGender? Gender { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
