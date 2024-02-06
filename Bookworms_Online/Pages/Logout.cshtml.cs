using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms_Online.Model;
using Bookworms_Online.Services;

namespace Bookworms_Online.Pages
{
    public class LogoutModel : PageModel
    {
			private readonly SignInManager<Member> signInManager;
			private readonly AuditLogService auditLogService;
			public LogoutModel(SignInManager<Member> signInManager, AuditLogService auditLogService)
			{
				this.signInManager = signInManager;
				this.auditLogService = auditLogService;
			}
			public void OnGet() { }
			public async Task<IActionResult> OnPostLogoutAsync()
			{
				var user = await signInManager.UserManager.GetUserAsync(User);
				auditLogService.LogActivity(user.Id, "User Logout", $"User: {user.UserName}");
				await signInManager.SignOutAsync();
				return RedirectToPage("Login");
			}
			public async Task<IActionResult> OnPostDontLogoutAsync()
			{
				return RedirectToPage("Index");
			}
	}
}
