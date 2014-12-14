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
        public event DataReceivedHandler DataReceived;
        public SensorType Type { get; private set; }
        public String SensorLocation { get; private set; }//May be class
        public String SensorFysicalLocation { get; private set; }
        public int SensorId { get; private set; }
        //Private
        internal delegate void DataReceivedHandler(object sender, ReceivedData data);
        private int SensorValue;

        //Constructor
        public Sensor()
        {
            
        }

        //Functions

        public int ReadSensorValue()
        {
            return SensorValue;
        }

        protected virtual void OnDataReceived(ReceivedData data)
        {
            DataReceivedHandler handler = DataReceived;
            if (handler != null) handler(this, data);
        }

    }
    class ReceivedData : EventArgs
    {
        public int Data { get; private set; }

        public ReceivedData(int data)
        {
            Data = data;
        }
    }
}
