using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A Name field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Login is 50.")]
        [MinLength(5, ErrorMessage = "Minimum length for the Login is 5.")]
        public string Login { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "An Email field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Email is 100.")]
        [MinLength(5, ErrorMessage = "Minimum length for the Email is 5.")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50.")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length for the Surname is 50.")]
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string AvatarFileName { get; set; }
        public bool IsInGame { get; set; }
        public bool IsMusic { get; set; }
    }
}
