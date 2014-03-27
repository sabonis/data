using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    public class KeyPressedEventArgs : EventArgs
    {
        public Keys KeyCode { get; set; }
        public KeyPressedEventArgs(Keys Key)
        {
            KeyCode = Key;
        }
    }
}
