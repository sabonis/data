using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleApplication2;

namespace ConsoleApplication1
{
    class Program
    {
        public int m_hookHandle = 0;
        public Win32API.DelHookProc m_pfnHookProc = null;

        static void Main(string[] args)
        {
            string msg = "";

            Hook_Start task1 = new Hook_Start();
            msg = task1.Execute();


            Console.Write(msg);
        }



    }
}
