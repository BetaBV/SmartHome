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
        public byte[] ActuatorLocation { get; private set; }//May be Class
        public String ActuatorPhysicalLocation { get; private set; }
        //Private

        private int value = 0;

        //Constructor
        public Actuator(string actuatorType, ActuatorLowType actuatorLowType, int actuatorId, byte[] actuatorLocation, string actuatorPhysicalLocation)
        {
            ActuatorPhysicalLocation = actuatorPhysicalLocation;
            ActuatorLocation = actuatorLocation;
            ActuatorId = actuatorId;
            ActuatorLowType = actuatorLowType;
            ActuatorType = actuatorType;
        }

        //Functions
        //Public
        public Byte[] WriteActuator(Byte value)
        {
            var retList = new List<Byte>();
            retList.Add((Byte)dataBytes.ACTUATORWRITE);
            retList.AddRange(ActuatorLocation);
            retList.Add((Byte)ActuatorLowType);
            retList.Add((Byte)dataBytes.THERMINATOR);
            return retList.ToArray();
        }

        public int ReadActuator()
        {
            return value;
        }

        public void SetActuatorValue(int newValue)
        {
            value = newValue;
        }
        //Private

    }
}
