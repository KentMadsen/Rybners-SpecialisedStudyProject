﻿//---------------------------------------------------------------------------->
// Include Fields
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//  ------------------------------------------------------------------------->
/* Author      : Kent vejrup Madsen
   Type        : C#, 
                 CSharp
   
   Title       : Specialised Study Project - SSP

   Name        : Application
   Description : 
*/

namespace Program
{

    class Application
    {
//---------------------------------------------------------------------------->
// Constructors
        public Application()
        {

        }

//---------------------------------------------------------------------------->
// Variables
    // States
        private Boolean ErrorOccured = false;


//---------------------------------------------------------------------------->
        // Tags: Initialising
        public void Init( String[] Arguments )
        {

        }

//---------------------------------------------------------------------------->
// Tags: Run
        public void Run()
        {
            String Path = @"C:\work\DataSet\NSF Research Award Abstracts";

            Libraries.IO.SearchDirectories SDirectories = new Libraries.IO.SearchDirectories( Path );

            SDirectories.useExtensionFilterForFiles = true;
            SDirectories.FileExtensionFilter = ".txt";

            SDirectories.Debug = true;

            SDirectories.Run();

            while (true)
                Thread.Sleep( 250 );
        }

//---------------------------------------------------------------------------->
// Tags: End
        public void End()
        {
            if( ErrorOccured )
            {

            }
        }

//---------------------------------------------------------------------------->
// Tags: Cleaning
        public void Clear()
        {

        }

    } // End Class : Application

} // End Namespace : Program
