using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Libraries.IO
{
    public static class Files
    {

        public static Boolean Create( String path )
        {
            try
            {
                File.Create( path );

                return true;
            }
            catch ( Exception ex )
            {
                return false;
            }
        }

        public static Boolean Exist( String path )
        {
            try
            {
                return File.Exists( path );
            }
            catch ( Exception Ex )
            {
                return false;
            }

        }

        public static Boolean Remove( String path )
        {

            try
            {
                File.Delete( path );
                
                return true;
            }
            catch ( Exception ex )
            {
                return false;
            }
        }
        
        public static String GetName( String PathToFile )
        {
            try
            {
                String retValue = Path.GetFileName( PathToFile );

                return retValue;
            }
            catch( Exception ex )
            {

            }

            return null;
        }

    } // End Class

} // End Namespace
