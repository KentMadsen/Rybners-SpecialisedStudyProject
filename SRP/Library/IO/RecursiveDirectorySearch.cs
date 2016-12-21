using System;
using System.Collections.Generic;
using System.IO;

using System.Threading;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.IO
{
    public abstract class RecursiveDirectorySearch
    {
//---------------------------------------------------------------------------->
// Abstract Functions
        protected abstract void FoundFile( String file );
        protected abstract void FoundDirectory( String Directory );
        
//---------------------------------------------------------------------------->
// Acessor
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

        protected Boolean triggerDirectories
        {
            get
            {
                return TriggerDirectories;
            }
            set
            {
                TriggerDirectories = value;
            }
        }

        protected Boolean triggerFiles
        {
            get
            {
                return TriggerFiles;
            }
            set
            {
                TriggerFiles = value;
            }
        }

//---------------------------------------------------------------------------->
// Variables
            // Main Variables
        private String internRootDirectory;

            // Filters
        private Boolean TriggerDirectories = false;
        private Boolean TriggerFiles       = false;

        protected Boolean Completed        = false;

        protected int waitMS = 25;

        protected bool errorOccured = false;
        protected bool pause        = false;
        protected bool exit         = false;

        // Buffers
        private Queue<String> rsBuffer = new Queue<String>();
        
//---------------------------------------------------------------------------->
// Functions
        protected void QueuePath( String path )
        {
            rsBuffer.Enqueue( path );
        }

        protected void QueuePaths( String[] paths )
        {
            foreach ( String path in paths )
            {
                rsBuffer.Enqueue( path );
            }
        }

        protected void Run()
        {
            AddDirectories( internRootDirectory );

            Search();
        }

        // Retrieves a string from inside the buffer, and removes it's index
        private String GetCurrentString()
        {
            String current = rsBuffer.Dequeue();
            

            return current;
        }

        private void Search()
        {
            if ( rsBuffer.Count == 0 )
                goto lExit;

            String current = GetCurrentString();

            if ( String.IsNullOrEmpty( current ) )
                goto End;

            SearchForDirectories( current );
            
            if ( TriggerDirectories == true )
                FoundDirectory( current );

            SearchForFiles( current );

            if( pause == true )
                while( this.pause )
                {
                    Thread.Sleep( waitMS );
                }

            if ( exit == true )
                goto lExit;

            End:
            if ( rsBuffer.Count != 0 )
                Search();

            lExit:
            this.exit = false;

                return;
        }

        private void AddDirectories( String path )
        {
            if ( Directory.Exists( path ) == false )
                return;
            
            rsBuffer.Enqueue( path );
        }

        private void SearchForDirectories( String current )
        {

            String[] directories = Directory.GetDirectories( current );

            foreach ( String s in directories )
            {
                rsBuffer.Enqueue( s );
            }
        }

        private void SearchForFiles( String current )
        {
            if ( TriggerFiles == false )
                return;

            String[] files = Directory.GetFiles( current );

            foreach ( String s in files )
            {
                FoundFile( s );
            }

        } // End Search
        
        private void Refresh()
        {
            rsBuffer.Clear();
        } // End refresh
        
    } // End Class

} // End Namespace
