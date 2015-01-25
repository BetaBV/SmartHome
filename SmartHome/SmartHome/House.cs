using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SmartHome.Enumerators;

namespace SmartHome
{
    class House
    {
        //Constants
        private const byte Version = 0x01;

        private const String ConnectionString = "Server=localhost;Database=betahomedb;Uid=root;Pwd=usbw;";

        //Variables
        //Public

        //Private
        private Serial serial;
        private List<Person> persons = new List<Person>();
        private List<Fysicallocation> locations = new List<Fysicallocation>();

        private List<Sensor> sensors = new List<Sensor>();
        private List<Actuator> actuators = new List<Actuator>();
        
        //Constructor
        public House(Serial serial)
        {
            //Set variables
            this.serial = serial;

            //Read sensors and actuators from mysql database

            var con = new MySqlConnection(ConnectionString);
            con.Open();

            var actuatorsCommand = new MySqlCommand("SELECT *" +
                                           "FROM actuator", con);
            var actuatorsReader = actuatorsCommand.ExecuteReader();

            while (actuatorsReader.Read())
            {
                ActuatorLowType actuatorType;
                Enum.TryParse(actuatorsReader.GetString("actuatorLowType"),out actuatorType);
                actuators.Add(new Actuator(actuatorsReader.GetString("actuatorType"), actuatorType,
                    actuatorsReader.GetInt32("actuatorId"), StringToByte(actuatorsReader.GetString("actuatorLocation")),
                    actuatorsReader.GetString("fysicalLocation")));
            }

            actuatorsReader.Close();

            var sensorCommand = new MySqlCommand("SELECT *" +
                                           "FROM sensor", con);
            var sensorReader = sensorCommand.ExecuteReader();

            while (sensorReader.Read())
            {
                SensorType sensorType;
                Enum.TryParse(sensorReader.GetString("sensorType"), out sensorType);
                sensors.Add(new Sensor(sensorType, StringToByte(sensorReader.GetString("sensorLocation")),
                    sensorReader.GetString("fysicalLocation"), sensorReader.GetInt32("sensorId")));
            }

            sensorReader.Close();
            //load persons
            var personCommand = new MySqlCommand("SELECT name,gender,birthdate,personId" +
                                          "FROM sensor", con);
            var personReader = personCommand.ExecuteReader();

            while (personReader.Read())
            {
                persons.Add(new Person(personReader.GetString("name"), personReader.GetChar("gender"),personReader.GetDateTime("birthdate"),personReader.GetInt32("personId")));
            }

            personReader.Close();

            var locationCommand = new MySqlCommand("SELECT *" +
                                                   "FROM fysicallocation",con);
            var locationReader = locationCommand.ExecuteReader();

            while (locationReader.Read())
            {
                locations.Add(new Fysicallocation(locationReader.GetString("fysicallocation"),locationReader.GetString("discription"),locationReader.GetString("propertys")));
            }

            //Initialise Serial
            serial.SerialReceived += serial_SerialReceived;
            if (!serial.IsOpen) serial.SerialConnect("COM17", 9600);//todo ask for com port
            serial.SerialWrite(new [] {(byte)dataBytes.INIT,Version,(byte)dataBytes.THERMINATOR});
        }

        //Functions
        //Public
        
        public int ReadSensor(byte[] location)
        {
            //write ReadSensor packet to arduino
            var readSensorBytes = new List<Byte>();
            readSensorBytes.Add((Byte)dataBytes.SENSORREAD);
            readSensorBytes.AddRange(location);
            readSensorBytes.Add((Byte)dataBytes.THERMINATOR);
            serial.SerialWrite(readSensorBytes.ToArray());

            //wait for arduino to respond(Yes bad I know :( )
            Thread.Sleep(50);
            return FindSensor(location).ReadSensorValue();
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
            //write ReadActuator packet to arduino
            var writeAcuatorBytes = new List<Byte>();
            writeAcuatorBytes.Add((Byte)dataBytes.ACTUATORREAD);
            writeAcuatorBytes.AddRange(location);
            writeAcuatorBytes.Add((Byte)dataBytes.THERMINATOR);
            serial.SerialWrite(writeAcuatorBytes.ToArray());
            
            //wait for aduino to respond(Yes bad I know :( )
            Thread.Sleep(50);
            return FindActuator(location).ReadActuator();
        }

        public List<Sensor> GetSensorsAtLocation(String location)
        {
            return sensors.Where(sensor=>sensor.SensorPhysicalLocation==location).ToList();
        }

        public List<Actuator> GetActuatorsAtLocation(String location)
        {
            return actuators.Where(actuator => actuator.ActuatorPhysicalLocation == location).ToList(); //todo
        }

        public List<String> GetLocations()
        {
            return locations.Select(location=>location.Location).ToList();
        }
        //Private

        private void serial_SerialReceived(object sender, SerialDataEventArgs data)
        {
            switch ((receivingBytes)data.Data[0])
            {
                case receivingBytes.INITSUCCES:
                    //todo
                    break;
                case receivingBytes.ACTUATORSUCCES:
                    FindActuator(new[] {data.Data[1], data.Data[2], data.Data[3]}).SetActuatorValue(data.Data[4]);
                    break;
                case receivingBytes.ACTUATORVALUE:
                    FindActuator(new[] {data.Data[1], data.Data[2], data.Data[3]}).SetActuatorValue(data.Data[4]);
                    break;
                case receivingBytes.SENSORVALUE:
                    FindSensor(new[] {data.Data[1], data.Data[2]}).SetSensorValue(data.Data[3]);
                    break;
                case receivingBytes.RECEIVEDNOTHERMINATOR:
                    //todo
                    break;
            }
        }

        private Actuator FindActuator(Byte[] location)
        {
            return actuators.FirstOrDefault(actuator => actuator.ActuatorLocation == location);
        }

        private Sensor FindSensor(Byte[] location)
        {
            return sensors.First(sensor => sensor.SensorLocation == location);
        }

        /// <summary>
        /// Makes bytearray from string in syntax 0xXX,0xXX,0xXX
        /// </summary>
        /// <param name="input">input string to make bytearray</param>
        /// <returns>bytearray from string</returns>
        private byte[] StringToByte(String input)
        {
            return input.Replace("0x", "").Split(',').Select(str => Convert.ToByte(str, 16)).ToArray();
        }

    }
}
