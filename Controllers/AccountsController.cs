using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;
using Bookly.APIs.Error;
using Bookly.APIs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Bookly.APIs.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "Email Already Exists"));

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });



        }


        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<ProfileDto>> GetProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new ProfileDto()
            {
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Email = email,
                UserName = user.UserName,
                JoinedDate = user.JoinedDate
            });
        }


        [Authorize]
        [HttpPut("profile")]
        public async Task<ActionResult<ActionDoneSuccessfullyMessageDto>> EditProfile(UpdatedUserDto model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            user.DisplayName = model.DisplayName;
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);
            return Ok(new ActionDoneSuccessfullyMessageDto("Profile was updated successfully"));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("roles")]
        public async Task<ActionResult<string>> AssignRole(AssignRoleDto model)
        {
            AppUser user = null;
            if (!string.IsNullOrEmpty(model.Id))
                user = await _userManager.FindByIdAsync(model.Id);

            if (!string.IsNullOrEmpty(model.Email))
                user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            if (!await _roleManager.RoleExistsAsync(model.Role))
                return BadRequest(new ApiResponse(400, "Role is not existed"));

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok($"Role {model.Role} is assigned to the user");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<ActionResult<RoleDto>> GetRoles(BaseRoleDto model)
        {
            AppUser user = null;
            if (!string.IsNullOrEmpty(model.Id))
                user = await _userManager.FindByIdAsync(model.Id);

            if (!string.IsNullOrEmpty(model.Email))
                user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new RoleDto()
            {
                Email = user.Email,
                Id = user.Id,
                Roles = roles
            });

        }



        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }



    }
}
