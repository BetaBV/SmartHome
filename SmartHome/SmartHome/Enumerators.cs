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
            LED = 0x01,
            PWM = 0x02,
            SERVO = 0x03
        }

        public enum dataBytes : byte
        {
            /// <summary>
            /// Reads a sensors value ARDUINO,PIN
            /// </summary>
            SENSORREAD=0x10,
            /// <summary>
            /// Reads the current(last known) position of Actuator ARDUINO,CHILD,PIN CHILD=0xfe if no child
            /// </summary>
            ACTUATORREAD=0x20,
            /// <summary>
            /// Writes position to actuator ARDUINO,CHILD,PIN,TYPE,VALUE CHILD=0xfe if no child
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

        public enum receivingBytes : byte
        {
            /// <summary>
            /// Retrieving sensor value ARDUINO, PIN, VALUE
            /// </summary>
            SENSORVALUE=0x10,
            /// <summary>
            /// Retreiving acuator value ARDUINO, CHILD, PIN, VALUE
            /// </summary>
            ACTUATORVALUE=0x20,
            /// <summary>
            /// Retrieving actuator value if succesfull movement ARDUINO, CHILD, PIN, VALUE
            /// </summary>
            ACTUATORSUCCES=0x21,
            /// <summary>
            /// Initialised correctly
            /// </summary>
            INITSUCCES=0xFA,
            /// <summary>
            /// No therminator received(read error)
            /// </summary>
            RECEIVEDNOTHERMINATOR=0xFE,
            /// <summary>
            /// Therminator
            /// </summary>
            THERMINATOR=0xFF
        }
    }
}
