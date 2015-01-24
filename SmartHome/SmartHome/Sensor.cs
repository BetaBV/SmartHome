using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Enumerators;

namespace SmartHome
{
    class Sensor
    {
        //Variables
        //Public
        public SensorType Type { get; private set; }
        public Byte[] SensorLocation { get; private set; }//May be class
        public String SensorPhysicalLocation { get; private set; }
        public int SensorId { get; private set; }
        //Private
        
        private int value;

        //Constructor
        public Sensor(SensorType type, Byte[] sensorLocation, String sensorPhysicalLocation, int sensorId)
        {
            SensorId = sensorId;
            SensorPhysicalLocation = sensorPhysicalLocation;
            SensorLocation = sensorLocation;
            Type = type;
        }

        //Functions

        public int ReadSensorValue()
        {
            return value;
        }

        public void SetSensorValue(int value)
        {
            this.value = value;
        }
    }
}
