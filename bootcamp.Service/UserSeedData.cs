using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.Repository.Identities;
using Microsoft.AspNetCore.Identity;

namespace bootcamp.Service
{
    public class UserSeedData
    {
        public static async Task Seed(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "admin" });
            }

            var editorRole = await roleManager.FindByNameAsync("editor");
            if (editorRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "editor" });
                editorRole = await roleManager.FindByNameAsync("editor");
            }


            var editorRoleClaim = await roleManager.GetClaimsAsync(editorRole!);

            if (!editorRoleClaim.Any())
            {
                await roleManager.AddClaimAsync(editorRole!, new Claim("update", "true"));
                await roleManager.AddClaimAsync(editorRole!, new Claim("delete", "true"));
            }


            var user = userManager.Users.FirstOrDefault();


            if (!await userManager.IsInRoleAsync(user, "editor"))
            {
                await userManager.AddToRoleAsync(user, "editor");
            }
        }
    }
}