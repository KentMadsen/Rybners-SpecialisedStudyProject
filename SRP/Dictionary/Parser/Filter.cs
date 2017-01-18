using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dictionary.Parser.Characters;

namespace Dictionary.Parser
{
    public class Filter
    {
        public enum ObjectType
        {
            Character,
            BreakStatements,
            None
        }

        public enum FilterType
        {
            Valid,
            Invalid,
            Break,
            None
        }

        public struct FilterObject
        {
            private FilterType Type;
            private char Character;

        }

        // Variables
        private Allow iCharacters = new Allow();
        private Allow iBreakStatements = new Allow();
        
        // Accessors
            // Characters
        public int LengthOfCharactersRanges
        {
            get
            {
                return iCharacters.LengthOfRanges;
            }
        }

        public int LengthOfCharactersIndividuals
        {
            get
            {
                return iCharacters.LengthOfIndividuals;
            }
        }

        public int LengthOfCharactersQueries
        {
            get
            {
                return iCharacters.LengthOfQueries;
            }
        }

            // BreakStatements
        public int LengthOfBreakRanges
        {
            get
            {
                return iBreakStatements.LengthOfRanges;
            }
        }

        public int LengthOfBreakIndividuals
        {
            get
            {
                return iBreakStatements.LengthOfIndividuals;
            }
        }

        public int LengthOfBreakQueries
        {
            get
            {
                return iBreakStatements.LengthOfQueries;
            }
        }

        // Constructors
        public Filter()
        {

        }
        
        // Ranges
        public Filter( char Begin, 
                       char End, 
                       bool CaseSensative )
        {
            AddCharacters( Begin, End, 
                           CaseSensative );
        }

        public Filter( char Begin,
                       char End,
                       bool CaseSensative, 
                       ObjectType Type )
        {
            switch( Type )
            {
                case ObjectType.Character:
                        AddCharacters( Begin, End, 
                                       CaseSensative );
                    break;

                case ObjectType.BreakStatements:
                        AddBreakstatements( Begin, End, 
                                            CaseSensative );
                    break;

                default:
                    // None
                    break;
            }
        }
        
        // Individual
        public Filter( char Individual, 
                       bool CaseSensative )
        {
            AddCharacters( Individual, 
                           CaseSensative );
        }

        public Filter( char Individual, 
                       bool CaseSensative, 
                       ObjectType Type )
        {
            Add( Individual, 
                 CaseSensative, 
                 Type );
        }


        // Primary Functions
            // Add Characters
                // Individual
        public void AddCharacters( StructureTypes.Individuals AddIndividual )
        {
            iCharacters.AddIndividuals( AddIndividual );
        }
        
        public void AddCharacters( char Individual, 
                                   bool CaseSensative )
        {
            StructureTypes.Individuals Ivls = new StructureTypes.Individuals();

            Ivls.Init( Individual, 
                       CaseSensative );

            AddCharacters( Ivls );
        }

                // Range
        public void AddCharacters( StructureTypes.Ranges AddRange )
        {
            iCharacters.AddRanges( AddRange );
        }

        public void AddCharacters( char Begin, 
                                   char End, 
                                   bool CaseSensative )
        {
            StructureTypes.Ranges Rng = new StructureTypes.Ranges();

            Rng.Init( Begin, 
                      End, 
                      CaseSensative );

            AddCharacters( Rng );

        }

            // Add Breakstatements
        public void AddBreakstatements( char Individual, 
                                        bool CaseSensative )
        {
            StructureTypes.Individuals Ivls = new StructureTypes.Individuals();

            Ivls.Init( Individual, 
                       CaseSensative );

            AddBreakstatements( Ivls );
        }

        public void AddBreakstatements( StructureTypes.Individuals Individual )
        {
            iBreakStatements.AddIndividuals( Individual );
        }
        
        public void AddBreakstatements( char Begin, char End, 
                                        bool CaseSensative )
        {
            StructureTypes.Ranges Range = new StructureTypes.Ranges();

            Range.Init( Begin, End, 
                        CaseSensative );

            AddBreakstatements( Range );
        }

        public void AddBreakstatements( StructureTypes.Ranges Range )
        {
            iBreakStatements.AddRanges( Range );
        }

            // Wrappers
                // Individual
        public void Add( StructureTypes.Individuals Individual )
        {
            AddCharacters( Individual );
        }

        public void Add( char Character )
        {
            AddCharacters( Character, 
                           false );
        }
        
        public void Add( char Character, 
                         bool Sensative )
        {
            AddCharacters( Character, 
                           Sensative );
        }

        public void Add( Char Individual, 
                         bool CaseSensative,
                         ObjectType Type )
        {

            switch ( Type )
            {
                case ObjectType.Character:
                        AddCharacters( Individual,
                                       CaseSensative );
                    break;

                case ObjectType.BreakStatements:
                        AddBreakstatements( Individual,
                                            CaseSensative );
                    break;

                default:
                        // None
                    break;
            }

        }

                // Ranges
        public void Add( StructureTypes.Ranges Range )
        {
            AddCharacters( Range );
        }

        public void Add( char Begin, char End )
        {
            AddCharacters( Begin, End, 
                           false );
        }

        public void Add( char Begin, char End, 
                         bool Sensative )
        {
            AddCharacters( Begin, End, 
                           Sensative );
        }

        public void Add( char Begin, char End,
                         bool Sensative, 
                         ObjectType Type )
        {
            switch( Type )
            {
                case ObjectType.Character:
                        AddCharacters( Begin, End, 
                                       Sensative );
                    break;

                case ObjectType.BreakStatements:
                        AddBreakstatements( Begin, End, 
                                            Sensative );
                    break;

                case ObjectType.None:
                        // None
                    break;
            }

        }



    } // End Class

} // End Namespace
