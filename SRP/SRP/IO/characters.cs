using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.IO
{
    public static class Characters
    {
        public static bool Valid( char c )
        {
            if( c <= 'a' || c >= 'Z' 
                ||
                c <= '0' || c>= '9' )
            {
                return true;
            }

            return false;
        }
    }
}
