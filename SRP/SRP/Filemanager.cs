using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

/*
 * Name   : 
 * Author : Kent v. Madsen
 * 
 * 
 * 
 */

namespace Studieretningsproject
{
    public class FileReadManager
    {
        // Internal Objects
        private struct Element
        {
            public int Identifier;
            public string Directory;
        }

        private struct Complete_Element
        {
            public Element Objects;
            public byte[] Data;
        }

        // Variables
        private Thread threadWorker;
        private bool threadAlive = false;

        private int InternalIterator = 0;

        List< Element > QueueList = new List< Element >();
        List< Complete_Element > ContentList = new List< Complete_Element >();

        // 
        public FileReadManager()
        {
            threadWorker = new Thread( Worker );
        }

        // Returns Identifier
        public int Queue( string directory )
        {
            // add to queue
            
            if( threadAlive == false )
            {
                threadWorker.Start();
            }

            InternalIterator++;

            Element e;
            e.Directory = directory;
            e.Identifier = InternalIterator;

            QueueList.Add( e );

            return e.Identifier;
        }

        // Returns Content, from a file
        public byte[] RetrieveContent( int id )
        {
            foreach( Complete_Element f in ContentList )
            {
                if( f.Objects.Identifier == id )
                {
                    return f.Data.ToArray();
                }
            }
            return null;
        }

        public void Worker()
        {
            Console.WriteLine( "Worker: " + System.Threading.Thread.CurrentThread );

            threadAlive = true;

            int sleep_iterator = 0;

            bool Continue = true;

            while( Continue )
            {
                bool work = true;
                bool skip = false;

                if( QueueList.Count == 0 )
                {
                    work = false;
                    skip = true;
                }

                if( skip == false )
                {
                    sleep_iterator = 0;

                    try
                    {
                        Element input = QueueList[0];
                        RemoveObject();

                        Byte[] data = System.IO.File.ReadAllBytes( input.Directory );

                        AddContentObject( input, 
                                          data );

                    }
                    catch( Exception ie )
                    {

                    }

                }

                if ( work == false )
                {
                    sleep_iterator++;

                    Thread.Sleep( 50 );
                }

                if ( sleep_iterator == 100 )
                {
                    Continue = false;
                }

            }

            threadAlive = false;
        } // End Worker

        private void RemoveObject()
        {
            QueueList.RemoveAt( 0 );
        }

        private void AddContentObject( Element IN_E, 
                                       byte[] Data )
        {
            Complete_Element retValue = new Complete_Element();

            retValue.Objects = IN_E;
            retValue.Data = Data;

            ContentList.Add( retValue );
        }

    } // end Filemanager

} // End namespace
