using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome
{
    class PipelineHandler
    {
        //Variables
        //Public

        //Private
        private House house;
        
        private NamedPipeServerStream pipeServer = new NamedPipeServerStream("SmartHomePipeline",PipeDirection.InOut,5);

        private volatile bool ServerRunning = true;

        //Constructor
        public PipelineHandler(House house)
        {
            this.house = house;
            
            var tr = new Thread(ListeningThread);
            tr.Start();
        }
        //Functions
        //public
        public void StopPipeline()
        {
            ServerRunning = false;
            pipeServer.Close();
        }
        //private
        private void ListeningThread()
        {
            while (ServerRunning)
            {
                try
                {
                    pipeServer.WaitForConnection();
                    if (pipeServer.ReadByte() == 0xAD)//Check if proper client
                    {
                        switch (pipeServer.ReadByte())
                        {
                            case 0x01://ReadActuator (location[3])
                                var locationA = new Byte[3];
                                pipeServer.Read(locationA, 0, 3);
                                pipeServer.WriteByte((Byte)house.ReadActuator(locationA));
                                break;
                            case 0x02://ReadSensor (location[2]
                                var locationS = new Byte[2];
                                pipeServer.Read(locationS, 0, 2);
                                pipeServer.WriteByte((Byte)house.ReadSensor(locationS));
                                break;
                            case 0x03://Write Actuator (location[3],value[1])
                                var locationAW = new byte[3];
                                pipeServer.Read(locationAW, 0, 3);
                                var value = (byte)pipeServer.ReadByte();
                                pipeServer.WriteByte(house.WriteActuator(locationAW, value) ? (Byte)0x10 : (Byte)0x20);
                                break;
                        }
                    }
                    pipeServer.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
            }
        }

        private void pipeHandleConnection()
        {
            
        }
    }
}
