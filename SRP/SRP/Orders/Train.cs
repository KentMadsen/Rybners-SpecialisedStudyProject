using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{

    class Train : Commands
    {

        public const string CommandValue = "train";

        public Train()
        {

        }

        // ----------------------------------------------------------------------------------- //
        public override void Initialise(  )
        {


            
        } // End Initialise


        // ----------------------------------------------------------------------------------- //
        public override void Run()
        {
            



        } // End Run
        
        // ----------------------------------------------------------------------------------- //
        public override void Clean()
        {


        } // End Clean

        public override string ToString()
        {
            return base.ToString();
        }

    } // End Class Train

} // End Namespace
