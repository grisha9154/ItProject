using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Models.ManageViewModels
{
    public static class ExtensionManager
    {
        public static Task<IdentityResult> SetFirstNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string firstName)
        {
            return Task.Run(() =>
            {
                user.FirstName = firstName;
                return userManager.UpdateAsync(user);
            });
        }
        public static Task<IdentityResult> SetLastNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string lastName)
        {
            return Task.Run(() =>
            {
                user.LastName = lastName;
                return userManager.UpdateAsync(user);
            });
        }
        public static Task<IdentityResult> SetCountryAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string country)
        {
            return Task.Run(() =>
            {
                user.Country = country;
                return userManager.UpdateAsync(user);
            });
        }

        public static Task<IdentityResult> SetCityAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string city)
        {
            return Task.Run(() =>
            {
                user.City = city;
                return userManager.UpdateAsync(user);
            });
        }

        public static Task<IdentityResult> SetCompanyAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string company)
        {
            return Task.Run(() =>
            {
                user.Company = company;
                return userManager.UpdateAsync(user);
            });
        }
    }
}
