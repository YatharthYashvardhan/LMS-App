using Microsoft.AspNetCore.Identity;
using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Buisness.IRepository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignUpModel userModel);
        Task<string?> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
        Task<int?> Tokens(string email);
    }
}