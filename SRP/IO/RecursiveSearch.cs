using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public abstract class RecursiveSearch
    {
        // Accessors
            // Public
        public List<String> foundDirectories
        {
            get
            {
                return DirectoriesFound;
            }
        }

        public List<String> foundFiles
        {
            get
            {
                return FilesFound;
            }
        }

        public String usedRootDirectory
        {
            get
            {
                return internRootDirectory;
            }
        }

            // Protected
        protected String externRootDirectory
        {
            get
            {
                return internRootDirectory;
            }
            set
            {
                internRootDirectory = value;
            }
        }

        // Variables
            // Main Variables
        private String internRootDirectory;

            // Filters
        private Boolean FilterForDirectories = false;
        private Boolean FilterForFiles = false;

            // Buffers
        private Queue<String> CurrentBuffer = new Queue<String>();

            // List
        private List<String> DirectoriesFound = new List<String>();
        private List<String> FilesFound = new List<String>();


        // Functions
        protected void run()
        {
            refresh();

            addDirectories( internRootDirectory );

            recursiveSearch();
        }

        // Retrieves a string from inside the buffer, and removes it's index
        private String getCurrentString()
        {
            String current = CurrentBuffer.Peek();
            CurrentBuffer.Dequeue();

            return current;
        }

        private void recursiveSearch()
        {
            String current = getCurrentString();

            FoundDirectory( current );

            searchForDirectories( current );
            
            searchForFiles( current );

            if ( CurrentBuffer.Count != 0 )
                recursiveSearch();
        }

        private void addDirectories( String path )
        {
            if ( Directory.Exists( path ) == false )
                return;

            if ( FilterForFiles == false )
                DirectoriesFound.Add( path );

            CurrentBuffer.Enqueue( path );
        }

        private void searchForDirectories( String current )
        {

            String[] directories = Directory.GetDirectories( current );

            foreach ( String s in directories )
            {
                CurrentBuffer.Enqueue( s );
            }
        }

        private void searchForFiles( String current )
        {
            if ( FilterForDirectories == true )
                return;

            String[] files = Directory.GetFiles( current );

            foreach ( String s in files )
            {
                FoundFile( s );

                addFiles( s );
            }

        }

        private void addFiles( String path )
        {
            FilesFound.Add( path );
        }

        private void refresh()
        {
            CurrentBuffer.Clear();

            DirectoriesFound.Clear();
            FilesFound.Clear();
        }

        protected abstract void FoundFile( String file );
        protected abstract void FoundDirectory( String Directory );

    } // End Class

} // End Namespace
