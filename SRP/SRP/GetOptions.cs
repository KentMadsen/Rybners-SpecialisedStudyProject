using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    public class GetOptions
    {
        public enum Type
        {
            shortOption,
            longOption
        }

        public struct Container
        {
            public Type t;
            
            public void Init( String t, String p )
            {
                Token = t;
                Parameter = p;
            }

            public string Token;
            public string Parameter;
        }

        public GetOptions()
        {

        }

        public Container[] Parsed( string[] args )
        {
            List<Container> ListOfOptions = new List<Container>();


            return null;
        }
        
        public Container[] Parsed( string args )
        {
            List<Container> ListOfOptions = new List<Container>();

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

                string[] payload = null;

                if( parameterised )
                {
                    payload = message.Split( '=' );
                }

                if (parameterised)
                    c.Init(payload[0], payload[1]);
                else
                {
                }

                switch ( level )
                {
                    case 1:
                        c.t = Type.shortOption;
                        

                        break;

                    case 2:
                        c.t = Type.longOption;
                        

                        break;

                    default:
                        // Unknown
                        break;
                }

            }

            return null;
        }
        
        private string[] Tokenizer( string ArgString )
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
            
            string strResult = "\0";

            for ( int i = Begin;
                      i <= EndPosition;
                      i ++ )
            {
                char current = Arg[i];

                if( current == '"' )
                {
                    if ( Inside )
                        Inside = false;
                    else
                        Inside = true;

                    continue;
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
                    {
                        strBuilder.Append( current );
                    }
                else
                {
                    strBuilder.Append( current );
                }
                
            }
            
            return strResult;
        }

        private String GetOptionType( String s, out int removed, out bool parameterised )
        {
            removed = 0;
            string output;

            StringBuilder builder = new StringBuilder();

            parameterised = false;

            Boolean InitPhase = true;

            for( int x = 0; 
                     x <= s.Length - 1; 
                     x ++ )
            {
                char currentChar = s[x];

                if( currentChar == '-' )
                {
                    if( InitPhase )
                        removed = removed + 1;
                    else
                        builder.Append( currentChar );
                    
                }
                else
                {
                    if ( InitPhase == true )
                        InitPhase = false;

                    if ( currentChar == '=' )
                        if ( parameterised == false )
                            parameterised = true;

                    builder.Append( currentChar );
                }
                
            }

            output = builder.ToString();

            return output;
        }

        private bool CharacterWriteable( char c, bool AllowSpace )
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
}
