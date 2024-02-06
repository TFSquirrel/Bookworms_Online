using Microsoft.AspNetCore.Identity;

namespace Bookworms_Online.Model
{
	public class Member : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Shipping { get; set; }
		public string Billing { get; set; }
		public string MobileNo { get; set; }
        public new string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }
        public string Email { get; set; }
		public string CreditCard { get; set; }
		public string Password { get; set; }
		public byte[] File { get; set; }
	}
}
