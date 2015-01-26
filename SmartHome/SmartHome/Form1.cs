using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartHome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32")]
        static extern bool AllocConsole();

        private Serial serial = new Serial();
        private House house;
        private PipelineHandler pipelineHandler;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (serial.SerialConnect(tbComPort.Text, 9600))
            {
                MessageBox.Show("Connected!");
                btConnect.Enabled = false;
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        private void btStartHouse_Click(object sender, EventArgs e)
        {
            house = new House(serial);
            btStartHouse.Enabled = false;
        }

        private void btStartPipe_Click(object sender, EventArgs e)
        {
            pipelineHandler=new PipelineHandler(house);
            btStartPipe.Enabled = false;
        }

        private void btShowConsole_Click(object sender, EventArgs e)
        {
            AllocConsole();
            btShowConsole.Enabled = false;
        }
    }
}
