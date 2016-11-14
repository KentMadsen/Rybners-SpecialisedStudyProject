using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    class Train : Commands
    {
        public override void Initialise( string[] Arguments )
        {
            if ( Arguments.Length == 1 )
                return;

            for( int x = 1; 
                     x <= Arguments.Length - 1; 
                     x ++ )
            {
                string[] parameter = Parameterise( Arguments[x] );

                Parameters( parameter[0], 
                            parameter[1] );
            }

        }

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
        }

        public override void Run()
        {

        }


        public override void Clean()
        {

        }

    } // End Class Train

} // End Namespace
