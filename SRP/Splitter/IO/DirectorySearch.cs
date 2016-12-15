﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitter
{
    class DirectorySearch
    {
        // Accessors
        List< String > foundDirectories
        {
            get
            {
                return DirectoriesFound;
            }
        }

        List< String > foundFiles
        {
            get
            {
                return FilesFound;
            }
        }

        // Variables
        private Boolean Debug                = false;

            // Main Variables
        private String rootDirectory;

            // Filters
        private Boolean FilterForDirectories = false;
        private Boolean FilterForFiles       = false;

            // List
        private List< String > DirectoriesFound = new List< String > ();
        private List< String > FilesFound       = new List< String > ();

            // Buffers
        private List< String > CurrentBuffer = new List< String > ();

        // Constructor
        protected DirectorySearch( String root )
        {
            rootDirectory = root;
        }
        
        // Functions
        protected void run()
        {
            refresh();

            addDirectories( rootDirectory );

            recursiveSearch();
        }
        
        private String getCurrentString()
        {
            String current = CurrentBuffer[0];
            CurrentBuffer.RemoveAt(0);

            return current;
        }

        private void recursiveSearch()
        {
            String current = getCurrentString();

            searchForDirectories( current );

            output( current );

            searchForFiles( current );

            if( CurrentBuffer.Count != 0 )
                recursiveSearch();
        }

        private void addDirectories( String path )
        {
            if ( Directory.Exists( path ) == false )
                return;

            if ( FilterForFiles == false )
                DirectoriesFound.Add( path );

            CurrentBuffer.Add( path );

        }

        private void searchForDirectories( String current )
        {

            String[] directories = Directory.GetDirectories( current );

            foreach( String s in directories )
            {
                CurrentBuffer.Add( s );
            }
        }
        
        private void searchForFiles( String current )
        {
            if ( FilterForDirectories == true )
                return;

            String[] files = Directory.GetFiles( current );

            foreach( String s in files )
            {
                addFiles( s );
            }

        }

        private void addFiles( String path )
        {
            FilesFound.Add( path );
        }

        private void refresh()
        {
            DirectoriesFound.Clear();
            FilesFound.Clear();
        }

        private void output( String s )
        {
            if( Debug == true )
            {
                Console.WriteLine( "Currently in : {0}", 
                                    s );
            }
        }

    }

}