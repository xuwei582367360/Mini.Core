using Microsoft.AspNetCore.Mvc;
using Mini.Common.Helper;
using Mini.Extensions.Authorization;
using Mini.Extensions.ServiceExtensions.Jwt;
using Mini.Model;
using Mini.Model.Entity;

namespace Mini.Web.Controllers
{
    public class BaseController : Controller
    {
        public TokenModel TokenModel { get; set; }

        public IJwtAppService _jwtAppServices;
        public BaseController(IJwtAppService jwtAppServices)
        {
            _jwtAppServices = jwtAppServices;
            GetUserInformation();
        }

        /// <summary>
        /// 解析token
        /// </summary>
        public void GetUserInformation()
        {
            string token = _jwtAppServices.GetCurrentAsync();
            if (token.IsNotEmptyOrNull())
            {
                //解析token
                UserInfo userInfo = JwtHelper.SerializeJwt(token);
            }
            else
            {
                TokenModel = new TokenModel();
            }
        }
    }
}
