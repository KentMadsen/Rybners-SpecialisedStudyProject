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

        List<String> listOfWords = new List<string>();

        // 
        private Boolean isBeginning, 
                        isEnd;

        private int begin, 
                    end;

        private char back,
                     front;
        
        private void Reset()
        {
            isBeginning = false;
            isEnd       = false;
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

                // Special Cases
                if( ( isBeginning != true ) && 
                    ( isEnd != true ) )
                {
                    SpecialRules( current, 
                                  back, front );
                }
                
                if( Tools.isSpace( current ) )
                {
                    AppendWord();
                }

            }

        }
        
        //
        private void AppendWord( )
        {
            String current = Builder.ToString();

            if( String.IsNullOrWhiteSpace( current ) )
                goto lClear;

            listOfWords.Add( current );

            lClear:
                Clear();
        }

        // Builder Functions 
        private void Append( char c )
        {
            Builder.Append( c );
        }

        private void Clear()
        {
            Builder.Clear();
        }

        // Rules
        private void SpecialRules( Char CurrentPos, 
                                   Char PriorPos, Char ForwardPos )
        {
            if( CurrentPos == '-' )
            {
                if ( Tools.isAlphabetic( PriorPos ) && 
                     Tools.isAlphabetic( ForwardPos ) )
                {

                }
            }      

        }
        
    } // End Tokenizer

} // End Namespace
