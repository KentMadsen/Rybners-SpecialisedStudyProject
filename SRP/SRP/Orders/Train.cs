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

                if( parameter.Length == 0 )
                {
                    // Command
                    ExecuteOrder( Arguments[x] );
                }
                else
                {
                    Parameters( parameter[0], 
                                parameter[1] );
                }

            }

        } // End Initialise

        // What to do
        protected override void ExecuteOrder( string Identifier )
        {
            Console.WriteLine( "Train : ExecuteOrder" );

            if( CommandIssued == false )
            {

                switch( Identifier )
                {
                    // train
                    case "run":
                            Set = Modes.Train;
                        break;

                    // upload filter words
                    case "train-filter":
                            Set = Modes.Filter;
                        break;
                        
                    // doing nothing
                    default:
                            Set = Modes.None;
                        break;
                }

                CommandIssued = true;
            }

        } // End Order

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
