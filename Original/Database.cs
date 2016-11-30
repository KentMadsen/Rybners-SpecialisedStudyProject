/*
    Summary : Functioner til programmet
    Title   : Database.cs
    Author  : Kent v. Madsen
*/

using System;
using System.Data;
using System.Data.OleDb;

namespace SRP
{
    class Database
    {
        string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data ";

        private OleDbConnection DBConnection;

        // Initialize
        public Database( string ProgramDirectory, 
                         string DatabaseName )
        {
            ConnectionString = ConnectionString + @"Source=" + ProgramDirectory + @"\" + DatabaseName;
            DBConnection = new OleDbConnection( ConnectionString );
        }

        // Tester om det virker
        public int TestConnection()
        {
            Console.WriteLine( "[Database]: Connecting" );

            if ( Connect() == 1 )
            {
                Console.WriteLine( "[Database]: OK" );

                    Disconnect();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int AddCategoryWords( int Category, 
                                     String Label  )
        {
            string Query = string.Format( "INSERT INTO CategoryWord ( IdCategory, Val) VALUES('{0}','{1}');", 
                                           Category, 
                                           Label );
            
            try
            {
                Connect();

                OleDbDataAdapter ODDA = new OleDbDataAdapter();

                ODDA.InsertCommand = new OleDbCommand( Query, 
                                                       DBConnection );

                ODDA.InsertCommand.ExecuteNonQuery();

                Disconnect();
            }
            catch( Exception e )
            {
                Console.WriteLine( e.Message );
                Disconnect();
            }

            return 0;
        }

        public int AddWords( int DocumentIdentity, 
                             String Label )
        { 
            string Query = string.Format( "INSERT INTO Words ( idDocument,Val ) VALUES ({0},'{1}')", 
                                          DocumentIdentity, 
                                          Label );
            
            try
            {
                Connect();
                
                OleDbDataAdapter ODDA = new OleDbDataAdapter();

                ODDA.InsertCommand = new OleDbCommand( Query, 
                                                       DBConnection );

                ODDA.InsertCommand.ExecuteNonQuery();
                
                Disconnect();
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                Disconnect();
            }

            return 0;
        }

        // Får forbindelse til Databasen
        private int Connect()
        {
            try
            {
                DBConnection.Open();
                return 1;
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message.ToString() );
                return 0;
            }
        }

        // Disconnecter den
        private int Disconnect()
        {
            try
            { 
                DBConnection.Close();
                return 1;
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message.ToString() );
                return 0;
            }
        }

        // Uploader et Dokument
        public int UploadDocument( String Title, 
                                   String Content, 
                                   String Url,
                                   bool ShowResult )
        {
            string Query = string.Format( "select * from [{0}]", 
                                          "Document" );

            try
            {
                if( Connect() == 1 )
                {

                }
                else
                {
                    return 0;
                }

                OleDbDataAdapter ODDA    = new OleDbDataAdapter( Query, 
                                                                 DBConnection );
                OleDbCommandBuilder ODC  = new OleDbCommandBuilder( ODDA );

                ODDA.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                DataSet DS = new DataSet();

                ODDA.Fill( DS, 
                           "Document" );

                DataRow DR = DS.Tables["Document"].NewRow();

                DR["Title"]      = Title;
                DR["Source"]     = Content;
                DR["Url"]  = Url;
                
                DS.Tables["Document"].Rows.Add( DR );

                ODDA.Update( DS, 
                             "Document" );

                if( ShowResult == true )
                {
                    Console.WriteLine( "Title: {0}", 
                                        Title );

                    Console.WriteLine( "Content: {0}", 
                                        Content );

                    Console.WriteLine( "Url: {0}", 
                                        Content );
                }

                Disconnect();
            }
            catch ( Exception e )
            {
                Console.WriteLine( "Error : {0} ", 
                                    e.ToString() );
                Disconnect();
                return -1;
            }
            
            return 0;
        }

        public struct Documents
        {
            public int id;
            public string Title;
            public string Source;
            public int Category;
        }

        // Får fat på samples samplet og ikke samplet
        public Documents[] SelectDocuments( bool katerogiseret, int id )
        {
            Documents[] Result;

            string Query= "";

            // Fjerner dem
            if( katerogiseret == true )
            {
                Query = string.Format( "select * from [{0}] where idCategory={1}", 
                                       "Document", id );
            }
            else
            {
                Query = string.Format( "select * from [{0}]", 
                                       "Document" );
            }

            DataSet DS = new DataSet();

            try
            {
                if( Connect() == 1 )
                {
                }

                OleDbDataAdapter ODDA = new OleDbDataAdapter( Query, 
                                                              DBConnection );

                ODDA.Fill( DS, "Document" );
                
                Disconnect();

                if( DS.Tables["Document"].Rows.Count == 0 )
                {
                    return null;
                }
                else
                {
                    int rows_Length = DS.Tables["Document"].Rows.Count;

                    Result = new Documents[ rows_Length ];

                    for( int x = 0; 
                             x <= ( rows_Length - 1 ); 
                             x ++ )
                    {
                        Result[x].Title     = (string) DS.Tables["Document"].Rows[x]["Title"];
                        Result[x].Source    = (string) DS.Tables["Document"].Rows[x]["Source"];
                        
                        Result[x].id        = (int) DS.Tables["Document"].Rows[x]["idDocument"];

                        try
                        {
                            Result[x].Category = (int)DS.Tables["Document"].Rows[x]["idCategory"];
                        }
                        catch ( Exception e )
                        {
                            Result[x].Category = 0;
                        }
                    }

                    return Result;
                }

            }
            catch( Exception e )
            {
                Console.WriteLine(e.ToString());
                Disconnect();
            }

            Result = null;
            return Result;
            
        }

        // Retunere Categorien
        public int CreateCategory( string Label )
        {

            string Query = "INSERT INTO Category ( Label ) VALUES ( '{0}' ) ";
            string RetrieveQuery = string.Format( "Select IdCategory FROM [Category] WHERE Label='{0}'", 
                                                  Label );
            
            // Indsætter Katerogien
            try
            {
                OleDbDataAdapter ODDA = new OleDbDataAdapter();

                Connect();

                ODDA.InsertCommand = new OleDbCommand( String.Format( Query, Label ), 
                                                       DBConnection );
                ODDA.InsertCommand.ExecuteNonQuery();

                Disconnect();
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString() );
                Disconnect();

                return -1;
            }

            // Retriever Id'en...
            try
            {
                Connect();

                OleDbDataAdapter ODDA = new OleDbDataAdapter( RetrieveQuery, 
                                                              DBConnection );
                DataSet DS = new DataSet();
                
                ODDA.Fill( DS, "Category" );
                int result = (int)DS.Tables["Category"].Rows[0]["idCategory"];
                
                Disconnect();
                
                return result;
            }
            catch( Exception e )
            {

                Console.WriteLine( e.ToString() );
                Disconnect();

                return -1;
            }
            
        }

        public bool WordsExist(int id)
        {
            string Query = "SELECT * FROM Words where IdDocument={0}";

            DataSet set = new DataSet();

            Connect();

            OleDbDataAdapter ODDA = new OleDbDataAdapter(string.Format(Query, id), DBConnection);
            
            ODDA.Fill( set, "Words" );

            try
            {
                string val = (string)set.Tables["Words"].Rows[0]["Val"];
                Disconnect();
                return true;
            }
            catch ( Exception e )
            {
                Disconnect();
                return false;
            }


            Disconnect();

            return false;
        }

        // Update Documentet
        public int UpdateDocument( int id, int newCategory )
        {
            Connect();

            try
            { 
            
                OleDbDataAdapter ODDA = new OleDbDataAdapter();
                OleDbCommand command = new OleDbCommand( "UPDATE", 
                                                          DBConnection );

                ODDA.SelectCommand = command;
                string updateQuery = string.Format( "UPDATE Document SET idCategory={0} WHERE IdDocument={1}", 
                                                     newCategory, 
                                                     id );

                command = new OleDbCommand( updateQuery, 
                                            DBConnection );

                ODDA.UpdateCommand = command;
                command.ExecuteNonQuery();

                Disconnect();

            }
            catch( Exception e )
            {
                Console.WriteLine( e.ToString() );
                Disconnect();
            }

            return 0;
        }



    }
}

