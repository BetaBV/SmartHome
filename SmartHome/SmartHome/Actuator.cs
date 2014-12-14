using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Enumerators;

namespace SmartHome
{
    class Actuator
    {
        //Variables
        //Public
        public String ActuatorType { get; private set; }
        public ActuatorLowType ActuatorLowType { get; private set; }
        public int ActuatorId { get; private set; }
        public String AcuatorLocation { get; private set; }//May be Class
        public String ActuatorPhysicalLocation { get; private set; }
        //Private

        //Constructor

        //Functions
        //Public
        public int[] WriteActuator(int value)
        {
            switch (ActuatorLowType)
            {
                case ActuatorLowType.LED:
                    return new int[] { };
                    break;
                case ActuatorLowType.SERVO:
                    return new int[] { };
                    break;

                default:
                    return new int[] { };
                    break;
            }
        }


        public int ReadActuator(byte[] data)
        {
            return 0;
        }
        //Private

    }
}
