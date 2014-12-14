using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    class Person
    {

        //Variables
        //Public
        public String Name { get; private set; }
        public Char Gender { get; private set; }
        public DateTime Birtdate { get; private set; }
        public int Age { get { return DelataYearsDateTime(Birtdate,DateTime.Now); }}
        public int Id { get; private set; }
        public String Location { get; private set; }//May be class
        //Private

        //Constructor

        //Functions
        //Public

        //Private
        private int DelataYearsDateTime(DateTime a, DateTime b)
        {
            var zeroTime = new DateTime(1, 1, 1);

            return (zeroTime + (b - a)).Year - 1;
        }
    }
}
