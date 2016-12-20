using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management
{
    class Program
    {
        static void Main( string[] args )
        {
            Application app = new Application();

            app.initialise( args );

            app.start();

            app.end();

            app.clear();
        }
    }
}
