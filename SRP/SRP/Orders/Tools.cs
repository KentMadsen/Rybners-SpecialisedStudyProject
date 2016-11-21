using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

namespace Studieretningsproject.Orders
{
    class Tools : Commands
    {
        public const string CommandWord = "tools";

        // ------------------------------------------------------------------------------------ //
        public override void Initialise( string[] Arguments )
        {
            if ( Arguments.Length == 1 )
                return;

            for( int x = 0; 
                     x <= Arguments.Length - 1; 
                     x ++ )
            {
                string[] parameter = Parameterise( Arguments[x] );
                
            }

        }
        
        

        // ------------------------------------------------------------------------------------ //
        public override void Run()
        {

            

        } // End Run

        // ------------------------------------------------------------------------------------ //
        public override void Clean()
        {

        } // End Clean


    }
}
