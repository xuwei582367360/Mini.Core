using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Model.Entity
{
    [SugarTable("用户信息")]
    public class UserInformation
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int 序号 { get; set; }
        public string 用户代码 { get; set; }
        public string 用户姓名 { get; set; }
        public string 登录代码 { get; set; }
        public string 登录密码 { get; set; }
        public string 密码确认 { get; set; }
        public DateTime? 最后登录时间 { get; set; }
        public string 部门 { get; set; }
        public string 职位 { get; set; }
        public string 是否在线 { get; set; }
        public int lastchatinfo { get; set; }
        public string AuthCode { get; set; }
        public DateTime? InvalidTime { get; set; }
    }
}
