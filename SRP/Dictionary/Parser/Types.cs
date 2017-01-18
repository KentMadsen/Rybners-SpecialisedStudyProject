using System;

namespace Dictionary.Parser
{
    public static class StructureTypes
    {
        public struct Ranges
        {
            // Variables
            private Char iBegin,
                         iEnd;

            private Boolean iCaseSensative;

            private int iPriority;

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

            public int Priority
            {
                get
                {
                    return iPriority;
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
            public void Init( char begin, char end,
                              bool caseSensative )
            {
                ResetPriority();

                iBegin = begin;
                iEnd   = end;

                iCaseSensative = caseSensative;
            }

            public void Init( char begin, char end, 
                              bool caseSensative, 
                              int found )
            {
                Init( begin, end, 
                      caseSensative );

                iPriority = found;
            }

            public void IncreasePriority()
            {
                iPriority++;
            }

            public void DecreasePriority()
            {
                iPriority--;
            }

            public void ResetPriority()
            {
                iPriority = 0;
            }

        } // Ranges

        public struct Individuals
        {
            // Variables
            private Char iChar;

            private Boolean iCaseSensative;

            private int iPriority;

            // Accessors
            public Char Character
            {
                get
                {
                    return iChar;
                }
            }

            public int Priority
            {
                get
                {
                    return iPriority;
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
                ResetPriority();

                iChar  = Character;
                iCaseSensative = Sensative;
            }

            public void Init( char Character, 
                              bool Sensative, 
                              int Found )
            {
                Init( Character, 
                      Sensative );

                iPriority = Found;
            }

            public void IncreasePriority()
            {
                iPriority++;
            }

            public void DecreasePriority()
            {
                iPriority--;
            }

            public void ResetPriority()
            {
                iPriority = 0;
            }

        } // Individuals


        public struct Tokens
        {

            // Variables
            private String Content;
            private ListTypes.TokenTypes TokenType;

            // Accessors
            public String Token
            {
                get
                {
                    return Content;
                }
            }

            public ListTypes.TokenTypes Type
            {
                get
                {
                    return TokenType;
                }
            }

            // Functions
            public void Init( String Token )
            {
                Content = Token;
            }

            public void Init( ListTypes.TokenTypes Type )
            {
                TokenType = Type;
            }

            public void Init( String Token, 
                              ListTypes.TokenTypes Type )
            {
                Init( Token );
                Init( Type );
            }
            
        } // Tokens

        public struct Composite
        {
            private int[] TokensId;

            // Functions
            public void Init( int[] Ids )
            {
                TokensId = Ids;
            }

            public int[] ArrayIds
            {

                get
                {
                    return TokensId;
                }

            }

        }

    } 

    public static class ListTypes
    {
        public enum TokenTypes
        {
            Word,
            Number,
            None
        }
    }

} // End Namespace
