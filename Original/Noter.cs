using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP
{
    class Noter
    {
        /*
        Skal skrives om senere og smides tilbage i interface.cs
        Har lavet en fejl...
        Laplace smoothing
        https://class.coursera.org/nlp/lecture/26
        */

        /*
            // Frequency
            List<int> FrequencyAnalysisFolder = new List<int>();
            List<int> FrequencyAnalysisRelevant = new List<int>();

            // Resultater
            float result = 0;

            string Folder = programFolder + @"\target";

            string[] files = Directory.GetFiles( Folder );
            
            // Mængden af tekster i mappen, hvor ordene indgår
            for( int x = 0; 
                     x <= Tokens.Count -1; 
                     x ++ )
            {
                int Frequency = 0;

                foreach( string s in files )
                {
                    string read = File.ReadAllText( s );
                    
                    if ( read.Contains(Tokens[x]) )
                    {
                        Frequency ++;
                    }

                }
                if( Frequency == 0 )
                {
                    Frequency = 0;
                }

                FrequencyAnalysisFolder.Add( Frequency );
            } */

        //DataTable Words = DataBank.Tables["Words"];

        //string FilterExpression = "idDocument={0}";

        // Går gennem documenternes bag of word

        /*for ( int x = 0; 
                  x <= Tokens.Count -1; 
                  x ++ )
        {
            int Frequency = 0;

            // For hver bag of word, hvor id'et til dokumentet er lig med = d
            for( int d = 0; 
                     d <= IDs.Count -1;
                     d ++ )
            {
                DataRow[] Row = Words.Select( string.Format( FilterExpression, IDs[d] ) );

                foreach( DataRow R in Row )
                {
                    if( (string)R["Val"] == Tokens[x] )
                    {
                        Frequency ++;
                    }
                }
            }

            if( Frequency == 0 )
            {
                Frequency = 1;
            }

            FrequencyAnalysisRelevant.Add( Frequency );

        }*/
        /*
        // Totale antal af Dokumenter p(r) 
        // Bruges senere
        Relevant = (IDs.Count);

        Target = (files.Length);
        Total = (Target + Relevant);

        float pRelevant = ((float)Relevant / (float)Total);
        float pTarget = (1 - (float)pRelevant);

        List<float> FrequencyVsTarget = new List<float>();

        */
    }
}
