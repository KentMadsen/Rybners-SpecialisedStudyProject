//  ------------------------------------------------------------------------->
// Include Fields
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
//  ------------------------------------------------------------------------->
/* Author      : Kent vejrup Madsen
   Type        : C#, 
                 CSharp
   
   Title       : Specialised Study Project - SSP

   Name        : Razor
   Description : 
*/

namespace Program.Input
{
    class Razor : Libraries.IO.RecursiveDirectorySearch
    {
        public void Initialise()
        {
            this.TriggerOnFiles = true;
            this.TriggerOnDirectories = true;
        }

        //  ------------------------------------------------------------------------->
        // Constructors
        public Razor( String Path )
        {
            Initialise();
            this.QueuePath( Path );
        }

        public Razor( String[] Paths )
        {
            Initialise();
            this.QueuePaths( Paths );
        }
        
//  ------------------------------------------------------------------------->
//  Override
        protected override void FoundFile( String Path )
        {
            Console.WriteLine( "Current : {0}", 
                                CurrentDirectory );

            Console.WriteLine( "Found File: {0}", 
                                Path );

            FileHandling( Path );

        }

        protected override void FoundDirectory( String Path )
        {
            if( String.IsNullOrWhiteSpace( CurrentDirectory ) )
            {
                CurrentDirectory = Path;
                return;
            }

            if( String.Equals( CurrentDirectory, Path ) == false )
                CurrentDirectory = Path;

        }

//  ------------------------------------------------------------------------->
// Variables
        private String CurrentDirectory = "\0";


//  ------------------------------------------------------------------------->
// Functions
        private void FileHandling( String path )
        {
            String filename = Libraries.IO.Files.GetName( path );

            ReadFile( path, 
                      filename );
        }

        private void ReadFile( String path, String Name )
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                using ( FileStream fs = File.Open( path, 
                                                   FileMode.Open, 
                                                   FileAccess.Read, 
                                                   FileShare.Read ) )
                {

                    using ( StreamReader reader = new StreamReader( fs ) )
                    {
                        String line;

                        try
                        {
                            while( ( line = reader.ReadLine() ) != null )
                            {
                                if( String.IsNullOrWhiteSpace( line ) != true )
                                {
                                    builder.Append( line );
                                }

                            }
                        }
                        catch( ArgumentOutOfRangeException AOOREx )
                        {

                        }
                        catch ( OutOfMemoryException OOMEx )
                        {

                        }
                        catch ( IOException IOEx )
                        {

                        }

                    }

                } // Using

            } // Try -> fs
            catch ( ArgumentOutOfRangeException AOOREx )
            {

            }
            catch ( ArgumentNullException ANEx )
            {

            }
            catch ( ArgumentException AEx )
            {

            }
            catch ( PathTooLongException PTLEx )
            {

            }
            catch ( DirectoryNotFoundException DNFEx )
            {

            }
            catch ( FileNotFoundException FNFEx )
            {

            }
            catch ( IOException IOEx )
            {

            }
            catch ( UnauthorizedAccessException UAEx )
            {

            }
            catch ( NotSupportedException NSEx )
            {

            }
            
        } // ReadFile()
        
    }

} // Namespace
