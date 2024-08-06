using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.API.Data
{
    public class AuthDBContext : IdentityDbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Create Reader and Writer Role

            var ReaderRoleId = "cefb32eb-f95c-4c6a-be0f-d5b19c76e5c7";
            var WriterRoleId = "c5a6e98a-8dda-4982-92bc-b56897bfd998";

            var Role = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = ReaderRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = ReaderRoleId
                },
                new IdentityRole()
                {
                    Id = WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = WriterRoleId
                }
            };

            //Seed Roles
            builder.Entity<IdentityRole>().HasData(Role);

            //Create Admin User
            var adminUserId = "c3fcd40a-2cb0-4419-a13f-5c162306bc56";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@vignez.in",
                Email = "admin@vignez.in",
                NormalizedEmail = "admin@vignez.in".ToUpper(),
                NormalizedUserName = "admin@vignez.in".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to Admin
            var AdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = ReaderRoleId
                },
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = WriterRoleId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(AdminRoles);
        }
    }
}
