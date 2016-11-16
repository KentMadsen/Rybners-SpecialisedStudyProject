using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    class Train : Commands
    {
        public Train()
        {

        }

        // Command Word
        public const string CommandValue = "train";

        FileReadManager fManager = new FileReadManager();

        public override void Initialise( string[] Arguments )
        {
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

        // What to do,
        protected override void ExecuteOrder( string Identifier )
        {
            switch( Identifier )
            {
                case "":

                    break;

                default:

                    break;
            }
        } // End Order

        // Parameters
        private void Parameters( string Identifier, 
                                 string Value )
        {
            switch( Identifier )
            {
                case "":

                    break;

                case " ":

                    break;

                default:

                    break;
            }

        } // End Parameters

        public override void Run()
        {

        } // End Run


        public override void Clean()
        {

        } // End Clean

    } // End Class Train

} // End Namespace
