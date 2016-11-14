using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Studieretningsproject.IO
{
    public static class Hash
    {
        public static byte[] MD5Hash( Byte[] Information )
        {

            using ( var hash = MD5.Create() )
            {
                return hash.ComputeHash( Information );
            }
            
        } // End MD5Hash

        public static byte[] SHA256Hash( Byte[] Information )
        {

            using ( var hash = SHA256.Create() )
            {
                return hash.ComputeHash( Information );
            }
            
        } // End byte[] SHA256

        public static byte[] SHA1Hash( Byte[] Information )
        {

            using ( var hash = SHA1.Create() )
            {
                return hash.ComputeHash( Information );
            }
            
        } // End byte[] SHA1Hash
    }
}
