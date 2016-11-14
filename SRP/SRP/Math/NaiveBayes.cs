using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studieretningsproject.Math
{
    public static class NaiveBayes
    {
        public static double Prior( int SelectedDocuments, int TotalDocuments )
        {
            return (double)SelectedDocuments / (double)TotalDocuments;
        }

        


    }
}
