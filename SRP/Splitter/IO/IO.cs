using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Splitter
{
    public static class IO
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
                catch( Exception ex )
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
                catch( Exception Ex ) 
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
                catch( Exception ex ) 
                {
                    return false;
                }
            }
        }

        public static class Directories
        {
            public static Boolean Create( String path )
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                catch( Exception ex )
                {

                }

                return false;
            }

            public static Boolean Exist( String path )
            {
                try
                {
                    return Directory.Exists(path);
                }
                catch( Exception ex )
                {

                }

                return false;
            }

            public static Boolean Remove( String path )
            {
                try
                {
                    Directory.Delete(path);
                    return true;
                }
                catch ( Exception ex )
                {

                }

                return false;
            }
        }

    }
}
