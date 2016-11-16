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

        private bool threadAlive
        {
            get;
            set;
        }

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

        // Thread
        public void Worker()
        {

            Console.WriteLine( "Worker: " + 
                                Thread.CurrentThread );

            threadAlive = true;

            int sleep_iterator = 0;
            int sleep_rotation = 0;

            bool Continue = true;

            while( Continue )
            {
                bool skip = false;

                if( QueueList.Count == 0 )
                {
                    skip = true;
                }

                if( skip == false )
                {
                    sleep_iterator = 0;
                    sleep_rotation = 0;

                    try
                    {
                        Element input = RetrieveObject();
                        RemoveObject();

                        Byte[] data = System.IO.File.ReadAllBytes( input.Directory );

                        AddContentObject( input, data );

                    }
                    catch( Exception ie )
                    {
                        Console.WriteLine( "Error" );
                    }

                }

                if ( skip == true )
                {
                    if( sleep_rotation < 50 )
                    {
                        sleep_rotation ++;
                    }

                    if( sleep_rotation == 50 )
                    {
                        sleep_iterator++;

                        Thread.Sleep( 50 );
                    }
                }

                if ( sleep_iterator == 100 )
                {
                    Continue = false;
                }

            }

            threadAlive = false;
        } // End Worker

        private Element RetrieveObject()
        {
            return QueueList[ 0 ];
        } // End Retrieve

        private void RemoveObject()
        {
            QueueList.RemoveAt( 0 );
        } // End RemoveObject

        private void AddContentObject( Element IN_E, 
                                       byte[] Data )
        {
            Complete_Element retValue = new Complete_Element();

            retValue.Objects = IN_E;
            retValue.Data = Data;

            ContentList.Add( retValue );
        } // End AddContentObject

    } // end Filemanager

} // End namespace
