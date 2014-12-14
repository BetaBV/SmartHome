using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Enumerators;

namespace SmartHome
{
    class Serial
    {
        //Variables
        //Public
        public event SerialReceivedHandler SerialReceived;
        public bool IsOpen {
            get {
                return serialPort.IsOpen;
            }
        }

        //Private
        internal delegate void SerialReceivedHandler(object sender, SerialDataEventArgs data);

        private SerialPort serialPort;

        private volatile bool stopping = false;

        //Constructor

        public Serial()
        {
            
        }

        //Functions
        //Private
        protected virtual void OnSerialReceived(SerialDataEventArgs data)
        {
            SerialReceivedHandler handler = SerialReceived;
            if (handler != null) handler(this, data);
        }

        private void dataReceiverThread()
        {
            //Create temporary list
            var dataBuffer = new List<byte>();
            DateTime timeout;
            while (!stopping)
            {
                if (serialPort.BytesToRead > 0)
                {
                    timeout = DateTime.Now.AddSeconds(2);//set timeout on 2s
                    do
                    {
                        //while no bytes available sleep 1 ms
                        while (serialPort.BytesToRead == 0)
                        {
                            if (DateTime.Now > timeout) break;//if timeout has been reached exit loop
                            Thread.Sleep(1);
                        }

                        if(serialPort.BytesToRead==0)break;//if timeout has been reached exit loop

                        //While thermenator byte hasn't been received
                        dataBuffer.Add((byte)serialPort.ReadByte());

                    } while (dataBuffer.Last() != (byte)dataBytes.THERMINATOR);

                    if (dataBuffer.Last() != (byte)dataBytes.THERMINATOR)
                    {
                        //clear list and send error message
                        dataBuffer.Clear();
                        SerialWrite(new[] {(byte)dataBytes.RECEIVEDNOTHERMINATOR, (byte)dataBytes.THERMINATOR});
                    }
                    else
                    {
                        //When thermanator byte has been received raise event and clear list
                        OnSerialReceived(new SerialDataEventArgs(dataBuffer.ToArray()));
                        dataBuffer.Clear();
                    }

                }
                //wait 10 ms before retrying
                Thread.Sleep(10);

            }
        }

        //Public
        public bool SerialConnect(string comPort, int boundRate)
        {
            //create new serial port
            serialPort=new SerialPort();
            //open port on [portname] with boundrate [boundRate]
            serialPort.BaudRate = boundRate;
            serialPort.PortName = comPort;
            try
            {
                //try to open the serial port
                serialPort.Open();
            }
            catch (Exception ex)
            {
                //on exception write to console and return false
                Console.WriteLine(ex.Message);
                return false;
            }


            //Set stopping to false and make a receiving thread
            stopping = false;

            var tr = new Thread(dataReceiverThread);
            tr.Start();

            //return serialport isOpen
            return serialPort.IsOpen;
        }

        public void SerialDisconnect()
        {
            //stop receiving thread
            stopping = true;
            //wait while there is data to be received
            while (serialPort.BytesToRead>0)
            {
                Thread.Sleep(1);
            }
            //close serial port
            serialPort.Close();
        }

        public bool SerialWrite(byte[] data)
        {
            try
            {
                //Write data from position 0 to count
                serialPort.Write(data, 0, data.Count());
            }
            catch (Exception ex)
            {
                //on exception write to console and return false
                Console.WriteLine(ex.Message);
                return false;
            }
            //no exception return true
            return true;

        }
    }

    class SerialDataEventArgs : EventArgs
    {
        public byte[] Data { get; private set; }

        public SerialDataEventArgs(byte[] data)
        {
            //Clone so data will not be overwritten
            Data = (byte[])data.MemberwiseClone();
        }
    }
    
}
