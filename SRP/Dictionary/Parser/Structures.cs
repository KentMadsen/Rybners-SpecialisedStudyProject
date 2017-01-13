using System;

namespace Dictionary.Parser
{
    public static class Structures
    {
        public struct Ranges
        {
            // Variables
            private Char iBegin,
                         iEnd;

            private Boolean iCaseSensative;

            // Accessors
            public Char Begin
            {
                get
                {
                    return iBegin;
                }
            }

            public Char End
            {
                get
                {
                    return iEnd;
                }
            }

            public Boolean CaseSensative
            {
                get
                {
                    return iCaseSensative;
                }
            }

            // Functions
            public void Init( char begin,
                              char end,
                              bool caseSensative )
            {
                iBegin = begin;
                iEnd = end;

                iCaseSensative = caseSensative;
            }

        } // Ranges

        public struct Individuals
        {
            // Variables
            private Char iChar;

            private Boolean iCaseSensative;

            // Accessors
            public Char Character
            {
                get
                {
                    return iChar;
                }
            }

            public Boolean CaseSensative
            {
                get
                {
                    return iCaseSensative;
                }
            }

            // Functions
            public void Init( char Character, 
                              bool Sensative )
            {
                iChar = Character;
                iCaseSensative = Sensative;
            }

        } // Individuals


    } // Class Structures

} // End Namespace
