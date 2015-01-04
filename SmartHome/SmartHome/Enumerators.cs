using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    namespace Enumerators
    {
        public enum SensorType : byte
        {
            LDR = 0x01,
            TEMP = 0x02,
            BUTTON = 0x03
        }

        public enum ActuatorLowType : byte
        {
            SERVO = 0x01,
            LED = 0x02
        }

        public enum dataBytes : byte
        {
            /// <summary>
            /// Reads a sensors value ARDUINO,PIN
            /// </summary>
            SENSORREAD=0x10,
            /// <summary>
            /// Reads the current(last known) position of Actuator ARDUINO,CHILD,PIN CHILD=0xff if no child
            /// </summary>
            ACTUATORREAD=0x20,
            /// <summary>
            /// Writes position to actuator ARDUINO,CHILD,PIN,VALUE CHILD=0xff if no child
            /// </summary>
            ACTUATORWRITE=0x21,
            /// <summary>
            /// Intialises conection, SERVERVERSION
            /// </summary>
            INIT=0xFA,
            /// <summary>
            /// When data has been received without a therminator
            /// </summary>
            RECEIVEDNOTHERMINATOR=0xFE,//N/A
            /// <summary>
            /// Therminator byte
            /// </summary>
            THERMINATOR=0xFF//Therminates command
        }
    }
}
