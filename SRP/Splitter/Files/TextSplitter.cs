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

        private StringBuilder TokenizeBuilder = new StringBuilder();
        private StringBuilder FilterBuilder = new StringBuilder();

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
                    string original = TokenizeBuilder.ToString().ToLower();
                    string currentToken = FilterText( original );

                    if ( currentToken.Length == 3 )
                        goto end;

                    if ( Tokens.ContainsKey( currentToken ) != true )
                    {


                        Console.WriteLine( "Original :[{0}]", original);
                        Console.WriteLine( "Added    :[{0}]", currentToken );

                        Tokens.Add( currentToken, 
                                    currentToken );
                    }

                    end:
                        ClearTokenize();
                }
                
            }

        }

        private Boolean isAlphabet( char current )
        {
            if ( current >= 'A' && 
                 current <= 'z' )
                return true;
            else
                return false;
        }

        private Boolean isSpecialCharacter( char current )
        {

            if ( current == '.' || 
                 current == ',' || 
                 current == '-' || 
                 current == '\'' )
                return true;
            else
                return false;
        }

        private Boolean isNumber( char current )
        {
            if ( current >= '0' && 
                 current <= '9')
                return true;
            else
                return false;
        }

        private Boolean isBetweenTwoLetters( char b, char f )
        {
            if ( isAlphabet( b ) && isAlphabet( f ) )
                return true;
            else
                return false;

        }

        private Boolean isBetweenTwoNumbers( char b, char f )
        {
            if ( isNumber( b ) && isNumber( f ) )
                return true;
            else
                return false;
        }

        private Boolean isBetweenTwoKeys(char b, char f)
        {
            if ( ( isNumber( b ) || isAlphabet( b ) ) && 
                 ( isNumber( f ) || isAlphabet( f ) ) )
                return true;
            else
                return false;
        }

        private String FilterText( string input )
        {
            // Variables
            int arrayEnd = input.Length -1;

            bool forwardEnd    = false, 
                 backwardBegin = true;

            char forward  = '\0',
                 backward = '\0',
                 current  = '\0';
            
            // Function
            for ( int i = 0; 
                     i <= arrayEnd; 
                     i ++ )
            {
                current = input[i];

                // part of the alphabet or a number
                if ( isAlphabet( current ) || isNumber( current ))
                {
                    AppendFilter( current );
                    continue;
                }

                // CurrentStates
                    //  it's the beginning
                if ( i == 0 )
                    backwardBegin = true;
                else
                    backwardBegin = false;

                    // It's the ending
                if ( i == arrayEnd )
                    forwardEnd = true;
                else
                    forwardEnd = false;

                // Assigning Values
                if ( forwardEnd == false )
                    forward = input[ i + 1 ];

                if ( backwardBegin == false )
                    backward = input[ i - 1 ];
                
                // Rules
                if( current == ',' || current == '.' ) 
                {
                    if ( forwardEnd == false && backwardBegin == false )
                    {
                        if ( isBetweenTwoKeys( backward, forward ))
                            AppendFilter( current );
                    }

                    continue;
                }

                if( current == '\'' || current == '-' )
                {
                    if ( forwardEnd == false && backwardBegin == false )
                    {
                        if ( isBetweenTwoKeys( backward, forward ) )
                            AppendFilter( current );
                    }

                    continue;
                }
                
            }

            String retValue = FilterBuilder.ToString();

            ClearFilter();

            return retValue;
        }
        
        private void AllowedCharacter( char c )
        {

            if ( isAlphabet( c ) )
            {
                AppendTokenize( c );
            }

            if( isNumber( c ) )
            {
                AppendTokenize( c );
            }

            if( isSpecialCharacter( c ) )
            {
                AppendTokenize( c );
            }

        }
        
        private void AppendTokenize( char a )
        {
            TokenizeBuilder.Append( a );
        }

        private void ClearTokenize()
        {
            TokenizeBuilder.Clear();
        }

        private void AppendFilter( char c )
        {
            FilterBuilder.Append(c);
        }

        private void ClearFilter()
        {
            FilterBuilder.Clear();
        }
    }

}
