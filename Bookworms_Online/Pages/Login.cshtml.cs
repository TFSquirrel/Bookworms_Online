using Bookworms_Online.ViewModels;
using Bookworms_Online.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms_Online.Services;

namespace Bookworms_Online.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<Member> signInManager;
        private readonly AuditLogService auditLogService;
        public LoginModel(SignInManager<Member> signInManager, AuditLogService auditLogService)
		{
			this.signInManager = signInManager;
            this.auditLogService = auditLogService;
        }
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
               LModel.RememberMe, false);
                if (identityResult != null)
				{
					if (identityResult.Succeeded)
					{
						return RedirectToPage("Index");
					}
					else if (identityResult.IsLockedOut)
					{
						return RedirectToPage("/Register");
					}
				}
                ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
	}
}
