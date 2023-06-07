﻿using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LibraryManagement.Webapp.Services
{
    public class AuthService
    {
        private readonly bool _isDevelopment;
        private readonly LibraryContext _db;
        private readonly ICryptService _crypt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(bool isDevelopment,
            LibraryContext db, ICryptService crypt, IHttpContextAccessor httpContextAccessor)
        {
            _isDevelopment = isDevelopment;
            _db = db;
            _crypt = crypt;
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext => _httpContextAccessor?.HttpContext
            ?? throw new NotSupportedException();

        public async Task<(bool success, string message)> TryLoginAsync(string username, string password)
        {
            var dbUser = _db.Users.FirstOrDefault(u => u.Username == username);
            if (dbUser is null) { return (false, "Unknown username or wrong password."); }
            var passwordHash = _crypt.GenerateHash(dbUser.Salt, password);
            if (!_isDevelopment && passwordHash != dbUser.PasswordHash)
            {
                return (false, "Unknown username or wrong password.");
            }
            var role = dbUser.Usertype.ToString();
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role),
                    //new Claim("Userdata", JsonSerializer.Serialize(currentUser)),
                };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
            };

            await HttpContext.SignInAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return (true, string.Empty);
        }
        public bool IsAuthenticated => HttpContext.User.Identity?.Name != null;
        public string? Username => HttpContext.User.Identity?.Name;
        public bool HasRole(string role) => HttpContext.User.IsInRole(role);
        public bool IsAdmin => HttpContext.User.IsInRole(Usertype.Admin.ToString());
        public Task LogoutAsync() => HttpContext.SignOutAsync();
    }
}
