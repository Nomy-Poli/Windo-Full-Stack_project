using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var ans = httpContext.Request.Cookies["username"] == "rut@busoft.co.il" || httpContext.Request.Cookies["username"] == "windo.org.il@gmail.com"|| httpContext.Request.Cookies["username"] == "yaeld@busoft.co.il";
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return ans;
                   //httpContext.User.Identity.IsAuthenticated;
        }
    }
}
