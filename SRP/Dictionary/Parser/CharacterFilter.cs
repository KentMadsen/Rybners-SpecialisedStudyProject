using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Parser
{
    public class CharacterFilter
    {
        // Variables
        List<Structures.Ranges> iCharacterRanges          = new List<Structures.Ranges>();
        List<Structures.Individuals> iCharacterIndividuals = new List<Structures.Individuals>();
            
        // Constructors
        public CharacterFilter()
        {

        }

        public CharacterFilter( char Begin, 
                                char End, 
                                bool CaseSensative )
        {
            AddRanges( Begin, End, 
                       CaseSensative );
        }

        public CharacterFilter( char Individual, 
                                bool CaseSensative )
        {
            AddIndividuals( Individual, 
                            CaseSensative );
        }

        // 
        public void AddIndividuals( char Individual, 
                                    bool CaseSensative )
        {
            Structures.Individuals Ivls = new Structures.Individuals();

            Ivls.Init( Individual, 
                      CaseSensative );

            iCharacterIndividuals.Add( Ivls );
        }

        public void AddRanges( char Begin, 
                               char End, 
                               bool CaseSensative )
        {
            Structures.Ranges Rng = new Structures.Ranges();

            Rng.Init( Begin, 
                      End, 
                      CaseSensative );

            iCharacterRanges.Add( Rng );
        }

        public void ClearIndividuals()
        {
            iCharacterIndividuals.Clear();
        }

        public void ClearRanges()
        {
            iCharacterRanges.Clear();
        }

        public void ClearBoth()
        {
            ClearIndividuals();
            ClearRanges();
        }

        // Functions
        public Boolean Filter( char character )
        {
            if ( insideRange( character ) )
                return true;

            if ( inIndividuals( character ) )
                return true;

            return false;
        }

        private Boolean inIndividuals( char current )
        {
            bool sensative = false;
            int length = iCharacterIndividuals.Count - 1;

            for( int index = 0; 
                     index <= length; 
                     index ++ )
            {
                sensative = iCharacterIndividuals[index].CaseSensative;
                
                if ( sensative )
                {
                    if ( isCharactersEqual( current, 
                                           iCharacterIndividuals[index].Character ) )
                        return true;
                }
                else
                {
                    if ( inIndividualsInsensative( current, 
                                                   iCharacterIndividuals[index].Character ) )
                        return true;
                }
            }

            return false;
        }

        private Boolean inIndividualsInsensative( char current, 
                                                  char character )
        {
            char lowerCurrent, 
                 lowerCharacter;

            lowerCurrent = Char.ToLower( current );
            lowerCharacter = Char.ToLower( character );

            if ( isCharactersEqual( lowerCurrent, 
                                   lowerCharacter ) )
                return true;

            return false;
        }

        private Boolean isCharactersEqual( char current, char character )
        {
            if ( current == character )
                return true;

            return false;
        }

        private Boolean insideRange( char currentChar )
        {
            bool sensative = false;
            int length = iCharacterIndividuals.Count - 1;

            foreach( Structures.Ranges currentRng in iCharacterRanges )
            {
                sensative = currentRng.CaseSensative;

                if( sensative )
                {
                    if ( isCharactersInsideRange( currentChar, 
                                                  currentRng.Begin, 
                                                  currentRng.End ) )
                        return true;
                }
                else
                {
                    if ( insideRangeInsensative( currentChar, 
                                                 currentRng.Begin, 
                                                 currentRng.End ) )
                        return true;
                }

            }

            return false;
        }

        private Boolean insideRangeInsensative( char current, 
                                                char begin, 
                                                char end )
        {
            char lowerCurrent, 
                 lowerBegin, lowerEnd;

            lowerCurrent = char.ToLower( current );

            lowerBegin = char.ToLower( begin );
            lowerEnd   = char.ToLower( end );

            if ( isCharactersInsideRange( lowerCurrent, 
                                          lowerBegin, 
                                          lowerEnd ) )
                return true;

            return false;
        }

        private Boolean isCharactersInsideRange( char current, 
                                                 char begin, 
                                                 char end )
        {
            if ( begin <= current || 
                 current >= end )
                return true;

            return false;
        }
        
    } // End Class

} // End Namespace
