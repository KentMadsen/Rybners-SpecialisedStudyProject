using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    public abstract class Commands
    {
        protected string[] Parameterise( string Parameter )
        {
            string[] retValue;

            if( Parameter.Contains( '=' ) )
            {
                retValue = Parameter.Split( '=' );
            }
            else
            {
                retValue = null;
            }

            return retValue;
        }

        protected abstract void ExecuteOrder( string input );

        public abstract void Initialise( string[] Arguments );

        public abstract void Run();

        public abstract void Clean();
        
    }
}
