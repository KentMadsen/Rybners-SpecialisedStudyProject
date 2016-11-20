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

        static Boolean CommandEmpty = false;
        static Boolean DebugBreak   = true;
        
        static void Main( string[] Arguments )
        {
            Console.Write("|Studieretnings Project : Naive Bayes ---------------------------------------------------------------------------------|");
            
            foreach( String s in Arguments )
            {
                Console.WriteLine( "Argument : {0}", 
                                    s );
            }
            
            // Initialise Command
            Initialise( Arguments );
            
            // If nothing is chosen, exit.
            if ( CommandEmpty == true )
            {
                // Exit
                return;
            }

            DebugBreakpoint();

            // Run chosen command
            Run();

            DebugBreakpoint();

            // Clean Exit
            Clean();

            DebugBreakpoint();

        } // End Main

        static void Initialise( string[] Arguments )
        {
            //
            if ( Arguments.Length == 0 )
                return;

            //
            switch( Arguments[0].ToLower() )
            {
                //
                case Orders.Predict.CommandValue:
                        Orders.Predict predict = new Orders.Predict();
                        predict.Initialise( Arguments );

                        Command = predict;
                    break;

                //
                case Orders.Train.CommandValue:
                        Orders.Train train = new Orders.Train();
                        train.Initialise( Arguments );

                        Command = train;
                    break;

                // 
                case Orders.Tools.CommandWord:
                        Orders.Tools tool = new Orders.Tools();
                        tool.Initialise( Arguments );

                        Command = tool;
                    break;

                // 
                default:
                        CommandEmpty = true;
                    break;
            }

        } // End Initialise()

        //
        static void Run()
        {
            if ( CommandEmpty == false )
                Command.Run();
        } // End Run()

        //
        static void Clean()
        {
            Command.Clean();
        } // End Clean()

        static void DebugBreakpoint()
        {
            if ( DebugBreak == true )
            {
                Console.WriteLine( "Click a key, to continue the process" );

                Console.ReadKey();
            }
        } // End DebugBreakpoint()

    } // End Class Program

} // End namespace
