using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{

    class Program
    {
        static Application App = new Application();

        static void Main( string[] args )
        {

            App.Init( args );

            App.Run();

            App.End();

            App.Clear();

        } // End void Main

    } // End class Program

} // End Namespace
