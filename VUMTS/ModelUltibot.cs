using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace VUMTS
{
    public class ModelUltibot : IModelUltibot
    {
        private bool _continue;
        private SerialPort _serialPort;
        private bool first = true;
        private IView view;
        private IController controller;

        IView IModel.View 
        { set => view = value; }
       
        IController IModel.Controller { set => controller = value; }

        void IModel.getDistance(string portName)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
        }

        public ModelUltibot()
        {
            _serialPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string message = _serialPort.ReadExisting();

                if (message.Contains("J"))
                    message = message.Split('J')[1];
                if (message.Contains("\n"))
                    message = message.Split('\r')[0];
                if (message != "")
                {
                    view.anzeigen(message);
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        void IModel.stop()
        {
            if(_serialPort.IsOpen)
                _serialPort.Close();
        }
    }
}
