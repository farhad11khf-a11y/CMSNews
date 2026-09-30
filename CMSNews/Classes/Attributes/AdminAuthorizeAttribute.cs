using CMSNews.Models.Context;
using CMSNews.Service.Service;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace CMSNews.Classes.Attributes
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(
            HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            string mobileNumber =
                httpContext.User.Identity.Name;

            DbCMSNewsContext db = new DbCMSNewsContext();
            UserService userService = new UserService(db);

            var user = userService.GetAll()
                .FirstOrDefault(u => u.MobileNumber == mobileNumber);

            if (user == null)
            {
                return false;
            }

            if (!user.IsActive)
            {
                return false;
            }

            if (user.Role != "Admin")
            {
                return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(
            AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result =
                    new RedirectResult("/Admin/Account/Login");

                return;
            }

            filterContext.Result =
                new HttpStatusCodeResult(403);
        }
    }
}