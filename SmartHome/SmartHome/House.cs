using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Enumerators;

namespace SmartHome
{
    class House
    {
        //Constants
        private const byte Version = 0x01;

        //Variables
        //Public

        //Private
        private Serial serial;
        private List<Person> persons;
        private List<String> locations;

        private List<Sensor> sensors = new List<Sensor>();
        private List<Actuator> actuators = new List<Actuator>();
        
        //Constructor
        public House(Serial serial, List<Person> persons, List<String> locations)
        {
            //Set variables
            this.serial = serial;
            this.persons = persons;
            this.locations = locations;

            //todo Get Info from MySQL

            //Initialise Serial
            serial.SerialReceived += serial_SerialReceived;
            if (!serial.IsOpen) serial.SerialConnect("COM17", 9600);//todo ask for com port
            serial.SerialWrite(new [] {(byte)dataBytes.INIT,Version,(byte)dataBytes.THERMINATOR});
        }

        //Functions
        //Public
        
        public int ReadSensor(string location)
        {

            return 0;//todo
        }

        public bool WriteActuator(Byte[] location, Byte value)
        {
            //Write to first actuator with correct location with value
            //if it doesn't exist write an empty byte array
            try
            {
                return serial.SerialWrite(actuators.First(actuator => actuator.ActuatorLocation == location).WriteActuator(value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            
        }

        public int ReadActuator(byte[] location)
        {
            return 0;//todo
        }

        public List<Sensor> GetSensorsAtLocation(String location)
        {
            return sensors.Where(sensor=>sensor.SensorPhysicalLocation==location).ToList();
        }

        public List<Actuator> GetActuatorsAtLocation(string location)
        {
            return actuators.Where(actuator => actuator.ActuatorPhysicalLocation == location).ToList(); //todo
        }

        public List<String> GetLocations()
        {
            return locations;
        }
        //Private

        private void serial_SerialReceived(object sender, SerialDataEventArgs data)
        {
            switch (data.Data[0])
            {
                case 0XFA://INIT

                    break;
                    //todo rest of commands
            }
        }

    }
}
