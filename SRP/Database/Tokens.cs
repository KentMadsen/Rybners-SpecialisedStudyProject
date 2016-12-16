using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Database
{
    public class Tokens : Access
    {
        public Tokens( string DatabasePath ) : base( DatabasePath )
        {

        }

        public void InsertWord( String Word )
        {

            OleDbCommand ODCom = new OleDbCommand();

            ODCom.CommandType = System.Data.CommandType.Text;
            ODCom.CommandText = @"insert into Word(Token) values(?);";

            ODCom.Connection = ODC;

            ODCom.Parameters.AddWithValue("@Token", Word);

            try
            {
                Connect();

                ODCom.ExecuteNonQuery();
            }
            catch( Exception ex )
            {

            }
            finally
            {
                if( ODC.State == System.Data.ConnectionState.Open )
                    Close();
            }
        }

    }

}
