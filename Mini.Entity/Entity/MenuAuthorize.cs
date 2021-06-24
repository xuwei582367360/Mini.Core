using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Model.Entity
{
    [SugarTable("MenuAuthorize")]
    public class MenuAuthorize
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string MeunId { get; set; }
        public string Authorized { get; set; }
        public int AuthorizeType { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }

    }
}
