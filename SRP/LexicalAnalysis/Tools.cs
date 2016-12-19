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

        public static Boolean isAlphabetic( char input )
        {
            if ( input >= 'A' &&
                 input <= 'Z')
                return true;

            if ( input >= 'a' && 
                 input <= 'z' )
                return true;

            // Lower case
            if ( input == 'å' || 
                 input == 'ø' || 
                 input == 'æ' )
                return true;

            // Upper case
            if ( input == 'Å' || 
                 input == 'Ø' || 
                 input == 'Æ')
                return true;
            
            return false;
        }

        public static Boolean isSpace( char input )
        {
            if ( input == ' ' )
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
