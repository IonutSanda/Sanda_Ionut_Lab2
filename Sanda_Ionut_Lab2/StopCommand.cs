using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Sanda_Ionut_Lab2.CustomComands
{
    class StopCommand
    {
        private static RoutedUICommand launch_command;

        static StopCommand()
        {
            InputGestureCollection myInputGesture = new InputGestureCollection();
            myInputGesture.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            launch_command = new RoutedUICommand("Launch", "Launch", typeof(StopCommand), myInputGesture);
        }
        public static RoutedUICommand Launch
        {
            get
            {
                return launch_command;
            }
        }
    }
}
