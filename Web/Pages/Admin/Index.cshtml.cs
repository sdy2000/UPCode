using Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
