using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static SportApplication.Infrastructure.Enumerations;

// **

namespace SportApplication.Data.Models
{
    public class User : Entity
    {
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        public string HashedPassword { get; set; } = string.Empty;

        public UserGender Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<Role> Roles { get; set; } 
            = new List<Role>();
        
        public List<Participation> Participations { get; set; } = new List<Participation>();
    }
}
