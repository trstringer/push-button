using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using System.Threading;
using System.Diagnostics;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace ButtonPusher
{
    public sealed class StartupTask : IBackgroundTask
    {
        private const int _buttonPin = 4;
        private GpioPin _button;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            SetupButton();

            Debug.WriteLine("Ending application...");
        }

        private void SetupButton()
        {
            var controller = GpioController.GetDefault();

            _button = controller.OpenPin(_buttonPin);
            _button.SetDriveMode(GpioPinDriveMode.InputPullUp);

            GpioPinValue oldPinValue = _button.Read();
            GpioPinValue newPinValue;
            int counter = 0;
            while (true)
            {
                newPinValue = _button.Read();
                if (newPinValue != oldPinValue)
                {
                    counter++;
                    Debug.WriteLine("New pin value set {0} {1}", newPinValue, counter);
                    oldPinValue = newPinValue;
                }                
            }
        }
    }
}
