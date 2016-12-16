using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Database
{
    public class Access : Database
    {
        public Access( String DatabasePath )
        {
            this.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data " + @"Source=" + DatabasePath;

            ODC = new OleDbConnection( this.ConnectionString );
        }
    }
}
