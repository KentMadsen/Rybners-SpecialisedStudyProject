using System;

using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace Splitter
{

    class Application
    {
        private Boolean Continue = true;

        public void initialise( String[] Arguments )
        {
            checkDirectories();


        }

        public void checkDirectories()
        {
            if( IO.Directories.Exist( Misc.Dump ) == false )
            {
                IO.Directories.Create( Misc.Dump );
            }

            if( IO.Directories.Exist( Misc.Tokens ) == false )
            {
                IO.Directories.Create( Misc.Tokens );
            }
        }

        public void start()
        {
            TextSplitter splitter = new TextSplitter( @"C:\Users\Fract\Desktop\Workspace\Reuters" );
            splitter.Initialise();
            splitter.ReadFiles();


            while( Continue )
            {

            }

        }

        // Last minute actions
        public void end()
        {

        }

        // clear memory, etc.
        public void clear()
        {

        }
        
    }

}
