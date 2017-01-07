using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using System.IO;

namespace Dictionary
{
    public class Library
    {
        // Variables
        private String iLibraryPath;
        
        // Accessors
        public String LibraryPath
        {
            get
            {
                return iLibraryPath;
            }

            set
            {
                iLibraryPath = value;
            }
        }

        // ----------------------------------------------------> Constructors
        public Library()
        {
            Default();

            Initialise();

            if( Safe() == false )
            {

            }
        }

        // Defaults Settings
        private void Default()
        {
            LibraryPath = Constants.Dir;
        }

        // ----------------------------------------------------> Initialise
        private void Initialise()
        {
            if( IO.Require( LibraryPath ) != true )
            {
                // Error
                return;
            }
            
            

        }

        // Checks Safety
        private bool Safe()
        {

            return true;
        }
        
        private static class IO
        {
            // Other Functions
            public static Boolean Require( String Path )
            {
                if ( Exist( Path ) )
                {
                    return true;
                }
                else
                {
                    return Create( Path );
                }
            }

            public static Boolean Create( String Path )
            {
                try
                {
                    Directory.CreateDirectory( Path );

                    return true;
                }
                catch ( PathTooLongException PTLEx )
                {

                }
                catch ( DirectoryNotFoundException DNFEx )
                {

                }
                catch ( IOException IOEx )
                {

                }
                catch ( UnauthorizedAccessException UAEx )
                {

                }
                catch ( ArgumentNullException ANEx )
                {

                }
                catch ( ArgumentException AEx )
                {

                }
                catch ( NotSupportedException NSEx )
                {

                }

                return false;
            }

            // Checks if a file or directory Exist
            public static Boolean Exist( String Path )
            {
                if ( File.Exists( Path ) )
                {
                    return true;
                }

                if ( Directory.Exists( Path ) )
                {
                    return true;
                }

                return false;
            }
        }
        

    } // End Class

} // End Namespace
