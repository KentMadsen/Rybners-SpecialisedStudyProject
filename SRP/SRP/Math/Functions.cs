using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Math
{
    public static class Functions
    {
        // Laplace smoothing -> Also called Adaptive Smoothing
        public static double LaplaceSmoothing( double Xi,
                                               double a,
                                               double N, 
                                               double ad )
        {   
            return ( ( ( Xi + a ) ) /
                     ( ( N ) + ad ) );
        }
        
    }
}
