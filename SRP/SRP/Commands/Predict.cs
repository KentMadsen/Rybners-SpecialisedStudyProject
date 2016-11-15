using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    class Predict : Commands
    {
        // Command Word
        public const string CommandValue = "predict";

        public override void Initialise( string[] Arguments )
        {

            if ( Arguments.Length == 1 )
                return;

            for ( int x = 1;
                      x <= Arguments.Length - 1;
                      x ++ )
            {
                string[] parameter = Parameterise( Arguments[x] );

                if( parameter == null )
                {
                    // Command

                }
                else
                {
                    Parameters( parameter[0], 
                                parameter[1] );
                }
            }

        }

        protected override void ExecuteOrder( string s )
        {

        }

        private void Parameters( string Identifier, 
                                 string Value )
        {
            switch( Identifier )
            {


                default:

                    break;
            }
        }

        public override void Run()
        {
            
        }

        public override void Clean()
        {
            
        }

    } // End Class

} // End Namespace
