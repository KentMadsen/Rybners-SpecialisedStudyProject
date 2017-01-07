using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Parser
{
    class Structures
    {
        public struct Ranges
        {
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
            public void Init(char begin,
                              char end,
                              bool caseSensative)
            {
                iBegin = begin;
                iEnd = end;

                iCaseSensative = caseSensative;
            }
        }

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
            public void Init(char Character, bool Sensative)
            {
                iChar = Character;
                iCaseSensative = Sensative;
            }

        }
    }
}
