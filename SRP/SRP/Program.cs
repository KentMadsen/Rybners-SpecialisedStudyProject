using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Author : Kent vejrup Madsen
// Source : Studieretnings Projekt
// Place  : Rybners Gymnasium - Højere Tekniske Gymnasium
// Name   : Tekst Klassificering, Text Classifying
// Note   : Rewritten


// Naive Insert Document
namespace Studieretningsproject
{

    class Program
    {
        static Commands command;

        static Boolean Activate = false;

        static void Main( string[] args )
        {
            Initialise( args );

            Run();

            Clean();
        }

        static void Initialise( string[] arguments )
        {
            switch( arguments[0] )
            {
            case "predict":
                    Predict predict = new Predict();
                    predict.Initialise( arguments );

                    break;

            case "train":
                    Train train = new Train();
                    train.Initialise( arguments );

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

    }

}
