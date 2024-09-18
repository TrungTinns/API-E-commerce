using Microsoft.AspNetCore.Identity;
using T_BookStore.Data;
using T_BookStore.Models;

namespace T_BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public Task<string> SignInAsync(SignInModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            throw new NotImplementedException();
        }
    }
}
