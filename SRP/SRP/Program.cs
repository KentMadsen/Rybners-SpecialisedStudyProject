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

        static Commands command = null;

        static Boolean Activate = false;
        static FileReadManager Reader;

        static void Main( string[] Arguments )
        {
            Reader = new FileReadManager();

            // Initialise Command
            Initialise( Arguments );

            // If nothing is chosen, exit.
            if ( command == null )
            {
                // Exit
                return;
            }

            // Run chosen command
            Run();

            // Clean Exit
            Clean();
        }

        static void Initialise( string[] Arguments )
        {

            if ( Arguments.Length == 0 )
                return;

            switch( Arguments[0] )
            {
                case Predict.CommandValue:
                        Predict predict = new Predict();
                        predict.Initialise( Arguments );
                    break;

                case Train.CommandValue:
                        Train train = new Train();
                        train.Initialise( Arguments );
                    break;

                default: 

                    break;
            }

        }

        static void Run()
        {
            if ( Activate == true )
                command.Run();

        }

        static void Clean()
        {
            command.Clean();
        }

    } // End Class Program

} // End namespace
