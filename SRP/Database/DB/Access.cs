//---------------------------------------------------------------------------->
// Include Fields
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
//  ------------------------------------------------------------------------->
/* Author      : Kent vejrup Madsen
   Type        : C#, 
                 CSharp
   
   Title       : Specialised Study Project - SSP

   Name        : Access
   Description : 
*/

namespace Database
{
    public class Access : Database
    {
        // Variables
        protected OleDbConnection ODCn;

//---------------------------------------------------------------------------->
// Constructors
        public Access( String DBpath )
        {
            this.connectionString = Misc.AccessConnectionString + DBpath;

            ODCn = new OleDbConnection( this.connectionString );
        }

        // Connection
        protected Boolean Open()
        {
            try
            {
                ODCn.Open();
            }
            catch( InvalidOperationException IOEx )
            {
                return false;
            }
            catch( OleDbException ODEx ) 
            {
                return false;
            }
            catch( Exception Ex )
            {
                return false;   
            }

            return true;
        }

        protected void Close()
        {
            ODCn.Close();
        }

    } // Class

} // End Namespace
