using Bookworms_Online.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Bookworms_Online.Model;
using Bookworms_Online.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Web;
using Bookworms_Online.Services;

namespace Bookworms_Online.Pages
{


    public class EncryptionHelper
    {
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
        public class RegisterModel : PageModel
        {

            private UserManager<Member> userManager { get; }
            private SignInManager<Member> signInManager { get; }
            
            private readonly AuditLogService auditLogService;

        [BindProperty]
            public Register RModel { get; set; }

            public RegisterModel(UserManager<Member> userManager,
            SignInManager<Member> signInManager, AuditLogService auditLogService)
            {
                this.userManager = userManager;
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

                var member = new Member
                    {
                        FirstName = RModel.FirstName,
                        LastName = RModel.LastName,
                        Shipping = HttpUtility.HtmlEncode(RModel.Shipping),
                        Billing = HttpUtility.HtmlEncode(RModel.Billing),
                        MobileNo = RModel.MobileNo,
                        CreditCard = RModel.CreditCard,
                        Email = RModel.Email,
                        UserName = RModel.Email,
                        Password = RModel.Password
                    };


                if (RModel.File != null && RModel.File.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg" };
                    var fileExtension = Path.GetExtension(RModel.File.FileName).ToLower();

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await RModel.File.CopyToAsync(memoryStream);
                            member.File = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid file format. Allowed format is JPG only.");
                        return Page();
                    }
                }

                var result = await userManager.CreateAsync(member, RModel.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(member, false);
                        auditLogService.LogActivity(member.Id, "User Created", $"User: {member.UserName}");
                        return RedirectToPage("Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return Page();
            }

        }
    
}
