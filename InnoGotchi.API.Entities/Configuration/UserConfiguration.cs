﻿using InnoGotchi.API.Entities.Models;
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
                    SettingsId = 1
                },
                new User
                {
                    Id = 2,
                    Login = "Lenon123",
                    Email = "goog55776x2@gmail.com",
                    Password = "asd456",
                    Name = "John",
                    Surname = "Lenon",
                    LastEntry = new DateTime(2022, 11, 9, 18, 22, 24), // год - месяц - день - час - минута - секунда
                    LastExit = new DateTime(2022, 11, 9, 19, 42, 34), // год - месяц - день - час - минута - секунда
                    SettingsId = 1                
                }
            );
        }
    }
}
