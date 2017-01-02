using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Parser
{
    public class Rule
    {
        // Allowed Characters
        List< Individual > ListOfIndividuals = new List< Individual >();
        List< Range > ListOfRanges           = new List< Range >();

        // Constructors
        public Rule()
        {

        }


        
        // 
        private bool isWithinAllowedCharacters( char c )
        {
            if ( isWithinRanges( c ) || isWithinIndividuals( c ) )
                return true;

            return false;
        }

        private bool isWithinRanges( char c )
        {
            int length = ListOfRanges.Count;

            for ( int index = 0; 
                      index <= length; 
                      index ++ )
            {
                char begin, 
                     end;

                begin = ListOfRanges[index].Begin;
                end = ListOfRanges[index].End;

                if ( isWithinRange( begin, 
                                    end,
                                    c ) )
                {
                    return true;
                }

            }

            return false;
        }

        private bool isWithinRange( char begin, 
                                    char end, 
                                    char current )
        {
            if ( begin <= current || current >= end )
                return true;

            return false;
        }

        private bool isWithinIndividuals( char c )
        {
            int length = ListOfIndividuals.Count;

            for( int index = 0; 
                     index <= length - 1; 
                     index ++ )
            {
                char current = ListOfIndividuals[index].Character;

                if ( current == c )
                    return true;

            }

            return false;
        }
        
        // Structures
        public struct Individual
        {
            char iChar;

            public void Set( char C )
            {
                iChar = C;
            }

            public char Character
            {
                get
                {
                    return iChar;
                }
            }
        }

            // Scope Characters are within
        public struct Range
        {
            char iBegin, 
                 iEnd;

            public void Set( char Begin, 
                             char End )
            {
                iBegin = Begin;
                iEnd = End;
            }

            public char Begin
            {
                get
                {
                    return iBegin;
                }
            }

            public char End
            {
                get
                {
                    return iEnd;
                }
            }
        }

        public struct Packages
        {
            String iToken;

            public void Set( String t )
            {
                iToken = t;
            }

            public String Token
            {
                get
                {
                    return iToken;
                }

            }
        }
    }

} // End Namespace
