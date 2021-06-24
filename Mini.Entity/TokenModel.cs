using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Model
{
    public class TokenModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string IsSystem { get; set; }
        public string IsDelete { get; set; }
        public string Role { get; set; }
    }
}
