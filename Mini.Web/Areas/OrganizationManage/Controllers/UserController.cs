using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini.Extensions.ServiceExtensions.Jwt;
using Mini.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini.Web.Areas.OrganizationManage.Controllers
{
    [Authorize(Policy = "Permission")]
    [Area("OrganizationManage")]
    public class UserController : BaseController
    {

        public UserController(IJwtAppService jwtAppServices) : base(jwtAppServices)
        {

        }
        public IActionResult UserIndex()
        {
            return View();
        }
    }
}
