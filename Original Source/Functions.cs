/*
    Summary : Functioner til programmet
    Title   : Functions.cs
    Author  : Kent v. Madsen
*/

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace SRP
{
    class Functions
    {
        // Key Load
        public static int Load( string ProgramDirectory, string Databasename )
        {
            Database DB = new Database( ProgramDirectory, 
                                        Databasename );

            try
            {
               // Findes directorien ?
                if( Directory.Exists( ProgramDirectory + @"\load\" ) )
                {
                    Console.WriteLine( @"Directory: load\ - OK" );

                    // Upload Each Document
                    string[] filePaths = Directory.GetFiles( ProgramDirectory + @"\load" );

                    Console.WriteLine( "Directory: Search in progress " );

                    if( filePaths == null || 
                        filePaths.Length == 0 )
                    {
                        Console.WriteLine( "Directory: Empty" );
                        return 0;
                    }

                    if( DB.TestConnection() == 1 )
                    {
                        // Works
                    }
                    else
                    {
                        Console.WriteLine("[Error]");
                    }

                    // String for hver file i directorien
                    foreach( string s in filePaths )
                    {
                        // Indexere dem, í tabellen Dokumenter, i databasen
                        string name = Path.GetFileName( s );

                        Console.WriteLine( "Detected: {0} ", 
                                           name );

                        string Content = File.ReadAllText( s );

                        string sourceRemoved = "";
                        string result        = "";
                        string resultUrl     = "";

                        // Fjerner udnødvendige ting
                        if ( Content.Contains("<source>") == true)
                        {
                        
                            // Får fat på Url'en til filen. givet man har sat det i
                            int begin = Content.IndexOf("<source>") + "<source>".Length;
                            int end   = Content.IndexOf("</source>");
                        
                            for( int x = begin; 
                                     x <= end -1; 
                                     x ++ )
                            {
                                resultUrl = resultUrl + Content[x].ToString();
                            }
                        

                            sourceRemoved = Content.Remove( begin - "<source>".Length, 
                                                           ( end + "</source>".Length ) - begin );

                            sourceRemoved = sourceRemoved.Remove( begin - "<source>".Length, 
                                                                  "<source>".Length );
 
                            result = sourceRemoved;

                        }
                        else
                        {
                            result = Content;
                        }

                        if ( DB.TestConnection() == 1 )
                        {
                            DB.UploadDocument( name, 
                                               result, 
                                               resultUrl, true );

                            File.Delete( s );
                        }
                        else
                        {
                            return 0;
                        }
                         

                    }

                }
                else
                {
                    DirectoryInfo DirectoryInfo = Directory.CreateDirectory( ProgramDirectory + @"\load\" );

                    Console.WriteLine( "Directory: Creation date - {0}", 
                                        Directory.GetCreationTime( ProgramDirectory + @"\load\") );

                    return 0;

                }

            }
            catch ( Exception e )
            {
                Console.WriteLine( "Process failed:{0}", e.ToString() );

                return -1;
            }



            return 1;

        }

        // Key Help
        public static int Help( string ProgramDirectory )
        {
            string readme = ProgramDirectory + @"\README.txt";

            if( File.Exists( readme ) == true )
            {
                string Content = File.ReadAllText( readme );

                Console.WriteLine( Content );

                return 1;
            }
            else
            {

            }

            return -1;
        }

        // Key Sampling
        public static int Categorize( Database DB )
        {
            Database.Documents[] Doc = DB.SelectDocuments(false, 0);
            
            Console.WriteLine( "If category is 0 its unknown" );
            Console.WriteLine( "ID: (ID) - (Category) - (Title)" );
            
            for ( int x = 0; 
                      x <= Doc.Length - 1; 
                      x ++ )
            {
                Console.WriteLine( "ID: {1} - {0} - {2}", 
                                   Doc[x].Category, Doc[x].id, 
                                   Doc[x].Title );
            }
            
            Console.WriteLine( "ID on Document" );

            Console.Write(" >> ");
            string command = Console.ReadLine();

            int commandInteger = int.Parse( command );

            Console.Write(" >> ");
            Console.WriteLine( "Set Category to" );
            string CatCommand = Console.ReadLine();
            int CatCommandInteger = int.Parse( CatCommand );

            DB.UpdateDocument( commandInteger, CatCommandInteger );

            return 0;
        }

        public static int WordCollecting( Database DB )
        {

            Database.Documents[] docs = DB.SelectDocuments( false, 0 );

            Console.WriteLine("[Tokenize]: Filtering - Beginning");
            
            // For hvert dokument i databasen
            for( int x = 0; 
                     x <= docs.Length -1; 
                     x ++ )
            {
                // der er allerede ord associaret med dokumentet
                if( DB.WordsExist( docs[x].id ) == true )
                {
                    Console.WriteLine( "Exist - {0}", 
                                       docs[x].Title );
                }
                else
                {
                    // Associarere ord med dokument
                    StringBuilder SB = new StringBuilder();

                    foreach ( char c in docs[x].Source.ToLower() )
                    {
                        if( c >= '0' && 
                            c <= '9' )
                        {
                            SB.Append( ' ' );
                            
                        }
                        
                        if( c >= 'a' && 
                            c <= 'z' )
                        {
                            SB.Append( c );
                            
                        }
                        
                        if( c == ' ' )
                        {
                            SB.Append( c );
                        }

                        // danish characters
                        if( c == 'ø' || 
                            c == 'æ' || 
                            c == 'å' )
                        {
                            SB.Append( c );
                        }

                        if( c == '\n' || c== '\r' )
                        {
                            SB.Append( ' ' );
                        }
                        
                        
                    }

                    string filtered = SB.ToString();
                    
                    // Stadie det i
                    Console.WriteLine("[Tokenize]: Filtering - Completed");
                    
                    Console.WriteLine("[Tokenize]: Tokenization - Beginning");
                    string[] tokenized = filtered.Split( ' ' );

                    List<string> tokens = new List<string>();

                    // Tilføjer det til en liste, for senere at fjerne tomme stringe
                    for( int i = 0; 
                             i <= tokenized.Length - 1; 
                             i ++)
                    {
                        tokens.Add( tokenized[i] );
                    }
                    
                    tokens.RemoveAll( String.IsNullOrWhiteSpace );


                    Console.WriteLine("[Tokenize]: Sorting");

                    List<string> SortedTokens = new List <string> ();
                    
                    // Placere den første Token
                    SortedTokens.Add( tokens[0] );

                    // Køre igennem Tokens, prøver og finde gentagelse, og sortere dem fra. Så vi har en lang liste for hvert ord.
                    for( int i = 0; 
                             i <= tokens.Count - 1; 
                             i ++ )
                    {
                        bool redundent = false;

                        for( int s = 0; 
                                 s <= SortedTokens.Count -1; 
                                 s ++)
                        {
                            if( tokens[i] == SortedTokens[s] )
                            {
                                redundent = true;
                            }
                        }

                        if( redundent == false )
                        {
                            SortedTokens.Add( tokens[i] );
                        }
                        
                    }

                    foreach( string s in SortedTokens )
                    {
                        // Documentets id og tilføjer labelen, stringen eller ordet.
                        DB.AddWords( docs[x].id, 
                                     s );
                    }

                }
                
            }
            
            Console.WriteLine("[Tokenize]: Complete");
            
            return 0;
        }


    }
}
