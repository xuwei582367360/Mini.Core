using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Model.Entity
{
    [SugarTable("RoleInfo")]
    public class RoleInfo
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int RoleId { get; set; }
        public string RoleGuid { get; set; }
        public string RoleName { get; set; }
        public int RoleSort { get; set; }
        public int RoleStatus { get; set; }

    }
}
