using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    class Predict : Commands
    {
        public Predict()
        {
            
        }

        // Command Word
        public const string CommandValue = "predict";

        //
        public override void Initialise( string[] Arguments )
        {
            Console.WriteLine( "Predict: Initialise" );

            if ( Arguments.Length == 1 )
                return;

            for ( int x = 1;
                      x <= Arguments.Length - 1;
                      x ++ )
            {
                string[] parameter = Parameterise( Arguments[x].ToLower() );

                if( parameter == null )
                {
                    // Command

                }
                else
                {
                    Parameters( parameter[0].ToLower(), 
                                parameter[1].ToLower() );
                }

            }

        } // Initialise


        private void Parameters( string Identifier, 
                                 string Value )
        {

            Console.WriteLine( "Predict: Parameters [{0}, {1}]", 
                               Identifier, Value );

            switch ( Identifier )
            {


                default:

                    break;
            }

        } // Parameters

        public override void Run()
        {
            Console.WriteLine( "Predict: Run" );

        } // Run

        public override void Clean()
        {
            Console.WriteLine( "Predict: Clean" );
        } // Clean

    } // End Class

} // End Namespace
