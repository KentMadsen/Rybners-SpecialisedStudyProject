﻿using System;
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
        static Orders.Commands command;
        static GetOptions Options;

        // Program States
        static bool Empty = true;

        static bool CommandInput = true;

        static bool Continue = false;
        static bool Skip = false;
        
        static void Main( string[] Arguments )
        {
            if ( Arguments.Length == 0 )
            {
                CommandInput = true;
                Continue = true;
            }

            Options = new GetOptions();
            
            Console.WriteLine("Input Command");

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

                switch( Current.t )
                {
                    case GetOptions.Type.Command:
                            Intpre_Commands( Current );
                        break;

                    case GetOptions.Type.ShortOption:
                            Intpre_Options( Current );
                        break;

                    case GetOptions.Type.Unknown:
                        goto default;

                    default:
                        // Unknown
                        break;
                }

            }
            

        } // End Interprete

        static void Intpre_Commands( GetOptions.Container Commands )
        {

            switch( Commands.Token )
            {
                case "quit":
                        Continue = false;
                    break;
            }

        }

        static void Intpre_Options( GetOptions.Container Commands )
        {

        }

        static void Cycle()
        {

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
        
        static void Initialise( )
        {
            command.Initialise();
        } // End Initialise()

        //
        static void Run()
        {
            command.Run();
        } // End Run()

        //
        static void Clean()
        {
            command.Clean();
        } // End Clean()
        
    } // End Class Program

} // End namespace
