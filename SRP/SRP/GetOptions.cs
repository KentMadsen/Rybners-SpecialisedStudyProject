using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    public class GetOptions
    {
        public enum Types
        {
            Command,
            ShortOption,
            LongOption,
            Unknown
        }

        public struct Container
        {
            
            public void Init( String token )
            {
                Token = token;

                Init( false );
            }

            public void Init( String token, 
                              String parameter )
            {
                Init( token );
                Init( true );

                Parameter = parameter;
            }

            public void Init( bool isParametersUsed )
            {
                PayloadIsFound = isParametersUsed;
            }

            public string Token;
            public string Parameter;
            public Types  Type;

            public bool PayloadIsFound;
        }
        
        public Container[] Parsed( string[] args )
        {
            List<Container> ListOfOptions = new List<Container>();


            return null;
        }
        
        public Container[] Parsed( string args )
        {
            List<Container> ListOfOptions = new List<Container>();

            // Split into an array
            string[] strs = Tokenizer( args );
            
            // Continue on
            for( int i = 0; 
                     i <= strs.Length - 1; 
                     i ++ )
            {
                Container c = new Container();

                string current = strs[i];

                int level;
                bool parameterised;

                string message = GetOptionType( current, 
                                                out level, 
                                                out parameterised );

                // Used given it contains parameters
                string[] payload = null;

                if( parameterised )
                {
                    payload = message.Split( '=' );
                }

                // If it contains parameters
                if ( parameterised )
                    c.Init( payload[0], 
                            payload[1] );
                else
                    c.Init( message );
                
                // Assign Type
                switch ( level )
                {
                    case 0:
                            c.Type = Types.Command;
                        break;

                    case 1:
                            c.Type = Types.ShortOption;
                        break;

                    case 2:
                            c.Type = Types.LongOption;
                        break;

                    default:
                        // Unknown
                            c.Type = Types.Unknown;
                        break;
                }

                Console.WriteLine("Added:{0}", c.Token);
                ListOfOptions.Add( c );

            }

            return ListOfOptions.ToArray();
        }
        
        private String[] Tokenizer( String ArgString )
        {
            List<String> BagOfTokens = new List<String>();

            int End = ArgString.Length - 1;
            
            for( int x = 0; 
                     x <= End; 
                     x ++ )
            {
                char current = ArgString[x];

                int skipTo = 0;

                String Result = ParsingTokens( x, out skipTo, 
                                               ArgString );
                
                x = skipTo;

                BagOfTokens.Add( Result );

            }

            return BagOfTokens.ToArray();
        }

        private string ParsingTokens( int Begin, out int SkipTo, 
                                      string Arg )
        {

            StringBuilder strBuilder = new StringBuilder();

            int EndPosition = Arg.Length - 1;
            SkipTo = EndPosition;

            bool Inside = false;
            
            string strResult = "";

            for ( int i = Begin;
                      i <= EndPosition;
                      i ++ )
            {
                char current = Arg[i];
                
                if ( current == '"' )
                {
                    if ( Inside )
                        Inside = false;
                    else
                        Inside = true;
                            goto write;   
                }

                if( current == ' ' )
                    if( Inside == false )
                    {
                        strResult = strBuilder.ToString();
                        strBuilder.Clear();

                        SkipTo = ( i );

                            break;
                    }
                    else
                        strBuilder.Append( current );
                else
                    strBuilder.Append( current );

                write:
                if ( i == EndPosition )
                {
                    strResult = strBuilder.ToString();
                    strBuilder.Clear();
                }
                else
                {
                    continue;
                }
            }
            
            return strResult;
        }

        private String GetOptionType( String s, 
                                      out int Removed, 
                                      out bool Parameterised )
        {
            // Parameters
            Removed = 0;
            Parameterised = false;

            // Variables
            string output;

            Boolean initPhase     = true;

            StringBuilder builder = new StringBuilder();
            
            for( int x = 0; 
                     x <= s.Length - 1; 
                     x ++ )
            {
                char currentChar = s[x];

                if( currentChar == '-' )
                {

                    if( initPhase )
                        Removed = Removed + 1;
                    else
                        builder.Append( currentChar );
                    
                }
                else
                {

                    if ( initPhase == true )
                        initPhase = false;

                    if ( currentChar == '=' )
                        if ( Parameterised == false )
                            Parameterised = true;

                    builder.Append( currentChar );
                }
                
            }

            output = builder.ToString();

            return output;
        }

        private bool CharacterWriteable( char c, 
                                         bool AllowSpace )
        {
            if( c <= 'A' || c >= 'z' )
                return true;
            
            if ( c <= '0' || c >= '9' )
                return true;

            if ( AllowSpace )
            {
                if ( c == ' ' )
                    return true;
            }

            if ( c <= '-' )
                return true;
            
            return false;
        }

    } // End GetOpts

} // End Namespace
