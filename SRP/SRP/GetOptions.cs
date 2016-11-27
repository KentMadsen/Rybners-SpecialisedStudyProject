using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    public class GetOptions
    {
        public struct Container
        {
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
                string current = strs[i];

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
