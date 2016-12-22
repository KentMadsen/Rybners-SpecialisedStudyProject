using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Program.Input
{
    class Razor : Libraries.IO.RecursiveDirectorySearch
    {
        private void FileHandling( String path )
        {
            String filename = Libraries.IO.Files.GetName( path );

            ReadFile( path, filename );
        }

        private void ReadFile( String path, String Name )
        {
            try
            {

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

                            }

                        }
                        catch( OutOfMemoryException OOMEx )
                        {

                        }
                        catch( IOException IOEx )
                        {

                        }

                    }

                } // Using

            } // Try -> fs
            catch( ArgumentOutOfRangeException AOOREx )
            {

            }
            catch( ArgumentNullException ANEx )
            {

            }
            catch( ArgumentException AEx )
            {

            }
            catch( PathTooLongException PTLEx )
            {

            }
            catch( DirectoryNotFoundException DNFEx )
            {

            }
            catch( FileNotFoundException FNFEx )
            {

            }
            catch( IOException IOEx )
            {

            }
            catch( UnauthorizedAccessException UAEx )
            {

            }
            catch( NotSupportedException NSEx )
            {

            }
            
        } // ReadFile()


        protected override void FoundFile( String Path )
        {

        }

        protected override void FoundDirectory( String Path )
        {

        }

        
    }
}
