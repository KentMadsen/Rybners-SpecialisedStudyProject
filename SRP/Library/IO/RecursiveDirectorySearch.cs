//  ------------------------------------------------------------------------->
// Include Fields
using System;
using System.Collections.Generic;
using System.IO;

using System.Threading;

using System.Linq;
using System.Text;

using System.Threading.Tasks;
//  ------------------------------------------------------------------------->
/* Author      : Kent vejrup Madsen
   Type        : C#, 
                 CSharp
   
   Title       : Specialised Study Project - SSP

   Name        : Recursive Directory Searcher
   Description : 
*/

namespace Libraries.IO
{
    public abstract class RecursiveDirectorySearch
    {
        
//  ------------------------------------------------------------------------->
// Constructors
        public RecursiveDirectorySearch()
        {
            iChildWorker = new Thread( Search );
        }

//  ------------------------------------------------------------------------->
// Abstract Functions
        protected abstract void FoundFile( String file );
        protected abstract void FoundDirectory( String Directory );
        
//  ------------------------------------------------------------------------->
// Acessor Objects & Primitives
            // Class shared, Accessors

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

//  ------------------------------------------------------------------------->
// Variables
            // Main Variables
        private String iRootDirectory;

            // Filters
                // Triggers -> do something
        private Boolean iTriggerDirectories = false;
        private Boolean iTriggerFiles       = false;

                // Search States
        private Boolean iCompleted      = false;

        private Boolean iErrorOccured   = false;
        private Boolean iPause          = false;
        private Boolean iExit           = false;
        
            // Wait
        private int iWaitMS = 25;
        
            // Buffers
        private Queue<String> iPathBuffer = new Queue<String>();
        
            // Children
        private Thread iChildWorker;

//  ------------------------------------------------------------------------->
// Shared Class Functions
        protected void QueuePath( String Path )
        {
            AddDirectory( Path );
        }

        protected void QueuePaths( String[] Paths )
        {
            foreach ( String path in Paths )
            {
                AddDirectory( path );
            }
        }
        
        protected String PeekQueue()
        {
            return iPathBuffer.Peek();
        }

        protected String DequeuePath()
        {
            return iPathBuffer.Dequeue();
        }

        protected void Run()
        {
            AddDirectory( iRootDirectory );

            iChildWorker.Start();
        }

//  ------------------------------------------------------------------------->
// Private Functions
        // Clear Buffer
        private void Refresh()
        {
            iPathBuffer.Clear();
        } // End refresh


        // Retrieves a string from inside the buffer, and removes it's index
        private String GetCurrentString()
        {
            return iPathBuffer.Dequeue();
        }

        // Add's Directories or Paths, to the Queue.
        private void AddDirectory( String path )
        {
            // Empty, do nothing
            if ( String.IsNullOrWhiteSpace( path ) )
                return;

            // Doesn't exist, do nothing
            if ( Directory.Exists( path ) == false )
                return;

            iPathBuffer.Enqueue( path );
        }


        //
        private void SearchForDirectories( String Current )
        {
            String[] directories = Directory.GetDirectories( Current );

            foreach ( String path in directories )
            {
                AddDirectory( path );
            }
        } // End SearchForDirectories

        //
        private void SearchForFiles( String Current )
        {
            if ( iTriggerFiles == false )
                return;

            String[] files = Directory.GetFiles( Current );

            foreach ( String file in files )
            {
                FoundFile( file );
            }

        } // End SearchForFiles

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
            
            // Get's the next path, in Queue
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
        } // End void Search()
        
    } // End Class

} // End Namespace
