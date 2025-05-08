using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YYvMiniProject2.Buisness.IRepository;
using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;


        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel user)
        {
            if (user != null)
            {
                var answer = await _accountRepository.CreateUser(user);

                if (!answer.Succeeded)
                {
                    foreach (var errorMessage in answer.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                    // Return a Bad Request (400) with the ModelState containing error details
                    return BadRequest(ModelState);
                }

                // Return a No Content (204) response for successful sign-up
                return NoContent();
            }

            // Return a Bad Request (400) if the provided user is null
            return BadRequest("Invalid user data");
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login([FromBody] SignInModel user)
        {
            if (ModelState.IsValid)
            {
                var answer = await _accountRepository.PasswordSignInAsync(user);

                if (string.IsNullOrEmpty(answer))
                {
                    return Unauthorized("Invalid credentials");
                }


                var result = new LoginResponseModel
                {
                    Email = user.Email,
                    Token = answer,

            };

                // Return a successful response with the user details and token
                return Ok(result);
            }

            // If ModelState is not valid, return BadRequest (400) with error details
            return BadRequest(ModelState);
        }


        [HttpGet]
        [Route("signout")]
        public async Task<int> Logout()
        {
            await _accountRepository.SignOutAsync();
            return StatusCodes.Status200OK;
        }

        [HttpGet]
        [Route("getBookToken")]
        public async Task<IActionResult> getBookToken(string email)
        {
            var token = await _accountRepository.Tokens(email);
            return Ok(token);
        }
    }
}

