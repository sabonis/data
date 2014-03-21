using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace hok
{
    [StructLayout(LayoutKind.Sequential)] 
    public class KeyboardHookStruct 
    { 
        public int vkCode; 
        public int scanCode; 
        public int flags; 
        public int time; 
        public int dwExtraInfo; 
    } 
}
