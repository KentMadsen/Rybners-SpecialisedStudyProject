using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    class Tools : Commands
    {
        private bool Selected = false;

        private enum Modes
        {
            Files,
            Data,
            Table,
            None
        }

        private Modes currentMode = Modes.None;

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

                if( parameter.Length == 0 )
                {
                    if ( Selected == false )
                    {
                        ExecuteOrder( Arguments[x] );

                        Selected = true;
                    }
                }
                else
                {
                    Parameters( parameter[0], 
                                parameter[1] );
                }

            }

        }

        // ------------------------------------------------------------------------------------ //
        private void Parameters( string Identifier, 
                                 string Value )
        {

            switch( currentMode )
            {
                case Modes.None:

                    break;

                case Modes.Data:
                        Data_Parameters( Identifier,
                                         Value );
                    break;

                case Modes.Files:
                        Files_Parameters( Identifier,
                                          Value );
                    break;

                case Modes.Table:
                        Table_Parameters( Identifier,
                                          Value );
                    break;
            }
            
        } // End Parameters

        private void Data_Parameters( string Identifier,
                                      string Value )
        {

            switch ( Identifier )
            {
                case "":

                    break;

                default:

                    break;
            }

        } // End Data_Parameters

        private void Files_Parameters( string Identifier,
                                       string Value )
        {

            switch ( Identifier )
            {
                case "":

                    break;

                default:

                    break;
            }

        } // End Files_Parameters

        private void Table_Parameters( string Identifier,
                                       string Value )
        {
            switch( Identifier )
            {
                case "":

                    break;

                default:

                    break;
            }
        } // End Table_Parameters

        protected override void ExecuteOrder( string input )
        {

            switch( input )
            {

                case "data":
                        currentMode = Modes.Data;
                    break;
                    
                case "files":
                        currentMode = Modes.Files;
                    break;

                case "table":
                        currentMode = Modes.Table;
                    break;

                default:
                    // Unknown Command
                        currentMode = Modes.None;
                    break;
            }

        } // End ExecuteOrder

        // ------------------------------------------------------------------------------------ //
        public override void Run()
        {

            switch( currentMode )
            {

                case Modes.None:
                        // Nothing
                    break;
                    
                case Modes.Data:
                        Data();
                    break;

                case Modes.Files:
                        Files();
                    break;

                case Modes.Table:
                        Tables();
                    break;

                default:
                        // Nothing
                    break;
            }

        } // End Run
        
        private void Files()
        {

        } // End Files

        private void Data()
        {

        } // End Data

        private void Tables()
        {

        } // End Tables

        // ------------------------------------------------------------------------------------ //
        public override void Clean()
        {

        } // End Clean


    }
}
