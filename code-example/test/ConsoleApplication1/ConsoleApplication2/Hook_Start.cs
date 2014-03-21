using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Hook_Start
    {
        public string Execute()
        {

            Console.TreatControlCAsInput = true;
            int check_i = 0;
            int win_number = 0;
            string msg = null;
            while (true)
            {
                //該Soft Ctrl+C 關閉 cmd禁止 
                ConsoleKeyInfo key = Console.ReadKey(true);

                win_number = (key.Key == ConsoleKey.NumPad8) ? check_i += 1 : check_i -= check_i;
                //start write txt
                if (win_number == 8) break;

                AddKey(key.Key, key.Modifiers);
                

            }

            
            return msg;
        }

        static void AddKey(ConsoleKey key, ConsoleModifiers mod)
        {
            if ((int)mod != 0)

                Console.Write(mod.ToString() + " + ");
                //Console.WriteLine(key);

        }


    }
}
