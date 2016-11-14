using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Studieretningsproject.IO
{
    public class Files
    {

        public static bool createDirectory( String path )
        {
            Boolean retValue = false;

            try
            {
                Directory.CreateDirectory( path );

                retValue = true;
            }
            catch( Exception ex )
            {
                retValue = false;
            }

            return retValue;
        }

        public static bool existDirectory( String path )
        {
            Boolean retValue = false;

            try
            {
                retValue = Directory.Exists( path );
                
                return retValue;
            }
            catch( Exception ex )
            {

                return retValue;
            }

        }

        public static String[] retrievePaths( string path )
        {
            
            try
            {
                String[] dirs = Directory.GetDirectories( path );

                return dirs;
            }
            catch( Exception ex )
            {

                return null;
            }

        }
        
    }

}
