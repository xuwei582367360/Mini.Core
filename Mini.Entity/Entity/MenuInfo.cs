using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Model.Entity
{
    [SugarTable("MenuInfo")]
    public class MenuInfo
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = false)]
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuSort { get; set; }
        public int MenuType { get; set; }
        public string ParentMenuId { get; set; }
        public int MenuStatus { get; set; }
        public string Authorize { get; set; }
        public string MenuURL { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }

    }
}
