﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    class Predict : Commands
    {
        public const string CommandValue = "predict";

        public Predict()
        {
            
        }
        
        //
        public override void Initialise( string[] Arguments )
        {
            if ( Arguments.Length == 1 )
                return;



        } // Initialise
        
        public override void Run()
        {


        } // Run

        public override void Clean()
        {

        } // Clean

    } // End Class

} // End Namespace