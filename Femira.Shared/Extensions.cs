using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Femira.Shared
{
     public static class Extensions
    {
        public static int GetUserId(this ClaimsPrincipal principal) =>
            Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
