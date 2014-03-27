using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keyboard;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.TreatControlCAsInput = true;
            int check_i = 0;
            int win_number = 0;
            string kkk = "";
            while (true)
            {
                //該Soft Ctrl+C 關閉 cmd禁止 
                ConsoleKeyInfo key2 = Console.ReadKey(true);

                win_number = (key2.Key == ConsoleKey.NumPad8) ? check_i += 1 : check_i -= check_i;
                //start write txt
                //Console.WriteLine(key.Key);

                kkk += key2.Key.ToString();

                if (win_number == 8)
                {
                    break;
                }
            }

            /*if (kkk.Length > 0) Console.WriteLine(kkk);
            {
                
            }*/

            var p = new Process { StartInfo = { FileName = "notepad.exe" } };

            p.Start();

            try
            {
                List<Key> keys = kkk.Select(c => new Key(c)).ToList();
                var procId = p.Id;
                p.WaitForInputIdle();
                foreach (var key in keys)
                {
                    key.PressForeground(p.MainWindowHandle);
                }

            }
            catch (InvalidOperationException)
            {
            }

        }

    }
}
