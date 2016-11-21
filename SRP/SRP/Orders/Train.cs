using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{

    class Train : Commands
    {

        enum Modes
        {
            Train,
            Filter,
            None
        }

        private Modes Set;
        private bool CommandIssued = false;

        public Train()
        {

        }

        // Command Word
        public const string CommandValue = "train";

        // ----------------------------------------------------------------------------------- //
        public override void Initialise( string[] Arguments )
        {
            Console.WriteLine( "Train : Initialise" );

            if ( Arguments.Length == 1 )
                return;

            for( int x = 1; 
                     x <= Arguments.Length - 1; 
                     x ++ )
            {
                string[] parameter = Parameterise( Arguments[x] );


            }

        } // End Initialise

  

        // Parameters
        private void Parameters( string Identifier, 
                                 string Value )
        {
            Console.WriteLine( "Train : Parameters" );

            switch( Identifier )
            {
                case "":

                    break;
                    
                default:

                    break;
            }

        } // End Parameters

        // ----------------------------------------------------------------------------------- //
        public override void Run()
        {
            Console.WriteLine( "Train : Run" );

            switch( Set )
            {
                case Modes.Filter:
                        MFilter();
                    break;

                case Modes.Train:
                        MTrain();
                    break;

                case Modes.None:
                        // What it says on package
                    break;
            }

        } // End Run

        private void MFilter()
        {

        }

        private void MTrain()
        {

        }


        // ----------------------------------------------------------------------------------- //
        public override void Clean()
        {
            Console.WriteLine( "Train: Clean" );

        } // End Clean

    } // End Class Train

} // End Namespace
