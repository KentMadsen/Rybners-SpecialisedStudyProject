using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Parser
{
    public class CharacterFilter
    {
        public CharacterFilter()
        {

        }

        List<Structures.Ranges> iCharacterRanges          = new List<Structures.Ranges>();
        List<Structures.Individuals> iCharacterIndividuals = new List<Structures.Individuals>();

        private Boolean InIndividuals( char current )
        {
            bool sensative = false;
            int length = iCharacterIndividuals.Count - 1;

            for( int x = 0; 
                     x <= length; 
                     x ++ )
            {
                sensative = iCharacterIndividuals[x].CaseSensative;
                
                if ( sensative )
                {
                    if ( InsideIndividual( current, 
                                           iCharacterIndividuals[x].Character ) )
                        return true;
                }
                else
                {
                    if ( InIndividualsInsensative( current, 
                                                   iCharacterIndividuals[x].Character ) )
                        return true;
                }
            }

            return false;
        }

        private Boolean InIndividualsInsensative( char current, char character )
        {
            char lowerCurrent, 
                 lowerCharacter;

            lowerCurrent = Char.ToLower( current );
            lowerCharacter = Char.ToLower( character );

            if ( InsideIndividual( lowerCurrent, 
                                   lowerCharacter ) )
                return true;

            return false;
        }

        private Boolean InsideIndividual( char current, char character )
        {
            if ( current == character )
                return true;

            return false;
        }

        private Boolean inRange( char currentChar )
        {
            bool sensative = false;
            int length = iCharacterIndividuals.Count - 1;

            for ( int x = 0; 
                      x <= length; 
                      x ++ )
            {
                sensative = iCharacterRanges[x].CaseSensative;
                
                if( sensative )
                {
                    if ( InsideRange( currentChar,
                                      iCharacterRanges[x].Begin,
                                      iCharacterRanges[x].End ) )
                        return true;
                }
                else
                {
                    if ( InsideRangeInsensative( currentChar,
                                                 iCharacterRanges[x].Begin,
                                                 iCharacterRanges[x].End ) )
                        return true;
                }
               
            }

            return false;
        }

        private Boolean InsideRangeInsensative( char current, 
                                                char begin, 
                                                char end )
        {
            char lowerCurrent, 
                 lowerBegin, lowerEnd;

            lowerCurrent = char.ToLower( current );

            lowerBegin = char.ToLower( begin );
            lowerEnd   = char.ToLower( end );

            if ( InsideRange( lowerCurrent, 
                              lowerBegin, 
                              lowerEnd ) )
                return true;

            return false;
        }

        private Boolean InsideRange( char current, 
                                     char begin, 
                                     char end )
        {
            if ( begin <= current || 
                 current >= end )
                return true;

            return false;
        }

        public Boolean isAllowed( char character )
        {
            if ( inRange( character ) )
                return true;

            if ( InIndividuals( character ) )
                return true;

            return false;
        }

    } // End Class

} // End Namespace
