using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        public string Login { get; set; }

        [EmailAddress(ErrorMessage = "Incorrect adress")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
