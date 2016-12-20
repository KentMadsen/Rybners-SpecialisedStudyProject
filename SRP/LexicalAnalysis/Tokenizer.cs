using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    class Tokenizer
    {
        StringBuilder Builder = new StringBuilder();
        private Dictionary<String, String> TokenMap = new Dictionary<string, string>();
        public Dictionary<String, String> Library
        {
            get
            {
                return TokenMap;
            }
        }
        // 
        private Boolean isBeginning, 
                        isEnd;

        private int begin, 
                    end;

        private char back,
                     front;

        private int minimumLength = 7;
        
        private void Reset()
        {
            isBeginning = false;
            isEnd       = false;
        }

        // Builder Functions 
        private void Append(char c)
        {
            Builder.Append(c);
        }

        private void Clear()
        {
            Builder.Clear();
        }


        private void State( int f, 
                            int begin, 
                            int end )
        {
            if ( f == begin )
                isBeginning = true;
            else
                isBeginning = false;

            if ( f == end )
                isEnd = true;
            else
                isEnd = false;
        }

        public void Parse( String Line )
        {
            begin = 0;
            end = Line.Length - 1;
            
            Reset();

            for ( int x = begin; 
                      x <= end; 
                      x ++ )
            {
                // Update State
                State( x, 
                       begin, 
                       end );

                // Current charracter 
                char current = Line[x];

                // Update characters
                if ( isBeginning != true )
                    back = Line[ x - 1 ];

                if ( isEnd != true )
                    front = Line[ x + 1 ];
                
                // Normal Parsing
                if( Tools.isAlphabetic( current ) )
                {
                    Append( current );
                }
                
                if( Tools.isSpace( current ) )
                {
                    AppendWord();
                }

                if(SpecialRules(current, back, front) == true)
                {
                    Append(current);
                }
                
            }

            Reset();
            
        }
        
        //
        private void AppendWord( )
        {
            String current = Builder.ToString().ToLower();

            if ( current.Length <= this.minimumLength )
                goto lClear;

            if( String.IsNullOrWhiteSpace( current ) )
                goto lClear;

            if( TokenMap.ContainsKey( current ) != true )
            {
                TokenMap.Add( current, 
                              current );
            }

            lClear:
                Clear();
        }

        // Rules
        private bool SpecialRules( Char CurrentPos, 
                                   Char PriorPos, Char ForwardPos )
        {
            if( CurrentPos == '\'' )
            {
                if ( Tools.isAlphabetic( PriorPos ) && 
                     Tools.isAlphabetic( ForwardPos ) )
                    return true;
            }

            if( CurrentPos == '-' )
            {
                if ( Tools.isAlphabetic( PriorPos ) && 
                     Tools.isAlphabetic( ForwardPos ) )
                {
                    return true;
                }
            }

            if( CurrentPos == '.' )
            {
                if ( Tools.isAlphabetic( ForwardPos ) && Tools.isAlphabetic(PriorPos))
                    return true;
            }
            
            return false;
        }
        
    } // End Tokenizer

} // End Namespace
