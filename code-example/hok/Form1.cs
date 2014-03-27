using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        public int m_hookHandle = 0;
        public Win32API.DelHookProc m_pfnHookProc = null;
        
        
        public Form1()
        {
            InitializeComponent();
        }

        public int HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam.ToInt32() == Win32API.WM_KEYUP || wParam.ToInt32() == Win32API.WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                listView1.Items.Add(string.Format("{0} {1} {2} {3}", nCode, wParam, lParam, keyData.ToString() ));
            }


            return Win32API.CallNextHookEx(m_hookHandle, nCode, wParam, lParam);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    m_pfnHookProc = new Win32API.DelHookProc(HookProc);
                    m_hookHandle = Win32API.SetWindowsHookEx(
                        Win32API.WH_KEYBOARD_LL,
                        m_pfnHookProc,
                        Win32API.GetModuleHandle(curModule.ModuleName),
                        0
                    );
                }

                if (m_hookHandle == 0)
                {
                    MessageBox.Show("呼叫 SetWindowsHookEx 失敗!");
                    return;
                }
            }
        }

       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool ret = Win32API.UnhookWindowsHookEx(m_hookHandle);
            if (ret == false)
            {
                MessageBox.Show("呼叫 UnhookWindowsHookEx 失敗!");
                return;
            }
            m_hookHandle = 0;
        }

        void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            listView1.Items.Add(e.KeyCode.ToString());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
