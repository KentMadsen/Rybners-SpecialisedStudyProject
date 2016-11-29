using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Orders
{
    public abstract class Commands
    {
        public abstract void Options( GetOptions.Container[] Options );

        public abstract void Initialise(  );

        public abstract void Run();

        public abstract void Clean();
        
    }
}
