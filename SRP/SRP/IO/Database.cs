using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

namespace Studieretningsproject.IO
{
    class Database
    {
        // Access Modifier's
        public string connectionString
        {
            get
            {
                return ConnectionString;
            }

        }

        // Variables
        private string ConnectionString;

        protected OleDbConnection DB;

        // Init
        public Database( string Path )
        {
            ConnectionString = ConnectionString + " " + @"Source=" + Path;

            DB = new OleDbConnection( ConnectionString );
        }

        // Connect
        protected int Connect()
        {

            try
            {
                DB.Open();

                return 1;
            }
            catch( Exception e )
            {
                return -1;
            }

        }

        // Disconnect
        protected int Disconnect()
        {

            try
            {
                DB.Close();
                return 1;
            }
            catch ( Exception E )
            {
                return -1;
            }

        }


    }

}
