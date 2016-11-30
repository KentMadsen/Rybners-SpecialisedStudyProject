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

        static GetOptions gOptions;


        // Program States
        static bool Empty = true;

        static bool CommandInput = true;

        static bool Continue = false;
        static bool Skip = false;

        static bool Complete = true;

        static List<GetOptions.Container> ListOfOptions = new List<GetOptions.Container>();

        static void Main( string[] Arguments )
        {
            if ( Arguments.Length == 0 )
            {
                CommandInput = true;
                Continue = true;
            }

            gOptions = new GetOptions();
            
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

                        GetOptions.Container[] conArray = gOptions.Parsed( UserCom );

                        // Interprete Commands
                        Interprete( conArray );

                        ListOfOptions.Clear();

                    }

                }
                
            }
            
        } // End Main

        static void Interprete( GetOptions.Container[] Commands )
        {

            for( int i = 0; 
                     i <= Commands.Length - 1; 
                     i ++ )
            {
                GetOptions.Container Current = Commands[i];
                
                switch ( Current.Type )
                {
                    
                    // | ------ Commands ------ |
                    case GetOptions.Types.Command:
                            Execute();
                        
                            Intpre_Commands( Current );
                        break;

                    case GetOptions.Types.LongOption:
                        goto SO;

                    case GetOptions.Types.ShortOption:
                        SO:
                            ListOfOptions.Add( Current );
                        break;
                        
                    // | ------ Unknown ------ |
                    case GetOptions.Types.Unknown:
                        goto default;

                    default:
                        break;
                }

            }
            
            Execute();
            
            
        } // End Interprete

        static void Execute()
        {
            if( Complete != true )
            {
                // Do the job
                Cycle();
            }
        }

        static void Intpre_Commands( GetOptions.Container Commands )
        {
            // Continue
            switch( Commands.Token.ToLower() )
            {
                // | ------ Main Commands ------ |
                case "help":
                        help:

                    break;

                case "version":
                        version:

                    break;
                    
                case "quit":
                            quit:
                        Continue = false;
                    break;

                // | ------ Jobs ------ |
                case "predict":
                        Complete = false;

                        Orders.Predict commnad_predict = new Orders.Predict();

                        AssignOrder( commnad_predict );
                    break;

                case "train":
                        Complete = false;

                        Orders.Train commnad_train = new Orders.Train();

                        AssignOrder( commnad_train );
                    break;

                // | ------ Fast ------ |
                case "v":
                    goto version;

                case "q":
                    goto quit;

                case "h":
                    goto help;
            }

        }

        static void AssignOrder ( Orders.Commands command )
        {
            // Tells program, it isn't empty
            Empty = false;

            // Assign user chosen command
            Command = command;

        } // End AssignOrder
        
        static void Cycle()
        {

            // if empty, don't do anything
            if ( Empty != true )
            {
                // Doing the job
                    // Insert Options
                Options();

                    // Initialise Command
                Initialise();

                    // Run chosen command
                Run();

                    // Clean Exit
                Clean();

                Complete = true;
            }
            
        }

        // ------------------------------------ //
        static void Options()
        {
            Command.Options( ListOfOptions.ToArray() );
        }

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
