using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Database
{

    public class Documents : Access
    {

        public Documents( string DBpath ) : base( DBpath )
        {

        }

        public void AddDocuments( String Title, 
                                  String Root, 
                                  int refContent )
        {
            Open();
            const String Query = "INSERT INTO Documents (Title, Content, Root) VALUES(@title,@content,@root)";

            string form = Query.Replace("@title", "'" + Title + "'");

            form = form.Replace("@root", "'"+Root+"'");

            form = form.Replace("@content", "'" + refContent.ToString() + "'");

            OleDbCommand ODC = new OleDbCommand( form );
            
            ODC.Connection = ODCn;

            ODC.ExecuteNonQuery();

            Close();
        }

        public int AddDocumentContent( String Content )
        {
            Open();

            const String Query = "INSERT INTO DocumentContents(Content) VALUES (@content);";
            const String Query2 = "SELECT @@identity";

            OleDbCommand ODC = new OleDbCommand( Query );

            ODC.Parameters.Add( "@content", OleDbType.VarChar ).Value = Content;
            ODC.Connection = ODCn;

            ODC.ExecuteNonQuery();

            ODC.CommandText = Query2;
            int i = (Int32)ODC.ExecuteScalar();

            Close();

            return i;
        }
        

    }

}
