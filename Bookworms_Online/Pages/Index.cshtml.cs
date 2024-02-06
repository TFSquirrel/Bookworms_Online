using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookworms_Online.Model;
using System.Web;

namespace Bookworms_Online.Pages
{
	[Authorize]
	public class IndexModel : PageModel
    {

        private readonly IHttpContextAccessor contxt;
        private readonly UserManager<Member> userManager;
        public IndexModel(IHttpContextAccessor httpContextAccessor, UserManager<Member> userManager)
        {
            contxt = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return NotFound();
            }

            ViewData["UserName"] = currentUser.UserName;
            ViewData["Email"] = currentUser.Email;
            ViewData["LastName"] = currentUser.LastName;
            ViewData["FirstName"] = currentUser.FirstName;
            ViewData["Shipping"] = HttpUtility.HtmlDecode(currentUser.Shipping);
            ViewData["Billing"] = HttpUtility.HtmlDecode(currentUser.Billing);
            ViewData["MobileNo"] = currentUser.MobileNo;
            ViewData["CreditCard"] = currentUser.CreditCard;
            ViewData["Password"] = currentUser.Password;
            ViewData["File"] = currentUser.File;

            return Page();
        }

    }
}