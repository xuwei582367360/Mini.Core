using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini.IServives;
using Mini.Model;
using Mini.Model.Entity;
using Mini.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserInfoServices _userInfoServices;

        public HomeController(IUserInfoServices userInfoServices) 
        {
            _userInfoServices = userInfoServices;
        }

        [HttpGet]
        public async Task<PageModel<UserInfo>> GetAll() 
        {
            return await _userInfoServices.QueryPage(null, 1, 10, "");
        }
    }
}
