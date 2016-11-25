using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject
{
    public class GetOpts
    {
        public struct Option
        {
            public void Set( string Token, string Parameters )
            {
                SetIToken( Token );
                SetIParameters( Parameters );
            }

            public void SetIToken( string token )
            {
                Token = token;
            }

            public void SetIParameters( string parameters )
            {
                Parameters = parameters;
            }

            string Token;
            string Parameters;
        }

    }
}
