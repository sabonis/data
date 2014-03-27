using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyboardHook.CreateHook();
            KeyboardHook.KeyPressed += KeyboardHook_KeyPressed;
            //Application.Run();
            //ConsoleApplication3.run();
            KeyboardHook.DisposeHook();
        }

        static void KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString());
        }
    }
}
