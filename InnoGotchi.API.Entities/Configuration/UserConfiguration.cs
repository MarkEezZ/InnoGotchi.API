using InnoGotchi.API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Login = "MarkEezZ",
                    Email = "goog55776@gmail.com",
                    Password = "qwe123",
                    Name = "Mark",
                    Surname = "Lovyagin",
                    Age = 19,
                    AvatarFileName = "ava_default.png",
                    IsInGame = true,
                    IsMusic = true
                },
                new User
                {
                    Id = 2,
                    Login = "Lenon123",
                    Email = "goog55776x2@gmail.com",
                    Password = "asd456",
                    Name = "John",
                    Surname = "Lenon",
                    AvatarFileName = "ava_default.png",
                    IsInGame = true,
                    IsMusic = true
                }
            );
        }
    }
}
