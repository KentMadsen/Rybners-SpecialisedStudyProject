using System;
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

        public override void Options( GetOptions.Container[] Options )
        {
            for (int x = 0; x <= Options.Length - 1; x++)
                Console.WriteLine(Options[x].Token);
        }

        //
        public override void Initialise(  )
        {


        } // Initialise
        
        public override void Run()
        {


        } // Run

        public override void Clean()
        {

        } // Clean

        public override string ToString()
        {
            return base.ToString();
        }
    } // End Class

} // End Namespace
