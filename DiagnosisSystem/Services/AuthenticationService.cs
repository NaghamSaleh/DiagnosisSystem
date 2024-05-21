using DiagnosisSystem.Services.Interfaces;

namespace DiagnosisSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IList<string>> SignInAsync(string email, string password)
        {
            await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles;
            }
            return null;
        }

        
    }

}
