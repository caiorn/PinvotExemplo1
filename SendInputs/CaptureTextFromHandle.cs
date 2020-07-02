using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SendInputs
{
    class CaptureTextFromHandle
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        // callback to enumerate child windows
        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr parameter);


        [Flags]
        internal enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x20
        }

        // Specific import for WM_GETTEXTLENGTH
        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", CharSet = CharSet.Auto)]
        internal static extern int SendMessageTimeout(
            IntPtr hwnd,
            uint Msg,              // Use WM_GETTEXTLENGTH
            int wParam,
            int lParam,
            SendMessageTimeoutFlags flags,
            uint uTimeout,
            out int lpdwResult);

        // Specific import for WM_GETTEXT
        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern uint SendMessageTimeoutText(
            IntPtr hWnd,
            uint Msg,              // Use WM_GETTEXT
            int countOfChars,
            StringBuilder text,
            SendMessageTimeoutFlags flags,
            uint uTImeoutj,
            out IntPtr result);

        /// <summary>
        /// Captura o texto de um componente especifico (botao, textbox, checkbox)
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        internal static string GetTextFromControl(IntPtr hwnd)
        {
            const uint WM_GETTEXTLENGTH = 0x000E;
            const uint WM_GETTEXT = 0x000D;
            int length;
            IntPtr p;

            var result = SendMessageTimeout(hwnd, WM_GETTEXTLENGTH, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 5, out length);

            if (result != 1 || length <= 0)
                return string.Empty;

            var sb = new StringBuilder(length + 1);

            return SendMessageTimeoutText(hwnd, WM_GETTEXT, sb.Capacity, sb, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 5, out p) != 0 ?
                    sb.ToString() :
                    string.Empty;
        }

        /// <summary>
        /// Captura todos os ponteiro e textos a partir da janela pai..(janela, groupbox, painel)
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        internal static Dictionary<IntPtr, string> getAllKeyAndValueChildsFromWindow(IntPtr hwnd)
        {
            Dictionary<IntPtr, string> dictionary = new Dictionary<IntPtr, string>();
            dictionary.Add(hwnd, GetTextFromControl(hwnd));
            foreach (var h in GetChildWindows(hwnd))
            {
                dictionary.Add(h, GetTextFromControl(h));
            }
                return dictionary;
        }

        private static bool EnumChildWindowsCallback(IntPtr handle, IntPtr pointer)
        {
            // this method will be called foreach child window
            // create a GCHandle from pointer
            var gcHandle = GCHandle.FromIntPtr(pointer);

            // cast pointer as list
            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
                throw new InvalidCastException("Invalid cast of GCHandle as List<IntPtr>");

            // Adds the handle to the list.
            list.Add(handle);

            return true;
        }

        private static IEnumerable<IntPtr> GetChildWindows(IntPtr parent)
        {
            // Create list to store child window handles.
            var result = new List<IntPtr>();

            // Allocate list handle to pass to EnumChildWindows.
            var listHandle = System.Runtime.InteropServices.GCHandle.Alloc(result);

            try
            {
                // enumerates though the children
                EnumChildWindows(parent, EnumChildWindowsCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                // free unmanaged list handle
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            return result;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [StructLayout(LayoutKind.Sequential)]
        struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(Boolean? filler)
                : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }

        }
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left, top, right, bottom;
        }

        private System.Drawing.Point getCenterWindow(System.Diagnostics.Process win)
        {

            //var p = System.Diagnostics.Process.GetProcessesByName("mspaint");
            //if (p.Length == 0) return;
            WINDOWINFO wi = new WINDOWINFO(false);
            GetWindowInfo(win.MainWindowHandle, ref wi);
            return new System.Drawing.Point((wi.rcWindow.left + wi.rcWindow.right) / 2, (wi.rcWindow.top + wi.rcWindow.bottom) / 2);
        }
    }
}
