using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Model
{
    public class JwtPayload
    {
        public string role { get; set; }
        public string scope { get; set; }
    }
}
