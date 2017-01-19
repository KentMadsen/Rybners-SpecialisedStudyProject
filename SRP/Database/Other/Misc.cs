using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Misc
    {
        // Access Default Connection String
        public const String AccessConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path}";

        // MySQL Default Connection String
        public const String MySQLConnectionString = "Server={Address};Database={Database};Uid={Userbane};Pwd={Password};";
    }
}
