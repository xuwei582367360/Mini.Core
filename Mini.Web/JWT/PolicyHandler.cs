using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Mini.Extensions.ServiceExtensions.Jwt;
using Mini.IServives;
using Mini.Model.Entity;
using Mini.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mini.Web.JWT
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        /// <summary>
        /// jwt 服务
        /// </summary>
        private readonly IJwtAppService _jwtApp;
        private readonly IMenuAuthorizeServices _menuAuthorizeServices;
        private readonly IMenuInfoServices _menuInfoServices;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="jwtApp"></param>
        public PolicyHandler(IDistributedCache cache, IAuthenticationSchemeProvider schemes, IJwtAppService jwtApp, IMenuAuthorizeServices menuAuthorizeServices, IMenuInfoServices menuInfoServices)
        {
            Schemes = schemes;
            _jwtApp = jwtApp;
            _menuAuthorizeServices = menuAuthorizeServices;
            _menuInfoServices = menuInfoServices;
            _cache = cache;
        }

        //授权处理
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            try
            {
                var httpContext = ((Microsoft.AspNetCore.Http.DefaultHttpContext)context.Resource).HttpContext;
                ////控制器白名单，在该名单中的控制器，需要登录，但不需要授权
                //var whiteController = new[] { "usersession", "home", "redirects" };
                //if (whiteController.Contains(httpContext.Request.Path.Value))
                //{
                //    return;
                //}
                //获取授权方式
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    //验证签发的用户信息
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result.Succeeded)
                    {
                        //判断是否为已停用的 Token
                        if (!await _jwtApp.IsCurrentActiveTokenAsync())
                        {
                            context.Fail();
                            return;
                        }

                        httpContext.User = result.Principal;
                        var url = httpContext.Request.Path.Value.ToLower();
                        var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;
                        var userId = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Actor).FirstOrDefault().Value;
                        var name = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                        HasMenu(context, role, url);

                        //判断是否过期
                        if (DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration).Value) >= DateTime.UtcNow)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                        }
                        return;
                    }
                    else
                    {
                        //当前端未传token时去cookie里面取
                        var token = _jwtApp.GetCurrentAsync();
                        if (string.IsNullOrWhiteSpace(token))
                        {
                            context.Fail();
                            return;
                        }
                        else
                        {
                            string userInfo = await _cache.GetStringAsync(_jwtApp.GetKey(token));
                            if (userInfo == null)
                            {
                                context.Fail();
                                return;
                            }
                            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(userInfo);
                            var role = user.Role;
                            var userId = user.UserId.ToString();
                            var name = user.UserName;
                            var url = httpContext.Request.Path.Value.ToLower();
                            HasMenu(context, role, url);
                            context.Succeed(requirement);
                            return;
                        }
                    }
                }
                context.Fail();
            }
            catch (Exception exc)
            {
                context.Fail();
            }
        }


        public async void HasMenu(AuthorizationHandlerContext context, string role, string url)
        {
            //获取角色对应的菜单权限
            List<Menu> menuList = new List<Menu>();
            List<MenuAuthorize> menuAuthorizeList = await _menuAuthorizeServices.Query(x => role.Contains(x.Authorized));
            menuAuthorizeList.ForEach(x =>
            {
                MenuInfo menuInfo = _menuInfoServices.Query(y => y.MenuId == x.MeunId).Result.FirstOrDefault();
                menuList.Add(new Menu { Role = x.Authorized, Url = menuInfo.MenuURL });
            });
            //判断角色与 Url 是否对应(一个用户多个角色情况)
            var menu = menuList.Where(x => role.Contains(x.Role) && url.Equals((x.Url?.ToLower()))).FirstOrDefault();

            if (menu == null)
            {
                context.Fail();
                return;
            }
        }
        /// <summary>
        /// 测试菜单类
        /// </summary>
        public class Menu
        {
            public string Role { get; set; }

            public string Url { get; set; }
        }
    }
}
