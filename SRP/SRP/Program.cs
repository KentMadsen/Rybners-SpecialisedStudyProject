using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
   Author  : Kent vejrup Madsen
  
   Place   : Rybners Gymnasium - Højere Tekniske Gymnasium (HTX)

   Project : Studieretnings Project
             Specialised Study Project

   Subject : Tekst Klassificering, 
             Text Classifying
   
   Note    : Rewritten 
 */

// Naive Insert Document
namespace Studieretningsproject
{

    class Program
    {
        static Orders.Commands Command;

        static GetOptions Options;


        // Program States
        static bool Empty = true;

        static bool CommandInput = true;

        static bool Continue = false;
        static bool Skip = false;

        static List<GetOptions.Container> ListOfOptions = new List<GetOptions.Container>();

        static void Main( string[] Arguments )
        {
            if ( Arguments.Length == 0 )
            {
                CommandInput = true;
                Continue = true;
            }

            Options = new GetOptions();
            
            while ( Continue )
            {

                if( Skip == false )
                {

                    if( CommandInput == false )
                    {
                        // Arguments first

                    }
                    else
                    {

                        // Commandline
                        Console.Write( ">>> " );
                        String UserCom = Console.ReadLine();

                        GetOptions.Container[] conArray = Options.Parsed( UserCom );

                        // Interprete Commands
                        Interprete( conArray );

                    }

                }

                // Do the job
                Cycle();

            }
            
        } // End Main

        static void Interprete( GetOptions.Container[] Commands )
        {

            for( int i = 0; 
                     i <= Commands.Length - 1; 
                     i ++ )
            {
                GetOptions.Container Current = Commands[i];

                switch( Current.Type )
                {

                    // | ------ Commands ------ |
                    case GetOptions.Types.Command:
                            Intpre_Commands( Current );
                        break;

                    // | ------ Options ------ | 
                    case GetOptions.Types.ShortOption:
                            ListOfOptions.Add( Current );
                        break;

                    case GetOptions.Types.LongOption:
                        goto case GetOptions.Types.ShortOption;
                        
                    // | ------ Unknown ------ |
                    case GetOptions.Types.Unknown:
                        goto default;

                    default:
                        break;
                }

            }
            
        } // End Interprete

        static void Intpre_Commands( GetOptions.Container Commands )
        {
            // Retrieve commands
            GetOptions.Container[] options = null;

            // Continue
            switch( Commands.Token.ToLower() )
            {
                // | ------ Main Commands ------ |
                case "status":

                    break;

                case "help":

                    break;

                case "version":

                    break;

                case "quit":
                        Continue = false;
                    break;

                // | ------ Jobs ------ |
                case "predict":
                        Orders.Predict commnad_predict = new Orders.Predict();

                        AssignOrder( commnad_predict, 
                                     options );
                    break;

                case "train":
                        Orders.Train commnad_train = new Orders.Train();

                        AssignOrder( commnad_train, 
                                     options );
                    break;
            }

        }

        static void Cycle()
        {
            // if empty, don't do anything
            if ( Empty != true )
            {
                // Doing the job
                    // Initialise Command
                Initialise();

                    // Run chosen command
                Run();

                    // Clean Exit
                Clean();
            }
            
        }

        static void AssignOrder ( Orders.Commands command, 
                                  GetOptions.Container[] options )
        {
            // Tells program, it isn't empty
            Empty = false;

            // Assign user chosen command
            Command = command;

            // If null, don't assign it
            if ( options != null )
            {
                Command.Options( options );
            }

        } // End AssignOrder
        
        static void Initialise( )
        {
            Command.Initialise();
        } // End Initialise()

        //
        static void Run()
        {
            Command.Run();
        } // End Run()

        //
        static void Clean()
        {
            Command.Clean();
        } // End Clean()
        
    } // End Class Program

} // End namespace
