using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Mini.Extensions.ServiceExtensions.Jwt;
using Mini.Extensions.ServiceExtensions.Jwt.Dto;
using Mini.IServives;
using Mini.Model;
using Mini.Model.Entity;
using Mini.Model.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mini.Common;
using Mini.Common.Helper;
using Microsoft.AspNetCore.Http;

namespace Mini.Web.Controllers
{
    
    public class HomeController : BaseController
    {

        private readonly IUserInfoServices _userInfoServices;
        private readonly IMenuInfoServices _menuInfoServices;
        private readonly IRoleInfoServices _roleInfoServices;
        private readonly IJwtAppService _JwtAppService;
        private readonly IDistributedCache _cache;
        public HomeController(IUserInfoServices userInfoServices,
                              IMenuInfoServices menuInfoServices,
                              IJwtAppService jwtAppServices,
                              IRoleInfoServices roleInfoServices,
                              IDistributedCache cache) : base(jwtAppServices)
        {
            _userInfoServices = userInfoServices;
            _menuInfoServices = menuInfoServices;
            _roleInfoServices = roleInfoServices;
            _JwtAppService = jwtAppServices;
            _cache = cache;
        }


        public async Task<IActionResult> MainIndex(string userName)
        {
            if (!userName.IsNotEmptyOrNull())
            {
                //跳转到登录页面（测试）
                return View(nameof(Login));
            }
            //获取所有启用的菜单
            List<MenuInfo> menuInfos = await _menuInfoServices.Query(x => x.MenuStatus == (int)StatusEnum.Yes);
            ViewBag.MenuList = menuInfos;
            return View();
        }

        [HttpGet]
        public IActionResult Skin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<JwtResponseDto>> LoginJson(string userName = "", string passWord = "")
        {
            var userinfo = new UserInfo { UserName = userName, UserPwd = passWord };
            MessageModel<UserInfo> message = await _userInfoServices.CheckLogin(userinfo);
            var user = message.response;
            var jwt = _JwtAppService.Create(user);
            var jwtresponse = new JwtResponseDto
            {
                Access = jwt.Token,
                Type = "Bearer",
                Profile = new Profile
                {
                    Name = user.UserName,
                    Auths = jwt.Auths,
                    Expires = jwt.Expires
                }
            };
            Response.Cookies.Append("X-Token", jwtresponse.Access, new CookieOptions
            {
                HttpOnly = false,
                Expires = jwt.Expires
            });
            return new MessageModel<JwtResponseDto> { msg = message.msg, status = message.status, response = jwtresponse };
        }

        [Authorize(Policy = "Permission")]
        [HttpGet]
        public async Task<IActionResult> LoginOff()
        {
            #region 退出系统
            //移除缓存
            #endregion
            return View(nameof(Login));
        }
    }
}
