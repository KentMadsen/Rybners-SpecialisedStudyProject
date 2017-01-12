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
        private List< Structures.Ranges > iCharacterRanges           = new List< Structures.Ranges >();
        private List< Structures.Individuals > iCharacterIndividuals = new List< Structures.Individuals >();
        
        // Accessors
        public int LengthOfRanges
        {
            get
            {
                return iCharacterRanges.Count;
            }
        }

        public int LengthOfIndividuals
        {
            get
            {
                return iCharacterIndividuals.Count;
            }
        }

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

        // Primary Functions
            // Add
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

            // Remove
                // Indexes
        public void RemoveRanges( int index )
        {
            iCharacterRanges.RemoveAt( index );
        }

        public void RemoveIndividuals( int index )
        {
            iCharacterIndividuals.RemoveAt( index );
        }

                // Character
        public void RemoveRanges( char Character )
        {

            for( int index = ( LengthOfRanges - 1 );
                     index >= 0;
                     index -- )
            {

                if( ( iCharacterRanges[index].Begin == Character ) || 
                    ( iCharacterRanges[index].End == Character ) )
                {
                    iCharacterRanges.RemoveAt( index ); 
                }

            }

        }

        public void RemoveRanges( char CharBegin, char CharEnd )
        {

            for( int index = ( LengthOfRanges - 1 ); 
                     index >= 0; 
                     index -- )
            {

                if ( ( iCharacterRanges[index].Begin == CharBegin ) && 
                     ( iCharacterRanges[index].End == CharEnd ) )
                {
                    iCharacterRanges.RemoveAt( index );
                }

            }

        }
        
        public void RemoveIndividuals( char Character )
        {

            for( int index = ( LengthOfIndividuals - 1 ); 
                     index >= 0; 
                     index -- )
            {

                if( iCharacterIndividuals[index].Character == Character )
                {
                    iCharacterIndividuals.RemoveAt( index );
                }

            }

        }

            // Clear
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
        public Boolean Run( char character )
        {
            if ( insideRange( character ) )
                return true;

            if ( insideIndividuals( character ) )
                return true;

            return false;
        }

            // Character Range, inside x -> y
        private Boolean insideRange( char currentChar )
        {
            bool sensative = false;

            foreach( Structures.Ranges currentRng in iCharacterRanges )
            {
                sensative = currentRng.CaseSensative;

                if( sensative )
                {
                    if ( isCharacterInsideRange( currentChar, 
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

        // Sensative Version
        private Boolean insideRangeInsensative( char current, 
                                                char begin, 
                                                char end )
        {
            // Init variables
            char lowerCurrent, 
                 lowerBegin, lowerEnd;

            // Lower Input
            lowerCurrent = char.ToLower( current );

            lowerBegin = char.ToLower( begin );
            lowerEnd   = char.ToLower( end );

            // Checks if it's inside boundry
            if ( isCharacterInsideRange( lowerCurrent, 
                                         lowerBegin, 
                                         lowerEnd ) )
                return true;

            // if it isn't
            return false;
        }

        private Boolean isCharacterInsideRange( char current,
                                                char begin,
                                                char end )
        {
            if ( begin <= current ||
                 current >= end )
                return true;

            return false;
        }

        // Individual Characters
        private Boolean insideIndividuals( char current )
        {
            bool sensative = false;

            foreach( Structures.Individuals Ind in iCharacterIndividuals )
            {
                sensative = Ind.CaseSensative;
                
                if ( sensative )
                {
                    if ( isCharactersEqual( current, 
                                            Ind.Character ) )
                        return true;
                }
                else
                {
                    if ( inIndividualsInsensative( current, 
                                                   Ind.Character ) )
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

            lowerCurrent   = Char.ToLower( current );
            lowerCharacter = Char.ToLower( character );

            if ( isCharactersEqual( lowerCurrent, 
                                    lowerCharacter ) )
                return true;

            return false;
        }

        private Boolean isCharactersEqual( char current, 
                                           char character )
        {
            if ( current == character )
                return true;

            return false;
        }
        
    } // End Class

} // End Namespace
