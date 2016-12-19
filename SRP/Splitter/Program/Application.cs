using System;

using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using LexicalAnalysis;

namespace Splitter
{

    class Application
    {
        private Nuclear nuke;
        private Boolean Continue = true;

        public void initialise( String[] Arguments )
        {


        }

        public void start()
        {
            String path = @"C:\Users\Fract\Desktop\Workspace\Reuters";

            nuke = new Nuclear(path);

            nuke.Search();

            while( Continue )
            {
                Thread.Sleep( 250 );
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
