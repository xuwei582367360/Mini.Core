using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mini.Common;
using Mini.Common.DB;
using Mini.Model;
using Mini.Model.Seed;
using SqlSugar;
using SqlSugar.Extensions;
namespace Mini.CoreApi.Controllers.DBFirst
{

    [Route("api/[controller]")]
    [ApiController]
    public class DBFirstController : ControllerBase
    {
        private readonly SqlSugarClient _sqlSugarClient;
        private readonly IWebHostEnvironment Env;

        public DBFirstController(IWebHostEnvironment env, ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient as SqlSugarClient;
            Env = env;
        }

        /// <summary>
        /// 获取实体(需指定表名和数据库)
        /// </summary>
        /// <param name="ConnID">数据库链接名称</param>
        /// <param name="tableNames">需要生成的表名</param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> GetFrameFilesByTableNamesForEntity([FromBody] string[] tableNames, [FromQuery] string ConnID = null)
        {
            ConnID = ConnID == null ? MainDb.CurrentDbConnId.ToLower() : ConnID;

            var isMuti = Appsettings.app(new string[] { "MutiDBEnabled" }).ObjToBool();
            var data = new MessageModel<string>() { success = true, msg = "" };
            if (Env.IsDevelopment())
            {
                data.response += $"库{ConnID}-Models层生成：{FrameSeed.CreateModels(_sqlSugarClient, ConnID, isMuti, tableNames)}";
            }
            else
            {
                data.success = false;
                data.msg = "当前不处于开发模式，代码生成不可用！";
            }
            return data;
        }
    }
}
