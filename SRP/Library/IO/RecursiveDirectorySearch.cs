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
// Acessor Objects & Primitives
            // Protected
                // Initial Root Path to search 
        protected String RootDirectory
        {
            get
            {
                return iRootDirectory;
            }
            set
            {
                iRootDirectory = value;
            }
        }

                // Functionality
        protected Boolean TriggerOnDirectories
        {
            get
            {
                return iTriggerDirectories;
            }
            set
            {
                iTriggerDirectories = value;
            }
        }

        protected Boolean TriggerOnFiles
        {
            get
            {
                return iTriggerFiles;
            }
            set
            {
                iTriggerFiles = value;
            }
        }

        protected Boolean Pause
        {
            get
            {
                return iPause;
            }
            set
            {
                iPause = value;
            }
        }

        protected Boolean ErrorOccured
        {
            get
            {
                return iErrorOccured;
            }
            set
            {
                iErrorOccured = value;
            }
        }

        protected Boolean Completed
        {
            get
            {
                return iCompleted;
            }
            set
            {
                iCompleted = value;
            }
        }

//---------------------------------------------------------------------------->
// Variables
            // Main Variables
        private String iRootDirectory;

            // Filters
        private Boolean iTriggerDirectories = false;
        private Boolean iTriggerFiles       = false;

        private Boolean iCompleted        = false;

        private int iWaitMS = 25;

        private bool iErrorOccured = false;
        private bool iPause        = false;
        private bool iExit         = false;

        // Buffers
        private Queue<String> iPathBuffer = new Queue<String>();
        
//---------------------------------------------------------------------------->
// Functions
        protected void QueuePath( String Path )
        {
            iPathBuffer.Enqueue( Path );
        }

        protected void QueuePaths( String[] Paths )
        {
            foreach ( String path in Paths )
            {
                iPathBuffer.Enqueue( path );
            }
        }

        protected void Run()
        {
            AddDirectories( iRootDirectory );

            Search();
        }

        // Retrieves a string from inside the buffer, and removes it's index
        private String GetCurrentString()
        {
            String current = iPathBuffer.Dequeue();
            
            return current;
        }

        private void Search()
        {
            // If Empty, Exit
            if ( iPathBuffer.Count == 0 )
                goto lExit;

            // Pause work
            if ( iPause == true )
                while ( this.iPause )
                {
                    Thread.Sleep( iWaitMS );
                }
            
            // get's the next path, in Queue
            String current = GetCurrentString();

            // If the Path is empty, go to the next Path, in queue
            if ( String.IsNullOrEmpty( current ) )
                goto End;

            // Retrieve Directories, Insert into buffer
            SearchForDirectories( current );
            
            // Do something, with the current directory ?
            if ( iTriggerDirectories == true )
                FoundDirectory( current );

            // search for files, in the current Path
            SearchForFiles( current );
            
            // if told to exit, exit's the current work
            if ( iExit == true )
                goto lExit;

            // If the buffer is zero, end the flow
            End:
            if ( iPathBuffer.Count != 0 )
                Search();

            lExit:
            this.iExit = false;
                return;
        }

        private void AddDirectories( String path )
        {
            if ( Directory.Exists( path ) == false )
                return;
            
            iPathBuffer.Enqueue( path );
        }

        private void SearchForDirectories( String current )
        {

            String[] directories = Directory.GetDirectories( current );

            foreach ( String s in directories )
            {
                iPathBuffer.Enqueue( s );
            }
        }

        private void SearchForFiles( String current )
        {
            if ( iTriggerFiles == false )
                return;

            String[] files = Directory.GetFiles( current );

            foreach ( String s in files )
            {
                FoundFile( s );
            }

        } // End Search
        
        // Clear Buffer
        private void Refresh()
        {
            iPathBuffer.Clear();
        } // End refresh
        
    } // End Class

} // End Namespace
