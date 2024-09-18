using Microsoft.AspNetCore.Identity;
using T_BookStore.Models;

namespace T_BookStore.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<String> SignInAsync(SignInModel model);
    }
}
