using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SendInputs
{
    class SendAction
    {

        private const int BM_CLICK = 0x00F5;

        private const uint WM_LBUTTONDOWN = 0x201;
        private const uint WM_LBUTTONUP = 0x202;
        private const uint MK_LBUTTON = 0x0001;

        private const int WM_SETTEXT = 0x000C;

        //para enviar acoes do mouse direto sem esta em segundo plano
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        //para enviar mensagens de texto direto sem esta em segundo plano
        [DllImport("User32.dll")]
        static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        //essa função permitira alterar o nome do processo de outras janelas
        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);

        //Essa função permiti deixar em segundo plano para enviar os comandos para uma janela especifica sem estar ativa
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern int SetForegroundWindow(IntPtr point);

        public static void SendClick(IntPtr hwnd)
        {
            SendMessage(hwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        public static void SendClick(IntPtr hwnd, int x, int y)
        {
            SendMessage(hwnd, WM_LBUTTONDOWN, new IntPtr(MK_LBUTTON), CreateLParam(x, y));
            SendMessage(hwnd, WM_LBUTTONUP, new IntPtr(MK_LBUTTON), CreateLParam(x, y));
        }

        public static void SendText(IntPtr hwnd, string msg)
        {
            SendMessage(hwnd, WM_SETTEXT, 0, msg);
        }

        public static void ChangeText(IntPtr hwnd, string text) {
            SetWindowText(hwnd, text);
        }

        private static IntPtr CreateLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
    }
}
