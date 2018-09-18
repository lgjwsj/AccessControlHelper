﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccessControlDemo.Controllers
{
    public class AccountController : Controller
    {
        [ActionName("Login")]
        public async Task<string> LoginAsync()
        {
            var u = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "testuser") }, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, u, new AuthenticationProperties { IsPersistent = true, AllowRefresh = true });
            return "Login success";
        }

        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "Logout success";
        }

        [Authorize("AccessControl")]
        public string Index()
          => "test";
    }
}