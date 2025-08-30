﻿using InvoiceApp.API.Entities.Enums;
using InvoiceApp.API.Entities.Indenties;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.API.DAL
{
    public static class AppDbContextSeed
    {
        public static async Task SeedDatabaseAsync(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(EUserRole)).Cast<EUserRole>().Select(x => x.ToString()))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminExists = await userManager.FindByNameAsync("admin");

            if (adminExists == null)
            {
                var userAdmin = new User 
                { 
                    UserName = "admin", 
                    Email = "admin@admin.com", 
                    EmailConfirmed = true 
                };

                await userManager.CreateAsync(userAdmin, "!Admin123.?");
                await userManager.AddToRoleAsync(userAdmin, EUserRole.Admin.ToString());
            }

            await context.SaveChangesAsync();
        }
    }
}