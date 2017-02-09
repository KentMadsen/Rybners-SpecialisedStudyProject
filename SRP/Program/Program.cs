/*
    Summary : Hoved
    Title   : Program.cs
    Author  : Kent v. Madsen
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP
{
    class Program
    {
        static string programDirectory;
        
        static void Main( string[] args )
        {
            Console.ForegroundColor = ConsoleColor.White;

            // Start Variabler
            bool work = true;
            
            string command;
            string DatabaseName = "data.accdb";
            
            // Interface til tekst klassifieren
            Interface ui = new Interface();

            // Init
            Console.Title = "Studieretnings Projekt - Tekst Klassificering - Kent v. Madsen";
            
            // This Directory
            programDirectory = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );

            Database db = new Database( programDirectory, 
                                        DatabaseName );

            db.TestConnection();
            
            // Work
            while ( work )
            { 
                Console.Write(" >> ");
                command = Console.ReadLine();

                // Tokenizer teksten, efter det er blevet gjordt til uncapitalized.
                string[] input = command.ToLower().Split(' ');

                // Første Felt
                switch ( input[0] )
                {   
                    default:
                            Console.WriteLine( "Error: Try Again..." );

                        break;

                        // bruges til at lave en ny række eller katerogi for sig selv.
                    case "create":
                        if( input[1] == "category" )
                        {
                            int i = db.CreateCategory( input[2] );

                            Console.WriteLine( "'{0}' : {1}", 
                                               i, 
                                               input[2] );

                        }


                        break;

                    // Første Felt//

                    case "show":

                        if( input[1] == "categories" || input[1] == "category" )
                        {
                            // Do something
                        }

                        break;


                        // Henter filer og sætter dem som documenter i databasen
                    case "load":
                        if ( ui.safe == true )
                        {
                            Functions.Load( programDirectory,
                                            DatabaseName );
                        }
                        else
                        {
                            Console.WriteLine( "Locked" );
                        }

                        break;

                    // Første Felt//

                    // brugeren sætter dokumenter i en category
                    case "categorize":

                        if( ui.safe == true )
                            {
                                Functions.Categorize( db );
                            }
                        else
                            {
                                Console.WriteLine("Locked");
                            }

                        break;
                        
                    case "tokenize":

                            Functions.WordCollecting( db );

                        break;

                    // Første Felt //

                    // typer af metoder
                    case "analyser":
                        
                        switch( input[1] )
                        {
                            // analyser type command -arg1 -arg2
                            case "naive":

                                if( input[2] == "start" )
                                {

                                    if ( ui.safe == true )
                                    {
                                        ui = new Naive( programDirectory,
                                                        DatabaseName );
                                        
                                        ui.run();
                                        
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Locked");
                                    }

                                }
                                
                                break;
                                
                            default:

                                break;
                        }
                        
                        break;

                    // Første Felt //

                    // README.txt
                    case "help":
                        if( Functions.Help( programDirectory ) == -1 )
                        {
                            Console.WriteLine( "[ERROR]: Does README.txt exist ?" );
                        }

                        break;
                        
                        // Bryder loopen
                    case "exit":
                            work = false;
                        break;
                        
                }

                // Exit
            }
            
        }

    }

}
