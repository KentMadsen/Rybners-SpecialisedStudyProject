//  ------------------------------------------------------------------------->
// Include Fields
using System;
using System.Collections.Generic;
using System.IO;

//  ------------------------------------------------------------------------->
/* Author      : Kent vejrup Madsen
   Type        : C#, 
                 CSharp
   
   Title       : Specialised Study Project - SSP

   Name        : SearchDirectories
   Description : 
*/

namespace Libraries.IO
{
    public class SearchDirectories : Abstracts.RecursiveDirectorySearch
    {
//  ------------------------------------------------------------------------->
// Accessors
        public List<String> ListOfFiles
        {
            get
            {
                return listOfFiles;
            }
        }

        public List<String> ListOfDirectories
        {
            get
            {
                return listOfDirectories;
            }
        }

        public Boolean ListCompleted
        {
            get
            {
                return this.Completed;
            }
        }

        public Boolean Debug
        {
            get
            {
                return iDebug;
            }
            set
            {
                iDebug = value;
            }
        }

//  ------------------------------------------------------------------------->
// Variables
        private List<String> listOfFiles       = new List<string>();
        private List<String> listOfDirectories = new List<string>();

        private Boolean iDebug = false;

//  ------------------------------------------------------------------------->
// Initialise
        public void Initialise()
        {
            this.TriggerOnDirectories = false;
            this.TriggerOnFiles       = true;
        }

//  ------------------------------------------------------------------------->
// Constructors
        public SearchDirectories()
        {
            Initialise();
        }

        public SearchDirectories( String s )
        {
            Initialise();

            this.QueuePath( s );
        }

//  ------------------------------------------------------------------------->
// Override
        protected override void FoundDirectory( string Directory )
        {
            if ( iDebug == true )
                Console.WriteLine( "Found:{0}", Directory );

            listOfDirectories.Add( Directory );
        }

        protected override void FoundFile( string File )
        {
            if (iDebug == true)
                Console.WriteLine("Found:{0}", File);

            listOfFiles.Add( File );
        }
    }
}
