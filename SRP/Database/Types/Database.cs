using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Database
{
    public abstract class Database
    {
        protected String ConnectionString;
        protected OleDbConnection ODC;

        protected bool Connect()
        {
            try
            {
                ODC.Open();
            }
            catch( Exception ex )
            {
                return false;
            }

            return true;
        }

        protected bool Close()
        {
            try
            {
                ODC.Close();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

    }

}
