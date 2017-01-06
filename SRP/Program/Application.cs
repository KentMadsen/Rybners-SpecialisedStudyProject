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
            int x = 0;

            while(x < 5)
            {
                Console.WriteLine("x={0}", x);
                x++;
            }

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
