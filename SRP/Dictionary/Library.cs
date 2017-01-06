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
        enum TypeSet
        {
            None,
            Danish,
            English
        };

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
            if( dirRequire( LibraryPath ) != true )
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

        // Other Functions
        private Boolean dirRequire( String Path )
        {
            if( ioExist( Path ) )
            {
                return true;
            }
            else
            {
                return dirCreate( Path );
            }
        }

        private Boolean dirCreate( String Path )
        {
            try
            {
                Directory.CreateDirectory( Path );

                return true;
            }
            catch( PathTooLongException PTLEx )
            {

            }
            catch( DirectoryNotFoundException DNFEx )
            {

            }
            catch( IOException IOEx )
            {

            }
            catch( UnauthorizedAccessException UAEx )
            {

            }
            catch( ArgumentNullException ANEx )
            {

            }
            catch( ArgumentException AEx )
            {

            }
            catch( NotSupportedException NSEx )
            {

            }
           
            return false;
        }

        // Checks if a file or directory Exist
        private Boolean ioExist( String Path )
        {
            if( File.Exists( Path ) )
            {
                return true;
            }

            if( Directory.Exists( Path ) )
            {
                return true;
            }
            
            return false;
        }
        
    } // End Class

} // End Namespace
