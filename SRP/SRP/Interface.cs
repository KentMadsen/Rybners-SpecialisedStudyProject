/*
    Summary : Functioner til programmet
    Title   : Interface.cs
    Author  : Kent v. Madsen
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SRP
{
    class Interface
    {
        public bool safe = true;
        

        virtual public void run()
        {

            Console.WriteLine( "Interface: Empty" );

        }
        
    }

    class Naive:Interface
    {
        private System.Threading.Thread slaveThread;

        private string programFolder;
        private string databaseName;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data ";
        
        
        // Thread Variables
        public int Done = 0;



        public Naive( String ProgramFolder, 
                      String DatabaseName )
        {
            programFolder = ProgramFolder;
            databaseName = DatabaseName;
        }
        
        private int Initialise()
        {
            if( Directory.Exists( programFolder + @"\target" ) )
            {
                // exist
                return 1;
            }
            else
            {
                try
                { 
                    Directory.CreateDirectory( programFolder + @"\target" );
                    return 0;
                }
                catch( Exception e )
                {
                    Console.WriteLine("Error: {0}", e);
                    return -1;
                }
            }
            
        }

        string Filter( string input )
        {
            // Copypasted fra Functions.cs linje 210
            string result;
            StringBuilder SB = new StringBuilder();
            
            foreach( char c in input.ToLower() )
            {
                if( c >= '0' && c <= '9' )
                {
                    SB.Append( ' ' );
                }

                if( c >= 'a' && c <= 'z' )
                {
                    SB.Append(c);
                }

                if ( c == ' ' )
                {
                    SB.Append(c);
                }

                // danish characters
                if ( c == 'ø' ||
                     c == 'æ' ||
                     c == 'å' )
                {
                    SB.Append(c);
                }

                if (c == '\n' || c == '\r')
                {
                    SB.Append(' ');
                }

            }

            result = SB.ToString();

            return result;
        }

        string[] Tokenize( string input )
        {
            List<string> Sorted = new List<string>();

            string[] t = input.Split(' ');

            Sorted.Add( t[0] );

            // Finder gentagelser
            for( int x = 0; 
                     x <= t.Length - 1; 
                     x ++ )
            {

                Sorted.Add(t[x]);
                
            }

            string[] t_result = Sorted.ToArray();

            return t_result;
        }

        private DataSet DataBank;


        // Beregninger
        // Laplace smoothing https://class.coursera.org/nlp/lecture/26
        // Input er : Count (W_i, c) + 1 og Count(w, c) + |v|. |v| = vocabalary, størelsen på dets "ord forråd"
        private float smoothing( int ValA, int ValB, 
                                 int v )
        {
            if( v == 0 )
            {
                v = 1;
            }

            return ( (float)ValA + 1 ) / ((float)ValB + ( v ) );
        }

        private float prior( int documents, int totalDocuments)
        {
            return (float)documents / (float)totalDocuments;
        }

        // Får fat på Dokumenternes IDer
        private List<int> GetDocumentIds(int Category)
        {
            List<int> DocumentIDs = new List<int>();

            try
            {
                for ( int x = 0;
                          x <= DataBank.Tables["Document"].Rows.Count - 1;
                          x ++)
                {

                    if ( (int) DataBank.Tables["Document"].Rows[x]["idCategory"] == Category )
                    {
                        int found = (int)DataBank.Tables["Document"].Rows[x]["IdDocument"];
                        DocumentIDs.Add( found );

                    }

                }
            }
            catch( Exception e )
            {
                Console.WriteLine("[Error]:", e);
                return null;
            }

            return DocumentIDs;
        }

        // Henter en ordlist og samler det sammen til en stor array. 
        private List<string> GetMegaDocument( List<int> DocumentIDs)
        {
            List<string> MegaDocument = new List<string>();

            DataTable Words = DataBank.Tables["Words"];

            try
            {
                for ( int x = 0;
                          x <= DocumentIDs.Count - 1;
                          x ++ )
                {

                    string FilterExpression = "idDocument={0}";
                    string DocumentFilter = string.Format( FilterExpression,
                                                           DocumentIDs[x] );

                    DataRow[] Rows = Words.Select( DocumentFilter );
                    
                    foreach ( DataRow r in Rows )
                    {
                       
                      MegaDocument.Add( (string) r["Val"]);
                        
                    }


                }

                return MegaDocument;

            }
            catch( Exception e )
            {
                Console.WriteLine(e);
                return null;
            }
            
        }

        private int TotalDocuments()
        {
            try
            {
                int i = 0;

                DataTable Docs = DataBank.Tables["Document"];
                DataRow[] Rows = Docs.Select();

                for ( int x = 0;
                          x <= DataBank.Tables["Document"].Rows.Count - 1;
                          x ++ )
                {
                    i++;
                }

                return i;
            }
            catch( Exception e )
            {
                Console.WriteLine(e);

            }
            
            return 0;
        }

        private int getCategoryCount(int id)
        {
            DataTable Words = DataBank.Tables["Document"];
            string FilterExpression = "idCategory={0}";

            DataRow[] Row = Words.Select( string.Format( FilterExpression, id ) );

            string DocumentFilter = string.Format( FilterExpression,
                                                   id);
            int result = 0;

            foreach( DataRow R in Row )
            {
                result = result + 1;
            }

            return result;
        }

        private float k( List<string> Tokens, 
                         int Category )
        {

            // Output er properbility i forhold til om hvor vidt, den høre til inde i kategorien
            // STart værdier
            List<int>   DocumentIDs       = GetDocumentIds( Category );

            List<int>   Frequency         = new List<int>();
            List<float> CalculationResult = new List<float>();


            //Totale antal dokumenter træningssættet (under kategorien) plus det vi læser
            float cj = prior( getCategoryCount(Category), 
                              TotalDocuments() );

            Console.WriteLine("[Test Specifics -----------------------------]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( " [IDS, Prior] : Count:{0}, {1}", 
                                DocumentIDs.Count, 
                                cj );

            Console.ForegroundColor = ConsoleColor.White;

            // Får fat på ordene i katerogien
            List < string > MegaDocument = GetMegaDocument( DocumentIDs );
            List < string > Temp = new List<string>();
            List<string> Vocabalary = new List<string>();
            
            DataTable Words = DataBank.Tables["Words"];
            DataRow[] r = Words.Select();

            foreach(DataRow rw in r)
            {
                Vocabalary.Add((string)rw["Val"]);
            }

            // Får fat på Vocabalary Size
            for ( int i = 0; 
                     i <= Vocabalary.Count - 1; 
                     i ++ )
            {
                if( i == 0 )
                {
                    Temp.Add(Vocabalary[0]);
                }

                bool exist = false;

                for( int j = 0; 
                         j <= Temp.Count -1; 
                         j ++ )
                {
                    if( Vocabalary[i] == Temp[j] )
                    {
                        exist = true;
                    }

                }

                if(exist == false)
                {
                    Temp.Add( Vocabalary[i] );
                }

            }
            
            int v = Temp.Count;
            
            // Frekvensen
            for ( int i = 0; 
                     i <= Tokens.Count - 1; 
                     i ++ )
            {
                int freq = 0;

                // Køre igennem tokens
                for( int j = 0; 
                         j <= MegaDocument.Count - 1; 
                         j ++ )
                {
                    if( Tokens[i] == MegaDocument[j] )
                    {
                        freq = freq + 1;
                    }
                }
                
                Frequency.Add( freq );
            }
            
            // First
            for( int i = 0; 
                     i <= Frequency.Count -1; 
                     i ++ )
            {
                float result = smoothing( Frequency[i], 
                                          MegaDocument.Count, v );
                
                CalculationResult.Add( result );
            }

            
            float FinalResult =  0.0f;

            for( int i = 0; 
                     i <= CalculationResult.Count - 1; 
                     i ++ )
            {
                
                FinalResult = FinalResult + (float)Math.Log(CalculationResult[i]);
            }

            FinalResult = FinalResult + (float)Math.Log(cj);

            Console.WriteLine( "[Exits Quitly & Returns Value ---------------]" );
            return FinalResult;
        }

        // Public
            // Åbner en thread
        public override void run()
        {
            slaveThread = new System.Threading.Thread( NaiveBayes );
            slaveThread.Start();
            
            Console.WriteLine( "[Thread]: {0} started", 
                               slaveThread.ManagedThreadId );
            
        }
        
        // resultaterne gemmes
        public struct blackbox
        {
            public int Category;
            public string CategoryName;
            public float Result;
        }

        // Function
        private void NaiveBayes()
        {
            connectionString = connectionString + @"Source=" + programFolder + @"\" + databaseName;

            // Init
            Done = 0;
            safe = false;

            switch ( Initialise() )
            {
                case 1:
                        Console.WriteLine("[Initialisation]: OK");
                    break;

                case -1:
                        Console.WriteLine("[Error]: Wasn't able to create directory");
                    goto exit;


                case 0:
                        Console.WriteLine("[Initialisation]: didnt exist, has been create it.\r\n Fill with files to caterogise");
                    goto exit;

            }

            DataBank = new DataSet();

            Console.WriteLine("[Naive Bayes]: Initialise");

            // Retrieving Test Samples
            string Query = "select * from [{0}]";

            OleDbConnection Connection = new OleDbConnection( connectionString );
            Connection.Open();

            // Filling Data
            OleDbDataAdapter ODDA = new OleDbDataAdapter( String.Format( Query, "Document" ),
                                                          Connection );
            ODDA.Fill( DataBank, 
                       "Document" );

            ODDA.SelectCommand.CommandText = string.Format( Query, 
                                                            "Category" );
            ODDA.Fill( DataBank, 
                      "Category" );

            ODDA.SelectCommand.CommandText = string.Format( Query, 
                                                            "Words" );
            ODDA.Fill( DataBank, 
                       "Words" );

            // lukker forbindelsen
            Connection.Close();

            Console.WriteLine( " [Naive Bayes]: Filled." );

            // Man kan arbejde videre
            safe = true;

            // Processing, beregner hvad filers relevance i forhold til test samplesne for hver katerogi.
            Console.WriteLine( " [Naive Bayes]: Processing." );

            string[] FileDirectories;

            // Navne på alle filerne i mappen
            FileDirectories = Directory.GetFiles( programFolder + @"\target" );
            

            foreach ( string s in FileDirectories )
            {
                // Fortæller brugeren hvad den arbejder med
                Console.WriteLine( " [Processing]: {0}", 
                                   s );

                // Henter Target Dokumenterne
                string input = File.ReadAllText( s );

                // Endnu engang fjerner udnødvendige karakter
                string filtered = Filter( input );

                // Tokenizer teksten
                string[] Tokens = Tokenize( filtered );

                // Sætter ordene i en liste
                List<string> TokensList = new List<string>();

                List<blackbox> resultater = new List<blackbox>();

                for ( int x = 0; 
                          x <= Tokens.Length -1; 
                          x ++ )
                {
                    TokensList.Add( Tokens[x] );
                }

                // Fjerne empty space og null
                TokensList.RemoveAll( String.IsNullOrWhiteSpace );
                
                DataTable Docs = DataBank.Tables["Category"];
                DataRow[] Rows = Docs.Select();

                foreach( DataRow rs in Rows )
                {
                    float t_result;

                    t_result = k( TokensList, (int)rs["IdCategory"] );

                    blackbox bb;
                    bb.Category = (int)rs["IdCategory"];
                    bb.Result = t_result;
                    bb.CategoryName = (string)rs["Label"];

                    Console.WriteLine("Category: {0} - Name: {2} - Score : {1}",
                                       bb.Category,
                                       bb.Result,
                                       bb.CategoryName);
                }
                
            }

            exit:
            // Output Result
            Console.WriteLine(" [Naive Bayes]: Exits Threads ");
            
            // Output Data
            Done = 1;
        }
      
    }

}

