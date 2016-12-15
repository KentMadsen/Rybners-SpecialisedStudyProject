using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitter
{

    public class TextSplitter : DirectorySearch
    {
        private Dictionary<String, String> Tokens = new Dictionary<string, string>();

        private StringBuilder Buildstring = new StringBuilder();
        private StringBuilder FilterBuild = new StringBuilder();

        public TextSplitter( String SearchDirectory )
        {
            this.externRootDirectory = SearchDirectory;
        }

        public void Initialise()
        {
            this.run();
        }

        public void ReadFiles()
        {

            for ( int i = 0;
                      i <= this.foundFiles.Count - 1;
                      i ++ )
            {
                Load( this.foundFiles[i] );
            }

        }

        private void Load( String s )
        {

            using ( FileStream fs = File.Open( s, 
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read ) )
            {
                using ( BufferedStream bs = new BufferedStream( fs ) )
                {

                    using ( StreamReader sr = new StreamReader( bs ) )
                    {
                        String Line;

                        while( ( Line = sr.ReadLine() ) != null )
                        {
                            TokenizeText( Line );
                        }

                    } // Using sr

                } // Using bs

            } // Using fs

        }

        private void TokenizeText( string inputLine )
        {
            int Length = inputLine.Length - 1;

            for( int x = 0; 
                     x <= Length; 
                     x ++ )
            {
                char currentChar = inputLine[x];

                AllowedCharacter( currentChar );

                if ( currentChar == ' '  ||
                     currentChar == '\r' ||
                     currentChar == '\n' ||
                     x == Length )
                {

                    string currentToken = FilterText( Buildstring.ToString().ToLower() );

                    if ( currentToken.Length == 4 )
                        goto end;

                    if ( Tokens.ContainsKey( currentToken ) != true )
                    {
                        Tokens.Add( currentToken, 
                                    currentToken );

                        Console.WriteLine( "Found : {0}", currentToken );
                    }

                    end:
                        Clear();
                }
                
            }

        }

        private Boolean FilterRules( char current )
        {

            return false;
        }

        private String FilterText( string input )
        {
            int arrayEnd = input.Length -1;

            bool ForwardEnd    = false, 
                 BackwardBegin = true;

            for( int i = 0; 
                     i <= arrayEnd; 
                     i ++ )
            {
                char current = input[i];

                char forward, 
                     backward;
                
                // CurrentStates
                if ( i == 0 )
                    BackwardBegin = true;
                else
                    BackwardBegin = false;

                // 
                if ( i == arrayEnd )
                    ForwardEnd = true;
                else
                    ForwardEnd = false;

            }

            String value = FilterBuild.ToString();

            FilterBuild.Clear();

            return value;
        }
        
        private void AllowedCharacter( char c )
        {

            if ( ( ( c >= 'A' ) && ( c <= 'z' ) ) )
            {
                Append( c );
            }

            if( ( c >= '0' ) && ( c <= '9' ) )
            {
                Append( c );
            }

            if( ( c == '.' ) || 
                ( c == ',' ) ||
                ( c == '-')  ||
                ( c == '_')  || 
                ( c == '\'' ) )
            {
                Append( c );
            }

        }
        
        private void Append( char a )
        {
            Buildstring.Append( a );
        }

        private void Clear()
        {
            Buildstring.Clear();
        }

    }

}
