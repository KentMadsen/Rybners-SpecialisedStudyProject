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

namespace Libraries.IO.Abstracts
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
                // Public
        public int WaitMS
        {
            get
            {
                return iWaitMS;
            }
            set
            {
                iWaitMS = value;
            }
        }
        
        public Boolean useExtensionFilterForFiles
        {
            get
            {
                return iUseExtensionFilterForFiles;
            }
            set
            {
                iUseExtensionFilterForFiles = value;
            }
        }

        public String addExtensionFileFilter
        {
            set
            {
                if( value.Contains( '|' ) )
                {
                    string[] extensions = value.Split( '|' );

                    for( int i = 0; 
                             i <= extensions.Length - 1; 
                             i ++ )
                    {
                        string current = extensions[i];

                        if ( String.IsNullOrWhiteSpace( current ) )
                            continue;

                        current = current.Replace( " ", "" );

                        this.ExtensionFilter.Add( current );
                    }
                }
                else
                {
                    this.ExtensionFilter.Add( value );
                }
            }

        } // end addExtensionFileFilter

                // Protected
        protected List<String> OriginalSources
        {
            get
            {
                return this.SourceDirectories;
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

                // States
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
        }

        protected Boolean Completed
        {
            get
            {
                return iCompleted;
            }
        }

//  ------------------------------------------------------------------------->
// Variables
    // Main Variables
        
            // Filters
                // Triggers -> do something
        private Boolean iTriggerDirectories = false;
        private Boolean iTriggerFiles       = false;

                // Search States
        private Boolean iUseExtensionFilterForFiles = false;

                    // Operation
        private Boolean iCompleted      = false;
        private Boolean iExit           = false;
        private Boolean iErrorOccured   = false;
        private Boolean iPause          = false;
        
        // false : include
        // true  : exclude
        private Boolean iExcludeFilter = false;

            // Wait
        private int iWaitMS = 25;

        private List<String> SourceDirectories = new List<string>();
        private List<String> ExtensionFilter   = new List<string>();

            // Buffers
        private Queue<String> iPathBuffer = new Queue<String>();

            // Children
        private Thread iChildWorker;

//  ------------------------------------------------------------------------->
// Shared Class Functions
    // Queue
        protected void QueuePath( String Path )
        {
            SourceDirectories.Add( Path );

            AddDirectory( Path );
        }

        protected void QueuePaths( String[] Paths )
        {
            foreach ( String path in Paths )
            {
                QueuePath( path );
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

   // thread Options
        public void Run()
        {
            iChildWorker.Start();
        }

//  ------------------------------------------------------------------------->
// Private Functions
        // Clear Buffer
        private void Refresh()
        {
            iPathBuffer.Clear();
        } 
        
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


        // Searches for the Directories in a directory
        private void SearchForDirectories( String Current )
        {
            String[] directories = Directory.GetDirectories( Current );

            foreach ( String path in directories )
            {
                AddDirectory( path );
            }
        }

        // Searches for the files in a directory
        private void SearchForFiles( String Current )
        {
            if ( iTriggerFiles == false )
                return;

            String[] files = Directory.GetFiles( Current );

            foreach ( String filePath in files )
            {
                if( iUseExtensionFilterForFiles )
                {
                    if( isAmongAllowedExtensions( filePath ) )
                    {
                        if( iExcludeFilter != true )
                        {
                            FoundFile( filePath );
                        }
                    }
                    else
                    {
                        if( iExcludeFilter )
                            FoundFile( filePath );
                    }
                }
                else
                {
                    FoundFile( filePath );
                }
            }

        }

        private Boolean isAmongAllowedExtensions(String path)
        {
            int length = this.ExtensionFilter.Count;

            if ( length == 0 )
                return true;

            length = length - 1;

            String extension = Path.GetExtension(path);

            for ( int x = 0;
                      x <= length;
                      x++ )
            {
                String current = this.ExtensionFilter[x];

                if ( String.Equals( path, current ) )
                {
                    return true;
                }
            }

            return false;
        }


        // Recursive function, that searches a directory, 
        // for other directories and files. then proceeds to another 
        // in the queue
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
