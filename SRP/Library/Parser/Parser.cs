using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Parser
{
    public class Parser
    {
        // Internal Variables
        private List< Rule > iOfRules = new List< Rule >();

        private String iPath = "";

        private String iStream = "";
        private int iPointer   = 0;


        // Constructors
        public Parser( String Path )
        {
            iPath = Path;
        }

        // Accessors
            // Add a Rule
        public void AddRule( Rule rule )
        {
            iOfRules.Add( rule );
        }

            // Array of Rules
        public void AddRules( Rule[] ruleArray )
        {
            iOfRules.AddRange( ruleArray );
        }
        
            // List of Rules
        public void AddRules( List<Rule> listOfRules )
        {
            iOfRules.AddRange( listOfRules );
        }
        
        public bool RemoveRule( int Index )
        {
            return false;
        }
        
    }

}
