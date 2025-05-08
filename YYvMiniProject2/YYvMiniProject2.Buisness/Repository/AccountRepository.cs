using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YYvMiniProject2.Buisness.IRepository;
using YYvMiniProject2.Data.DbContext;
using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Buisness.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IdentityResult> CreateUser(SignUpModel userModel)
        {
            var user = new ApplicationUser()
            {
                FullName = userModel.FullName,
                Email = userModel.Email,
                UserName = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                bookToken = 5
            };
            var answer = await _userManager.CreateAsync(user, userModel.Password);
            return answer;
        }
        public async Task<string?> PasswordSignInAsync(SignInModel signInModel)
        {

            var answer = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RemberMe, false);
            if (!answer.Succeeded)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(signInModel.Email);

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email , signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(60),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int?> Tokens(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return user.bookToken;
            }
            return null;
        }

    }
}
