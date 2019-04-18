namespace PrivateSchoolExe.Migrations
{
    using PrivateSchoolExe.Data;
    using PrivateSchoolExe.Helpers;
    using PrivateSchoolExe.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<PrivateSchoolExe.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        Authentication auth = new Authentication();
        protected override void Seed(PrivateSchoolExe.Data.DataContext context)
        {
            context.Levels.AddOrUpdate(x => x.Id,
                 new Level() { Id = 1, Title = "Student", Access = 1 },
                 new Level() { Id = 2, Title = "Trainer", Access = 2 },
                 new Level() { Id = 3, Title = "HeadMaster", Access = 3 }
                  );

            context.Courses.AddOrUpdate(x => x.Id,
                new Course()
                {
                    Id = 1,
                    Title = "Classes",
                    Stream = "C#",
                    Type = "Full",
                    StartDate = new DateTime(2019, 05, 05),
                    EndDate = new DateTime(2019, 08, 05),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 1,
                            Description = "Prepare Assignment with Chatterbot system",
                            Title = "ChatterBots",
                            Submission = new DateTime(2019, 6, 22)
                       },
                       new Assignment()
                       {
                           Id = 2,
                            Description = "Prepare Assignment with Bootstrap Framework",
                            Title = "Framework",
                            Submission = new DateTime(2019, 8, 17)
                       },
                       new Assignment()
                       {
                           Id = 3,
                            Description = "Prepare Assignment with .Net framework",
                            Title = "System windows",
                            Submission = new DateTime(2019, 12, 15)
                       }

                    }
                    
                },
                new Course()
                {
                    Id = 2,
                    Title = "MySQL",
                    Stream = "Java",
                    Type = "Full",
                    StartDate = new DateTime(2019, 06, 13),
                    EndDate = new DateTime(2019, 06, 13),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 4,
                            Description = "Prepare Assignment with Bulma",
                            Title = "Bulma",
                            Submission = new DateTime(2019, 6, 22)
                       },
                       new Assignment()
                       {
                          Id = 5,
                            Description = "Prepare Assignment with Methods",
                            Title = "Sudoku",
                            Submission = new DateTime(2019, 7, 17)
                       }
                    }                 
                },
                new Course()
                {
                    Id = 3,
                    Title = "Methods",
                    Stream = "Java",
                    Type = "Full",
                    StartDate = new DateTime(2019, 04, 22),
                    EndDate = new DateTime(2019, 07, 22),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 6,
                            Description = "Prepare Assignment with Repositories",
                            Title = "Repository build",
                            Submission = new DateTime(2019, 6, 22)
                       },
                       new Assignment()
                       {
                         Id = 7,
                              Description = "Prepare Assignment with Linq",
                              Title = "String alignment",
                              Submission = new DateTime(2019, 8, 01)
                       }
                    }
                 
                },
                new Course()
                {
                    Id = 4,
                    Title = "Errors",
                    Stream = "C#",
                    Type = "Full",
                    StartDate = new DateTime(2019, 05, 09),
                    EndDate = new DateTime(2019, 08, 15),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 8,
                            Description = "Prepare Assignment with Try/Catch",
                            Title = "Error Handling",
                            Submission = new DateTime(2019, 6, 13)
                       },
                       new Assignment()
                       {
                         Id = 9,
                              Description = "Prepare Assignment with Error Interceptor",
                              Title = "Interceptors",
                              Submission = new DateTime(2019, 7, 10)
                       },
                       new Assignment()
                       {
                         Id = 10,
                              Description = "Prepare Assignment with Libraries",
                              Title = "NPM",
                              Submission = new DateTime(2019, 8 , 08)
                       }
                    }

                },
                new Course()
                {
                    Id = 5,
                    Title = "Interfaces",
                    Stream = "C#",
                    Type = "Part",
                    StartDate = new DateTime(2019, 05, 20),
                    EndDate = new DateTime(2019, 11, 15),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 11,
                            Description = "Prepare Assignment with Systems",
                            Title = "Inheritance",
                            Submission = new DateTime(2019, 10, 28)
                       },
                       new Assignment()
                       {
                         Id = 12,
                              Description = "Prepare Assignment with ICollection",
                              Title = "Promotes",
                              Submission = new DateTime(2019, 8, 10)
                       }
                    }

                },
                new Course()
                {
                    Id = 6,
                    Title = "Tomcat",
                    Stream = "Java",
                    Type = "Part",
                    StartDate = new DateTime(2019, 06, 22),
                    EndDate = new DateTime(2019, 12, 07),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 13,
                            Description = "Prepare Assignment with Servlets ",
                            Title = "Hibernate",
                            Submission = new DateTime(2019, 11, 24)
                       },
                       new Assignment()
                       {
                         Id = 14,
                              Description = "Prepare Assignment with AOP",
                              Title = "JPA",
                              Submission = new DateTime(2019, 9, 10)
                       }
                    }

                },
                new Course()
                {
                    Id = 7,
                    Title = "ASP.NET",
                    Stream = "C#",
                    Type = "Part",
                    StartDate = new DateTime(2019, 07, 25),
                    EndDate = new DateTime(2019, 12, 17),
                    Assignments = new List<Assignment>()
                    {
                       new Assignment()
                       {
                           Id = 15,
                            Description = "Prepare Assignment with ADO.NET",
                            Title = "Entity MVC",
                            Submission = new DateTime(2019, 8, 28)
                       },
                       new Assignment()
                       {
                         Id = 16,
                              Description = "Prepare Assignment with Dapper",
                              Title = "Mapper",
                              Submission = new DateTime(2019, 12, 10)
                       }
                    }

                }
                );

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("password", out passwordHash, out passwordSalt);
            context.Users.AddOrUpdate(x => x.Id,
                //Students
            new Model.User()
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Stark",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "1",
                LevelId = 1,
                DateOfBirth = new DateTime(1988, 07, 22),

            },
            new Model.User()
            {
                Id = 2,
                FirstName = "Nick",
                LastName = "Fury",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "2",
                LevelId = 1,
                DateOfBirth = new DateTime(1970, 11, 11),

            },
            new Model.User()
            {
                Id = 3,
                FirstName = "Thor",
                LastName = "Odinson",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "3",
                LevelId = 1,
                DateOfBirth = new DateTime(1985, 02, 17),

            },
            new Model.User()
            {
                Id = 4,
                FirstName = "Bruce",
                LastName = "Banner",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "4",
                LevelId = 1,
                DateOfBirth = new DateTime(1987, 10, 19),

            },
            new Model.User()
            {
                Id = 5,
                FirstName = "Steve",
                LastName = "Rogers",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "5",
                LevelId = 1,
                DateOfBirth = new DateTime(1991, 08, 26),

            },
            new Model.User()
            {
                Id = 6,
                FirstName = "Wanda",
                LastName = "Maximoff",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = "6",
                LevelId = 1,
                DateOfBirth = new DateTime(1993, 01, 05),

            },
            new Model.User()
            {
                Id = 7,
                FirstName = "Peter",
                LastName = "Parker",
                Email = "7",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 1,
                DateOfBirth = new DateTime(1990, 06, 20),               
            },
                //Trainers
            new Model.User()
            {
                Id = 8,
                FirstName = "Clark",
                LastName = "Kent",
                Email = "8",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1978, 04, 17),
                Subject = "HTML5",

            },
            new Model.User()
            {
                Id = 9,
                FirstName = "Bruce",
                LastName = "Wayne",
                Email = "9",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1969, 10, 27),
                Subject = "Bootstrap"
            },
            new Model.User()
            {
                Id = 10,
                FirstName = "Barry",
                LastName = "Allen",
                Email = "10",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1985, 04, 22),
                Subject = "Methods"
            },
            new Model.User()
            {
                Id = 11,
                FirstName = "Diana",
                LastName = "Prince",
                Email = "11",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1983, 03, 19),
                Subject = "Interfaces"
            },
            new Model.User()
            {
                Id = 12,
                FirstName = "Arthur",
                LastName = "Curry",
                Email = "12",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1975, 12, 12),
                Subject = "Javascript"
            },
            new Model.User()
            {
                Id = 13,
                FirstName = "Victor",
                LastName = "Stone",
                Email = "13",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 2,
                DateOfBirth = new DateTime(1989, 02, 24),
                Subject = "Typescript"
            },
            //HeadMaster
            new Model.User()
            {
                Id = 14,
                FirstName = "Stan",
                LastName = "Lee",
                Email = "14",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LevelId = 3,
                DateOfBirth = new DateTime(1960, 05, 21),
            }
            );

           

        }


        
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }


        }
    }
}
