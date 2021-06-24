using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini.Model.Entity
{
    [SugarTable("Customer")]
    public class Customer
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Cid { get; set; }
        public int Fid { get; set; }
        public string Cname { get; set; }
        public string Curl { get; set; }
        public string Cpic { get; set; }
        public string Ctype { get; set; }
    }

}
