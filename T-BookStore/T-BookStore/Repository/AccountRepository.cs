using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T_BookStore.Data;
using T_BookStore.Helpers;
using T_BookStore.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace T_BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if(user == null || !passwordValid)
            {
                return String.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //Check Customer role is exist
                if(!await roleManager.RoleExistsAsync(ApplicationRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(ApplicationRole.Customer));
                }

                await userManager.AddToRoleAsync(user, ApplicationRole.Customer);
            }

            return result;
        }
    }
}
