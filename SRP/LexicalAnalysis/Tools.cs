using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysis
{
    public static class Tools
    {
        public static Boolean isNumber( char input )
        {
            if ( input >= '0' &&
                 input <= '9')
                return true;
            else
                return false;
        }

        public static Boolean isAlphabet( char input )
        {
            if ( input >= 'A' &&
                 input <= 'z')
                return true;
            else
                return false;
        }

        public static Boolean isCharacter( char input, char[] Chars )
        {
            foreach( char current in Chars )
            {
                if ( input == current )
                    return true;
            }

            return false;
        }

    }
}
