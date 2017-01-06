//---------------------------------------------------------------------------->
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
// Variables
    // States
        private Boolean ErrorOccured = false;

        private Dictionary.Library DicLib = new Dictionary.Library();
        
//---------------------------------------------------------------------------->
// Constructors
        public Application()
        {

        }
        
//---------------------------------------------------------------------------->
        // Tags: Initialising
        public void Init( String[] Arguments )
        {
            
        }

//---------------------------------------------------------------------------->
// Tags: Run
        public void Run()
        {

            while (true) ;
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
