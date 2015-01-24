using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    class Fysicallocation
    {
        //Variables
        //Public
        public String Location { get; private set; }
        public String Description { get; private set; }
        public String Properties { get; private set; }
        //Private
        
        //Constructor
        public Fysicallocation(string location, string description, string properties)
        {
            Properties = properties;
            Description = description;
            Location = location;
        }

        //Functions
        //Public

        //Private

    }
}
