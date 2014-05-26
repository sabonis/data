using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationexe
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> start_for = new List<string>();

            start_for.Add(@"C:\\xampp\\mysql\\bin\\mysqld.exe");//mysql路徑
            start_for.Add(@"C:\\xampp\\apache\\bin\\httpd.exe");//apache路徑

            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            foreach (string C_val in start_for)
            {
                Process SomeProgram = new Process();
                SomeProgram.StartInfo.FileName = C_val;
                SomeProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//不要有視窗
                SomeProgram.Start();
            }
           
        }
    }
}
