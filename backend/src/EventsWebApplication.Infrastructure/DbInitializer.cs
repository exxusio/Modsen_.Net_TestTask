using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Consts;

namespace EventsWebApplication.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext appDbcontext)
        {
            await appDbcontext.Database.MigrateAsync();
        }

        public static async Task SeedAsync(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            var userRepository = unitOfWork.GetRepository<IUserRepository, User>();
            var roleRepository = unitOfWork.GetRepository<IRoleRepository, Role>();

            var adminRole = await roleRepository.GetByNameAsync(BaseRoles.Admin);
            if (adminRole == null)
            {
                adminRole = new Role { Name = BaseRoles.Admin };
                await roleRepository.AddAsync(adminRole);

                var hashPassword = passwordHasher.HashPassword("admin");
                var adminUser = new User
                {
                    Login = "admin",
                    HashPassword = hashPassword,
                    FirstName = "admin",
                    LastName = "admin",
                    DateOfBirth = new DateTime(2001, 1, 1),
                    Email = "admin@mail.ru",
                    RoleId = adminRole.Id
                };

                await userRepository.AddAsync(adminUser);
            }

            var userRole = await roleRepository.GetByNameAsync(BaseRoles.User);
            if (userRole == null)
            {
                userRole = new Role { Name = BaseRoles.User };
                await roleRepository.AddAsync(userRole);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}