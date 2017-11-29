using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace pr1
{
    public partial class Form2 : Form
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;

        public static string text;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }


        private delegate IntPtr LowLevelKeyboardProc(

            int nCode, IntPtr wParam, IntPtr lParam);

        static Queue qq = new Queue(10);
        static Array myTargetArray = Array.CreateInstance(typeof(String), 10);
        private static IntPtr HookCallback(

            int nCode, IntPtr wParam, IntPtr lParam)

        {

            if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYDOWN)

            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.V)
                {
                    if (qq.Count == 10)
                    {
                        qq.Dequeue();
                        qq.TrimToSize();
                    }
                    qq.Enqueue(Clipboard.GetText());
                    qq.CopyTo(myTargetArray, 0);
                    text = qq.Count.ToString();
                    Clipboard.Clear();
                }

                if ((Keys)vkCode == Keys.D1)
                {
                    text += myTargetArray.GetValue(0);
                }

                if ((Keys)vkCode == Keys.D2)
                {
                    text += myTargetArray.GetValue(1);
                }

                if ((Keys)vkCode == Keys.D3)
                {
                    text += myTargetArray.GetValue(2);
                }

                if ((Keys)vkCode == Keys.D4)
                {
                    text += myTargetArray.GetValue(3);
                }

                if ((Keys)vkCode == Keys.D5)
                {
                    text += myTargetArray.GetValue(4);
                }

                if ((Keys)vkCode == Keys.D6)
                {
                    text += myTargetArray.GetValue(5);
                }

                if ((Keys)vkCode == Keys.D7)
                {
                    text += myTargetArray.GetValue(6);
                }

                if ((Keys)vkCode == Keys.D8)
                {
                    text += myTargetArray.GetValue(7);
                }

                if ((Keys)vkCode == Keys.D9)
                {
                    text += myTargetArray.GetValue(8);
                }

                if ((Keys)vkCode == Keys.D0)
                {
                    text += myTargetArray.GetValue(9);
                }

                //text += ' '  + Convert.ToString((Keys)vkCode);

            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public Form2()
        {
            InitializeComponent();
            qq.Clear();
            Clipboard.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _hookID = SetHook(_proc);
        }
       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label2.Text = text;
        }
    }
}
