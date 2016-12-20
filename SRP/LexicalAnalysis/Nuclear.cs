using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LexicalAnalysis;

namespace LexicalAnalysis
{
    /* 
       Author : Kent v. Madsen
       Description :
     
       
     */
    public class Nuclear : IO.RecursiveSearch
    {
                
//--------------------------------------------------------------------------->
// Variables
        private Tokenizer tokenBuilder = new Tokenizer();
        private Thread childSearcher;

//--------------------------------------------------------------------------->
// Initialise
        public void Initialise()
        {
            this.triggerDirectories = false;
            this.triggerFiles       = true;

            this.childSearcher = new Thread( this.Start );
        }

        public void Initialise( String Path )
        {
            Initialise();

            this.QueuePath( Path );
        }

        public void Initialise( String[] Paths )
        {
            Initialise();

            this.QueuePaths( Paths );
        }

        public Dictionary<String, String> Token
        {
            get
            {
                return tokenBuilder.Library;
            }
        }

//--------------------------------------------------------------------------->
// Constructors
        public Nuclear()
        {
            Initialise();
        }

        public Nuclear( String Path )
        {
            Initialise( Path );
            
        }

        public Nuclear( String[] Paths )
        {
            Initialise();

            this.QueuePaths( Paths );
        }

//--------------------------------------------------------------------------->
// Public Functions
        private void Start()
        {
            this.Run();
        }

        private void Pause()
        {
            this.pause = true;
        }

        private void Continuing()
        {
            this.pause = false;
        }

        private void Exit()
        {
            this.exit = true;
        }

//--------------------------------------------------------------------------->
// Overriding functions
        protected override void FoundDirectory( string Directory )
        {
            Flow( Directory, Types.TypeWork.Directory );
        }

        protected override void FoundFile( string File )
        {
            Flow( File, Types.TypeWork.File );
        }

//--------------------------------------------------------------------------->
// Threads Functions
        public void Search()
        {
            this.childSearcher.Start();
        }

        public Boolean SearchStatus()
        {
            return this.childSearcher.IsAlive;
        }
        
        public Boolean isBackground()
        {
            return this.childSearcher.IsBackground;
        }

//--------------------------------------------------------------------------->
// work
        private void Flow( String Path, 
                           Types.TypeWork type )
        {
            switch( type )
            {
                case Types.TypeWork.Directory:
                        DirectoryFlow( Path );
                    break;

                case Types.TypeWork.File:
                        FileFlow( Path );
                    break;

                case Types.TypeWork.Unknown:
                    break;

                default:
                    break;
            }
        }

        private void FileFlow( String File )
        {
            try
            {
                using ( StreamReader sr = new StreamReader( File ) )
                {
                    String line;

                    while( sr.Peek() >= 0 )
                    {
                        line = sr.ReadLine();

                        tokenBuilder.Parse( line );

                    }

                }
                
            }
            catch(Exception ex)
            {

            }
            
        }
        
        private static void DirectoryFlow( String Directory )
        {

        }

        //

        private static class Types
        {
            public enum TypeWork
            {
                Directory,
                File,
                Unknown
            }
            
        } // End Work

    } // End Class

} // End Namespace
