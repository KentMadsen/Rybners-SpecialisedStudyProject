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
        public String UsedRootDirectory
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
        private Boolean TriggerDirectories = false;
        private Boolean TriggerFiles = false;

            // Buffers
        private Queue<String> rsBuffer = new Queue<String>();
        
        // Functions
        protected void QueuePath( String inp )
        {
            rsBuffer.Enqueue( inp );
        }

        protected void QueuePaths( String[] inp )
        {
            foreach ( String s in inp )
                rsBuffer.Enqueue( s );
        }

        protected void run()
        {
            refresh();

            addDirectories( internRootDirectory );

            recursiveSearch();
        }

        // Retrieves a string from inside the buffer, and removes it's index
        private String getCurrentString()
        {
            String current = rsBuffer.Peek();
            rsBuffer.Dequeue();

            return current;
        }

        private void recursiveSearch()
        {
            String current = getCurrentString();

            FoundDirectory( current );

            searchForDirectories( current );
            
            searchForFiles( current );

            if ( rsBuffer.Count != 0 )
                recursiveSearch();
        }

        private void addDirectories( String path )
        {
            if ( Directory.Exists( path ) == false )
                return;

            if ( TriggerDirectories == true )
                FoundDirectory( path );

            rsBuffer.Enqueue( path );
        }

        private void searchForDirectories( String current )
        {

            String[] directories = Directory.GetDirectories( current );

            foreach ( String s in directories )
            {
                rsBuffer.Enqueue( s );
            }
        }

        private void searchForFiles( String current )
        {
            if ( TriggerFiles == false )
                return;

            String[] files = Directory.GetFiles( current );

            foreach ( String s in files )
            {
                FoundFile( s );
            }

        }
        
        private void refresh()
        {
            rsBuffer.Clear();
        }

        protected abstract void FoundFile( String file );
        protected abstract void FoundDirectory( String Directory );

    } // End Class

} // End Namespace
