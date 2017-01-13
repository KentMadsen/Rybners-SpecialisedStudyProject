﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Parser
{
    public class Filter
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
        public Filter()
        {

        }
        
        public Filter( char Begin, 
                       char End, 
                       bool CaseSensative )
        {
            AddRanges( Begin, End, 
                       CaseSensative );
        }

        public Filter( char Individual, 
                       bool CaseSensative )
        {
            AddIndividuals( Individual, 
                            CaseSensative );
        }

        // Primary Functions
            // Add
        private void AddIndividuals( Structures.Individuals AddIndividual )
        {
            iCharacterIndividuals.Add( AddIndividual );
        }
        
        public void AddIndividuals( char Individual, 
                                    bool CaseSensative )
        {
            Structures.Individuals Ivls = new Structures.Individuals();

            Ivls.Init( Individual, 
                       CaseSensative );

            AddIndividuals( Ivls );
        }

        private void AddRanges( Structures.Ranges AddRange )
        {
            iCharacterRanges.Add( AddRange );
        }

        public void AddRanges( char Begin, 
                               char End, 
                               bool CaseSensative )
        {
            Structures.Ranges Rng = new Structures.Ranges();

            Rng.Init( Begin, 
                      End, 
                      CaseSensative );

            AddRanges( Rng );
        }

                // Wrappers
        public void Add( Structures.Individuals Individual )
        {
            AddIndividuals( Individual );
        }

        public void Add( char Character )
        {
            AddIndividuals( Character, 
                            false );
        }
        
        public void Add( char Character, 
                         bool Sensative )
        {
            AddIndividuals( Character, 
                            Sensative );
        }

        public void Add( Structures.Ranges Range )
        {
            AddRanges( Range );
        }

        public void Add( char Begin, char End )
        {
            AddRanges( Begin, End, 
                       false );
        }

        public void Add( char Begin, char End, 
                         bool Sensative )
        {
            AddRanges( Begin, End, 
                       Sensative );
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

        public void RemoveRanges( char Begin, 
                                  char End )
        {

            for( int index = ( LengthOfRanges - 1 ); 
                     index >= 0; 
                     index -- )
            {

                if ( ( iCharacterRanges[index].Begin == Begin ) && 
                     ( iCharacterRanges[index].End == End ) )
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
        public Boolean Run( char Character )
        {
            if ( isInsideRange( Character ) )
                return true;

            if ( isInsideIndividuals( Character ) )
                return true;

            return false;
        }

            // Character Range, inside x -> y
        private Boolean isInsideRange( char Character )
        {
            bool sensative = false;

            foreach( Structures.Ranges currentRng in iCharacterRanges )
            {
                sensative = currentRng.CaseSensative;

                if( sensative )
                {
                    if ( isCharacterInsideRange( Character, 
                                                 currentRng.Begin, 
                                                 currentRng.End ) )
                        return true;
                }
                else
                {
                    if ( insideRangeInsensative( Character, 
                                                 currentRng.Begin, 
                                                 currentRng.End ) )
                        return true;
                }

            }

            return false;
        }

            // Lowers Characters 
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
        private Boolean isInsideIndividuals( char Character )
        {
            bool sensative = false;

            foreach( Structures.Individuals Ind in iCharacterIndividuals )
            {
                sensative = Ind.CaseSensative;
                
                if ( sensative )
                {
                    if ( isCharactersEqual( Character, 
                                            Ind.Character ) )
                        return true;
                }
                else
                {
                    if ( inIndividualsInsensative( Character, 
                                                   Ind.Character ) )
                        return true;
                }
            }

            return false;
        }

            // Lowers Characters
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