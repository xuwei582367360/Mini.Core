using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini.Extensions.ServiceExtensions.Jwt;
using Mini.IServives;
using Mini.Model;
using Mini.Model.Entity;
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
        private readonly IUserInfoServices _userInfoServices;

        public UserController(IJwtAppService jwtAppServices, IUserInfoServices userInfoServices) : base(jwtAppServices)
        {
            _userInfoServices = userInfoServices;
        }
        public IActionResult UserIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<UserInfo>> GetUserList()
        {
            var entityList = await _userInfoServices.QueryPage(null,1,10,null);
            return entityList;
        }
    }
}
